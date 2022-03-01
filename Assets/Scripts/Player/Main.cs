//Gabriel 'DiosMussolinos' Vergari
using System;
using UnityEngine;
using UnityEngine.Events;

public class Main : MonoBehaviour
{

    ///////// PUBLIC /////////
    public Email_Scriptable selectedEmail = null;
    public GameObject emailPrefab;
    public GameObject selected;
    public GameObject noEmail;

    public int healthPoints;

    public Email_Scriptable[] _totalEmails;

    public UnityEvent onGameEnd;
    public UnityEvent onGameEndWin;
    public UnityEvent onGameEndLoss;
    ///////// PUBLIC /////////

    ///////// PRIVATES /////////
    [SerializeField]
    private int _phishingAmount;

    [SerializeField]
    private Email_Scriptable[] _normalEmails;

    [SerializeField]
    private Email_Scriptable[] _phishing;

    [SerializeField]
    private GameObject[] _totalPopUps;

    private float time = 0.5f;
    ///////// PRIVATES /////////

    //Do its commands BEFORE the first frame
    private void Awake()
    {

        AwakeRandomizationEmail();
    
    }

    private void AwakeRandomizationEmail()
    {

        #region Email Randomization

        int proportion = _totalEmails.Length / _phishingAmount;

        for (int i = 0; i < _totalEmails.Length; i++)
        {
            proportion--;

            if (proportion > 0)
            {
                //Random Index from the Array
                int randomIndex = UnityEngine.Random.Range(0, _normalEmails.Length);

                //Add it to the TOTAL array, and remove the previously selected
                _totalEmails[i] = _normalEmails[randomIndex];
                StartRemoveAt(ref _normalEmails, randomIndex);
            }
            else
            {
                int randomIndex = UnityEngine.Random.Range(0, _phishing.Length);
                
                //Add it to the TOTAL array, and remove the previously selected
                _totalEmails[i] = _phishing[randomIndex];
                StartRemoveAt(ref _phishing, randomIndex);

                //Return Proportion
                proportion = _totalEmails.Length / _phishingAmount;
            }
        }
        
        #endregion
    
    }

    // Start is called before the first frame update
    void Start()
    {
        StartUICreation();
    }

    public void StartUICreation()
    {
        #region Spawn Emails

        //Find By Name
        GameObject Content = GameObject.Find("Content");

        //Vectors to spawn -- 110 is the 100 + offset
        Vector2 height = new Vector2(0, 110);

        for (int i = 0; i < _totalEmails.Length; i++)
        {
            //Email Pos Based on Pos in the array
            Vector2 Transfor = new Vector2(Content.transform.position.x - 25, Content.transform.position.y);
            Vector2 emailNewPos = Transfor - (height * i);

            //Instantiate & Set Child
            GameObject ChildObject = Instantiate(emailPrefab, emailNewPos, Quaternion.identity);
            ChildObject.transform.parent = Content.transform;

            //Add Scriptable Object Here
            EmailHolder holder = ChildObject.GetComponent<EmailHolder>();
            holder.holder = _totalEmails[i];
        }
        #endregion

        #region Update Height - Scroll Bar

        RectTransform RectT = Content.GetComponent<RectTransform>();
        RectT.sizeDelta = new Vector2(RectT.sizeDelta.x, height.y * _totalEmails.Length);

        #endregion
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
            //Turn Off the 
            noEmail.SetActive(false);
        }
        else
        {
            noEmail.SetActive(true);
        }

        #endregion


        time -= Time.deltaTime;

        if(time < 0)
        {
            SpawnPopUp();
            time = 0.5f;
        }
        

        if (_totalEmails.Length == 0)
        {
            EndGame(true);
        }

    }

    ///////// GENERAL FUNCTIONS FOR THE GAME /////////

    public void UICreation()
    {
        #region Spawn Emails

        //Find By Name
        GameObject Content = GameObject.Find("Content");

        //Vectors to spawn -- 110 is the 100 + offset
        Vector2 height = new Vector2(0, 110);

        //-1 to work on
        for (int i = 0; i < _totalEmails.Length - 1; i++)
        {
            //Email Pos Based on Pos in the array
            Vector2 Transfor = new Vector2(Content.transform.position.x - 25, Content.transform.position.y);
            Vector2 emailNewPos = Transfor - (height * i);

            //Instantiate & Set Child
            GameObject ChildObject = Instantiate(emailPrefab, emailNewPos, Quaternion.identity);
            ChildObject.transform.parent = Content.transform;

            //Add Scriptable Object Here
            EmailHolder holder = ChildObject.GetComponent<EmailHolder>();
            holder.holder = _totalEmails[i];
        }
        #endregion

        #region Update Height - Scroll Bar

        RectTransform RectT = Content.GetComponent<RectTransform>();
        RectT.sizeDelta = new Vector2(RectT.sizeDelta.x, height.y * _totalEmails.Length);

        #endregion
    }

    public void LoseHealth(int HpLost)
    {
        healthPoints -= HpLost;

        if (healthPoints <= 0)
        {
            EndGame(false);
        }
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

    public void DestroyAllEmails(GameObject[] EmailsOnScene)
    {

        #region Destroy it ALL

        for (int j = 0; j < EmailsOnScene.Length; j++)
        {
            Destroy(EmailsOnScene[j]);
        }

        #endregion

    }

    public void AddNormalEmails()
    {
        #region Add Normal Emails

        if (_normalEmails.Length > 0)
        {
            //Add Email From _NormalEmails
            _totalEmails = (Email_Scriptable[])AddArrayAtStart(_normalEmails[_normalEmails.Length - 1], _totalEmails);

            //Update The Previous _NormalEmails Email From The Previous Selected Email
            StartRemoveAt(ref _normalEmails, _normalEmails.Length - 1);

            //Delete ALL emails on the scene
            GameObject[] EmailsOnScene = GameObject.FindGameObjectsWithTag("Email");
            DestroyAllEmails(EmailsOnScene);

            //ReCreate the UI with all the emails
            UICreation();
        }

        #endregion
    }

    public void AddPhishingEmails()
    {

        #region Add Phishing Emails
        
        if (_phishing.Length > 0)
        {

            //Add Email From _NormalEmails
            _totalEmails = (Email_Scriptable[])AddArrayAtStart(_normalEmails[_normalEmails.Length - 1], _totalEmails);

            //Update The Previous _NormalEmails Email From The Previous Selected Email
            StartRemoveAt(ref _normalEmails, _normalEmails.Length - 1);

            //Delete ALL emails on the scene
            GameObject[] EmailsOnScene = GameObject.FindGameObjectsWithTag("Email");
            DestroyAllEmails(EmailsOnScene);

            //ReCreate the UI with all the emails
            UICreation();
        
        }
        
        #endregion
    
    }

    public Array AddArrayAtStart(object o, Array oldArray)
    {

        #region Add 'Array Object' At The Start Of the array

        Array NewArray = Array.CreateInstance(oldArray.GetType().GetElementType(), oldArray.Length + 1);

        for(int i = 0; i < 0; ++i)
        {
            NewArray.SetValue(oldArray.GetValue(i), i);
        }

        for(int i = 0 + 1; i < oldArray.Length; ++i)
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
        #endregion
        

        #region Spawn Unique PopUp
        if (CanSpawn)
        {
            //Get Position
            float x = UnityEngine.Random.Range(590, 1386);
            float y = UnityEngine.Random.Range(146, 640);
            float z = 6;
            Vector3 popUpNewPos = new Vector3(x, y, z);

            //Instantiate and select the instantiated as child of ...
            GameObject PopUp = GameObject.Find("==PopUps==");
            GameObject ChildObject = Instantiate(_totalPopUps[randomIndex], popUpNewPos, Quaternion.identity);
            ChildObject.transform.parent = PopUp.transform;
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