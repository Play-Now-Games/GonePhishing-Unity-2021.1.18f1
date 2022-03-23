//Gabriel 'DiosMussolinos' Vergari
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{

    ///////// PUBLIC /////////
    public Email_Scriptable selectedEmail = null;
    public GameObject emailPrefab;
    public GameObject selected;
    [HideInInspector]
    public SelectedAnimator selectedAnimator;
    public GameObject noEmail;

    public int healthPoints;
    public int maxHealthPoints;

    public Email_Scriptable[] totalEmails;

    public int phishingAmount;

    public Email_Scriptable[] normalEmails;
    //To Do: Delete _phishing
    public Email_Scriptable[] easyPhishing;
    public Email_Scriptable[] mediumPhishing;
    public Email_Scriptable[] hardPhishing;

    public UnityEvent onGameEnd;
    public UnityEvent onGameEndWin;
    public UnityEvent onGameEndLoss;

    public UnityEvent healthUpdate;

    [HideInInspector]
    public bool dayEnded = false;
    ///////// PUBLIC /////////

    ///////// PRIVATES /////////
    [SerializeField]
    private GameObject[] _totalPopUps;

    [SerializeField]
    private int _strike = 0;

    private float time = 5;
    private float popUpLimiter = 0;
    [SerializeField]
    private int currency = 0;

    private DayTimer _time;

    private HealthDisplay _healthDisplay;


    private SoundsHolder _audioScript;
    ///////// PRIVATES /////////

    //Do its commands BEFORE the first frame
    private void Awake()
    {
        //GetAudio Source
        GameObject speakers = GameObject.FindGameObjectWithTag("Speakers");
        _audioScript = speakers.GetComponent<SoundsHolder>();

        //Get HealthDisplay
        GameObject health = GameObject.FindGameObjectWithTag("HealthDisplay");
        _healthDisplay = health.GetComponent<HealthDisplay>();

        //Scene ID James = 1
        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            AwakeRandomizationEmail();
        }

        _time = GetComponent<DayTimer>();


    }

    private void AwakeRandomizationEmail()
    {
        #region Email Randomization

        int proportion = totalEmails.Length / phishingAmount;

        for (int i = 0; i < totalEmails.Length; i++)
        {
            proportion--;

            if (proportion > 0)
            {
                #region Add Normal Email
                //Random Index from the Array
                int randomIndex = UnityEngine.Random.Range(0, normalEmails.Length);

                //Add it to the TOTAL array, and remove the previously selected
                totalEmails[i] = normalEmails[randomIndex];
                StartRemoveAt(ref normalEmails, randomIndex);
                #endregion
            }
            else
            {

                int randomIndex = UnityEngine.Random.Range(0, 3);

                switch(randomIndex)
                {
                    case 1:
                        AwakeAddEmail(easyPhishing, i);
                        break;
                    case 2:
                        AwakeAddEmail(mediumPhishing, i);
                        break;
                    case 3:
                        AwakeAddEmail(hardPhishing, i);
                        break;
                }

                //Return Proportion
                proportion = totalEmails.Length / phishingAmount;
            }
        }

        #endregion
    }

    private void AwakeAddEmail(Email_Scriptable[] originalArray, int i)
    {
        if (originalArray.Length > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, originalArray.Length);

            //Add it to the TOTAL array, and remove the previously selected
            totalEmails[i] = originalArray[randomIndex];
            StartRemoveAt(ref originalArray, randomIndex);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        selectedAnimator = selected.GetComponent<SelectedAnimator>();

        UICreation();
    }


    public void StartRemoveAt<T>(ref T[] arr, int index)
    {
        #region Remove The Array Info
        for (int i = index; i < arr.Length - 1; i++)
        {
            //Move elements to fill the gap
            arr[i] = arr[i + 1];
        }
        
        //Remove the index
        Array.Resize(ref arr, arr.Length - 1);
        #endregion
    }

    // Update is called once per frame
    void Update()
    {

        #region Show email or Not

        if (selectedEmail)
        {
            noEmail.SetActive(false);
            selected.SetActive(true);
        }
        else if(!selectedAnimator.isAnimating) //don't change while animating
        {
            noEmail.SetActive(true);
            selected.SetActive(false);
        }

        #endregion

        #region PopUps Spawn
        
        time -= Time.deltaTime;

        if((time < 0) && (!dayEnded))
        {
            SpawnPopUp();
            time = 5;
        }

        #endregion

        if (totalEmails.Length == 1 && !dayEnded)
        {
            if(_time.CurrentTime < _time.CurrentTimeLimit)
            {
                dayEnded = true;
                EndGame(true);
            }
        }

    }

    ///////// GENERAL FUNCTIONS FOR THE GAME /////////

    public void UICreation()
    {
        #region Spawn Emails

        //Find By Name
        GameObject Content = GameObject.Find("Content");

        //Vectors to spawn -- 110 is the 100 + offset
        Vector2 height = new Vector2(0, 2.5f);

        //-1 to work on
        for (int i = 0; i < totalEmails.Length - 1; i++)
        {
            //Email Pos Based on Pos in the array
            Vector2 Transfor = new Vector2(Content.transform.position.x - 15, Content.transform.position.y);
            Vector2 emailNewPos = Transfor - (height * i);

            //Instantiate & Set Child
            GameObject ChildObject = Instantiate(emailPrefab, new Vector3(emailNewPos.x, emailNewPos.y, 0), Quaternion.identity);
            ChildObject.transform.parent = Content.transform;

            //Add Scriptable Object Here
            EmailHolder holder = ChildObject.GetComponent<EmailHolder>();
            holder.holder = totalEmails[i];
        }
        #endregion

        #region Update Height - Scroll Bar

        RectTransform RectT = Content.GetComponent<RectTransform>();
        RectT.sizeDelta = new Vector2(RectT.sizeDelta.x, height.y * totalEmails.Length);

        #endregion
    }


    public void LoseHealth(int HpLost)
    {
        #region HP Related
        healthPoints -= HpLost;

        healthUpdate.Invoke();

        if (healthPoints <= 0)
        {
            EndGame(false);
        }

        //PopUpFormula
        popUpLimiter = ((healthPoints / 1.5f) * -1) + 4;

        #endregion


        #region Call Animation
        _healthDisplay.toAnimate = true;
        #endregion

    }

    public void GainHealth(int HpGain)
    {
        #region HP Related
        if (healthPoints < 5)
        {
            healthPoints += HpGain;
            popUpLimiter = ((healthPoints / 1.5f) * -1) + 4;
        }
        #endregion
    }

    public void StrikeAdd(int value)
    {
        #region Strikes
        _strike++;

        if(_strike >= 3)
        {
            GainHealth(1);
            StrikeZero();
        }
        #endregion
    }

    public void StrikeZero()
    {
        _strike = 0;
    }

    public void RestoreHealth(int HpRestored)
    {
        #region HP Related
        healthPoints += HpRestored;

        if (healthPoints > maxHealthPoints)
            healthPoints = maxHealthPoints;

        healthUpdate.Invoke();
        #endregion
    }

    public void EndGame(bool win)
    {
        onGameEnd.Invoke();
        if (win)
        {
            onGameEndWin.Invoke();
        }
        else
        {
            onGameEndLoss.Invoke();
        }
    }

    public void GiveMoney(int value)
    {
        currency += value;
    }

    public void LoseMoney(int value)
    {
        currency -= value;

        if (currency < 0)
        {
            currency = 0;
        }
    }

    public void DestroyAllEmails(GameObject[] EmailsOnScene)
    {

        #region Destroy it ALL

        for (int i = 0; i < EmailsOnScene.Length; i++)
        {
            Destroy(EmailsOnScene[i]);
        }

        #endregion

    }

    public void AddNormalEmails()
    {
        #region Add Normal Emails

        if (normalEmails.Length > 0)
        {

            //Add Email From _NormalEmails
            totalEmails = (Email_Scriptable[])AddArrayAtStart(normalEmails[0], totalEmails);
            
            //Update The Previous _NormalEmails Email From The Previous Selected Email
            StartRemoveAt(ref normalEmails, 0);

            //UICreation();

        }

        #endregion
    }

    public void AddPhishingEmails(Email_Scriptable[] originalArray)
    {

        #region Add Phishing Emails

        if (originalArray.Length > 0)
        {

            //Add Email From _phishing
            totalEmails = (Email_Scriptable[])AddArrayAtStart(originalArray[0], totalEmails);
            
            switch(originalArray[0].difficulty)
            {
                case 1:
                    StartRemoveAt(ref easyPhishing, 0);
                    break;
                case 2:
                    StartRemoveAt(ref mediumPhishing, 0);
                    break;
                case 3:
                    StartRemoveAt(ref hardPhishing, 0);
                    break;

            }            
        }
        else
        {
            if (normalEmails.Length > 0)
            {
                AddNormalEmails();
            }
        }
        #endregion

    }

    public Array AddArrayAtStart(object o, Array oldArray)
    {

        #region Add 'Array Object' At The Start Of the array

        Array NewArray = Array.CreateInstance(oldArray.GetType().GetElementType(), oldArray.Length + 1);

        for (int i = 0; i < 0; ++i)
        {
            NewArray.SetValue(oldArray.GetValue(i), i);
        }

        for (int i = 0 + 1; i < oldArray.Length; ++i)
        {
            NewArray.SetValue(oldArray.GetValue(i - 1), i);
        }

        NewArray.SetValue(o, 0);

        oldArray = NewArray;

        return oldArray;

        #endregion
    
    }


    public void SpawnPopUp()
    {

        #region Temporary Variables
        //Random PopUp
        int randomIndex = UnityEngine.Random.Range(0, _totalPopUps.Length);

        //Can Spawn
        bool CanSpawn = true;

        //PopUpArray
        GameObject[] Pops = GameObject.FindGameObjectsWithTag("PopUps");
        #endregion

        #region Search For Repetitive PopUps On Scene

        if ((Pops.Length < (int)popUpLimiter) && (totalEmails.Length != 1))
        {
            for (int i = 0; i < Pops.Length; i++)
            {
                CanSpawn = true;

                PopUp_Holder HolderInScene = Pops[i].GetComponent<PopUp_Holder>();
                PopUp_Holder ToSpawn = _totalPopUps[randomIndex].GetComponent<PopUp_Holder>();

                if (HolderInScene.ID == ToSpawn.ID)
                {
                    CanSpawn = false;
                    break;
                }
            }
            
            if (CanSpawn)
            {
                
                //Get Position
                float x = UnityEngine.Random.Range(105, 128);
                float y = UnityEngine.Random.Range(-383, -367);
                float z = 583.3f;
                
                Vector3 popUpNewPos = new Vector3(x, y, z);

                //Instantiate and select the instantiated as child of ...
                GameObject PopUp = GameObject.Find("==PopUps==");
                GameObject ChildObject = Instantiate(_totalPopUps[randomIndex], popUpNewPos, Quaternion.identity);
                ChildObject.transform.parent = PopUp.transform;

                #region Play Sound
                _audioScript.PlaySpawnPop();
                #endregion
            }
        }
        #endregion

    }

    public void DestroyPopUps(int popUpID)
    {
        #region Destroy Pop-Ups
        GameObject[] PopUpsOnScene = GameObject.FindGameObjectsWithTag("PopUps");

        for (int i = 0; i < _totalPopUps.Length - 1; i++)
        {

            PopUp_Holder PopUpScript = PopUpsOnScene[i].GetComponent<PopUp_Holder>();

            if (popUpID == PopUpScript.ID)
            {
                Destroy(PopUpsOnScene[i]);
                break;
            }
        }
        #endregion

        #region Play Sounds
        _audioScript.PlayDestroyPop();
        #endregion
    }

    ///////// GENERAL FUNCTIONS FOR THE GAME /////////

}

/*
Yes, I'm workaholic, how did you notice?
⣿⣿⣿⣿⣿⣿⣿⣿⡿⠿⠛⠛⠛⠋⠉⠈⠉⠉⠉⠉⠛⠻⢿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⡿⠋⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠉⠛⢿⣿⣿⣿⣿
⣿⣿⣿⣿⡏⣀⠀⠀⠀⠀⠀⠀⣀⣤⣤⣤⣄⡀⠀⠀⠀⠀⠀⠀⠙⢿⣿⣿
⣿⣿⣿⢏⣴⣿⣷⠀⠀⠀⠀⠀⢾⣿⣿⣿⣿⣿⣿⡆⠀⠀⠀⠀⠀⠀⠈⣿⣿
⣿⣿⣟⣾⣿⡟⠁⠀⠀⠀⠀⠀⢀⣾⣿⣿⣿⣿⣿⣷⢢⠀⠀⠀⠀⠀⠀⢸⣿
⣿⣿⣿⣿⣟⠀⡴⠄⠀⠀⠀⠀⠀⠀⠙⠻⣿⣿⣿⣿⣷⣄⠀⠀⠀⠀⠀⠀⠀⣿
⣿⣿⣿⠟⠻⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠶⢴⣿⣿⣿⣿⣿⣧⠀⠀⠀⠀⠀⠀⣿
⣿⣁⡀⠀⠀⢰⢠⣦⠀⠀⠀⠀⠀⠀⠀⠀⢀⣼⣿⣿⣿⣿⣿⡄⠀⣴⣶⣿⡄⣿
⣿⡋⠀⠀⠀⠎⢸⣿⡆⠀⠀⠀⠀⠀⠀⣴⣿⣿⣿⣿⣿⣿⣿⠗⢘⣿⣟⠛⠿⣼
⣿⣿⠋⢀⡌⢰⣿⡿⢿⡀⠀⠀⠀⠀⠀⠙⠿⣿⣿⣿⣿⣿⡇⠀⢸⣿⣿⣧⢀⣼
⣿⣿⣷⢻⠄⠘⠛⠋⠛⠃⠀⠀⠀⠀⠀⢿⣧⠈⠉⠙⠛⠋⠀⠀⠀⣿⣿⣿⣿⣿
⣿⣿⣧⠀⠈⢸⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠟⠀⠀⠀⠀⢀⢃⠀⠀⢸⣿⣿⣿⣿
⣿⣿⡿⠀⠴⢗⣠⣤⣴⡶⠶⠖⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⡸⠀⣿⣿⣿⣿
⣿⣿⣿⡀⢠⣾⣿⠏⠀⠠⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠛⠉⠀⣿⣿⣿⣿
⣿⣿⣿⣧⠈⢹⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣰⣿⣿⣿⣿
⣿⣿⣿⣿⡄⠈⠃⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣠⣴⣾⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣧⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣠⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣷⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣴⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣦⣄⣀⣀⣀⣀⠀⠀⠀⠀⠘⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⡄⠀⠀⠀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣧⠀⠀⠀⠙⣿⣿⡟⢻⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠇⠀⠁⠀⠀⠹⣿⠃⠀⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⡿⠛⣿⣿⠀⠀⠀⠀⠀⠀⠀⠀⢐⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣿⣿⣿⣿⠿⠛⠉⠉⠁⠀⢻⣿⡇⠀⠀⠀⠀⠀⠀⢀⠈⣿⣿⡿⠉⠛⠛⠛⠉⠉
⣿⡿⠋⠁⠀⠀⢀⣀⣠⡴⣸⣿⣇⡄⠀⠀⠀⠀⢀⡿⠄⠙⠛⠀⣀⣠⣤⣤⠄*/