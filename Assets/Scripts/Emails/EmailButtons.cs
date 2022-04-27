//Gabriel 'DiosMussolinos' Vergari
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmailButtons : MonoBehaviour
{

    public Email_Scriptable holderCopy;

    [SerializeField]
    private Main mainScript;
    private SoundsHolder _soundsHolder;

    private Button _button;
    private Vector2 _position;

    [SerializeField]
    private Phishy phishy;
    [SerializeField]
    private ScoreSystem score;

    private void Start()
    {
        #region Player Related

        GameObject player = GameObject.Find("====Character/Camera====");
        mainScript = player.GetComponent<Main>();


        GameObject soundHolder = GameObject.FindGameObjectWithTag("Speakers");
        _soundsHolder = soundHolder.GetComponent<SoundsHolder>();

        #endregion


        _button = this.GetComponent<Button>();
        _position = this.gameObject.GetComponent<RectTransform>().anchoredPosition;
    }

    private void Update()
    {
        if (mainScript.selectedEmail)
        {
            _button.interactable = true;
        }
        else
        {
            _button.interactable = false;
        }
    }

    public void ClickAnswer()
    {
        #region Click Ãnswer Button
        if (mainScript.selectedEmail) //ignore clicks when no seslected email
        {
            if (!mainScript.dayEnded)
            {

                GameObject[] EmailsOnScene = GameObject.FindGameObjectsWithTag("Email");

                //Search the Email on the Array of Emails
                for (int i = 0; i < EmailsOnScene.Length; i++)
                {
                    if (mainScript.totalEmails[i].ID == holderCopy.ID)
                    {
                        //Remove Array from the original Array
                        mainScript.StartRemoveAt(ref mainScript.totalEmails, i);

                        //Destroy emails to Recreate in a new position
                        mainScript.DestroyAllEmails(EmailsOnScene);

                        //Restart the "Nothing Here"
                        RestartNothingHere();

                        //start animation
                        mainScript.selectedAnimator.Animate(_position);

                        //Email.isPhishing == True
                        if (holderCopy.isPhishing)
                        {
                            BadFeedBackPhishing();
                        }
                        else
                        {
                            GoodFeedBackReal();
                        }

                        break;
                    }
                }
            }
        }
        #endregion
    }


    public void ClickIgnored()
    {
        #region Click Ignore Button
        if (mainScript.selectedEmail) //ignore clicks when no seslected email
        {
            if(!mainScript.dayEnded)
            {
                GameObject[] EmailsOnScene = GameObject.FindGameObjectsWithTag("Email");

                //Search the Email on the Array of Emails
                for (int i = 0; i < EmailsOnScene.Length; i++)
                {
                    if (mainScript.totalEmails[i].ID == holderCopy.ID)
                    {

                        //Remove Array from the original Array
                        mainScript.StartRemoveAt(ref mainScript.totalEmails, i);

                        //Destroy emails to Recreate in a new position
                        mainScript.DestroyAllEmails(EmailsOnScene);

                        //Restart the "Nothing Here"
                        RestartNothingHere();

                        //start animation
                        mainScript.selectedAnimator.Animate(_position);

                        //Email.isPhishing == True
                        if (!holderCopy.isPhishing)
                        {
                            BadFeedBackReal();
                        }
                        else
                        {
                            GoodFeedBackPhishing();
                        }

                        break;
                    }
                }
            }
        }
        #endregion
    }



    private void RestartNothingHere()
    {

        //Restart Nothing Here
        mainScript.selectedEmail = null; //Main will set selected to inactive when selectedEmail is null 
    }

    private void GoodFeedBackReal()
    {
        #region Good Feeback
        //mainScript.StrikeAdd(1);
        mainScript.GiveMoney(100);

        _soundsHolder.PlayGoodFeedback();

        //UI Creation
        mainScript.UICreation();
        phishy.TriggerPhishyComment(false);
        score.AddScore(100);
        score.ScoreMultiplierStreakAdd();
        #endregion
    }

    private void BadFeedBackReal()
    {
        #region Bad feedBack
        //mainScript.LoseHealth(1);
        mainScript.LoseMoney(200);

        int rand = UnityEngine.Random.Range(0, 2);

        #region Add Email (if possible)
        if (mainScript.generateEmails)
        {
            if (rand == 0)
            {
                mainScript.GenerateEmail();
            }
            else
            {
                //Play Feedback sound is on the AddEmail Functions
                switch (score.streak)
                {
                    case 0:
                    case 1:
                        mainScript.GenerateEmail(true, 1);
                        break;
                    case 2:
                        mainScript.GenerateEmail(true, 2);
                        break;
                    case 3:
                        mainScript.GenerateEmail(true, 3);
                        break;
                }
            }
        }
        else
        {

            if (rand == 0 && mainScript.normalEmails.Length > 0)
            {
                mainScript.AddEmails(mainScript.normalEmails);
            }
            else
            {
                //Play Feedback sound is on the AddEmail Functions
                switch (score.streak)
                {
                    case 0:
                        mainScript.AddEmails(mainScript.easyPhishing);
                        break;
                    case 1:
                        mainScript.AddEmails(mainScript.easyPhishing);
                        break;
                    case 2:
                        mainScript.AddEmails(mainScript.mediumPhishing);
                        break;
                    case 3:
                        mainScript.AddEmails(mainScript.hardPhishing);
                        break;
                }
            }
        }
        #endregion

        mainScript.UICreation();
        phishy.TriggerPhishyComment(true);
        score.ScoreMultiplierStreakReset();
        #endregion
    }
    private void GoodFeedBackPhishing()
    {
        #region Good Feeback
        //mainScript.StrikeAdd(1);
        mainScript.GiveMoney(100);

        _soundsHolder.PlayGoodFeedback();

        //UI Creation
        mainScript.UICreation();
        phishy.TriggerPhishyComment(false);
        score.AddScore(100);
        score.ScoreMultiplierStreakAdd();
        #endregion
    }

    private void BadFeedBackPhishing()
    {
        #region Bad feedBack
        //mainScript.LoseHealth(1);
        mainScript.LoseMoney(200);

        int rand = UnityEngine.Random.Range(0, 2);

        #region Add Email (if possible)
        if (mainScript.generateEmails)
        {
            if (rand == 0)
            {
                mainScript.GenerateEmail();
            }
            else
            {
                //Play Feedback sound is on the AddEmail Functions
                switch (score.streak)
                {
                    case 0:
                    case 1:
                        mainScript.GenerateEmail(true, 1);
                        break;
                    case 2:
                        mainScript.GenerateEmail(true, 2);
                        break;
                    case 3:
                        mainScript.GenerateEmail(true, 3);
                        break;
                }
            }
        }
        else
        {

            if (rand == 0 && mainScript.normalEmails.Length > 0)
            {
                mainScript.AddEmails(mainScript.normalEmails);
            }
            else
            {
                //Play Feedback sound is on the AddEmail Functions
                switch (score.streak)
                {
                    case 0:
                        mainScript.AddEmails(mainScript.easyPhishing);
                        break;
                    case 1:
                        mainScript.AddEmails(mainScript.easyPhishing);
                        break;
                    case 2:
                        mainScript.AddEmails(mainScript.mediumPhishing);
                        break;
                    case 3:
                        mainScript.AddEmails(mainScript.hardPhishing);
                        break;
                }
            }
        }
        #endregion

        mainScript.UICreation();
        phishy.TriggerPhishyComment(true);
        score.ScoreMultiplierStreakReset();
        #endregion
    }
}


/*
"Sometime you want to end a project, find a job. Some other times, bugs remove the whole level of sanity that you have.
I wrote most of this code, and I don't understand where the bug is. How am i? Dude, a grave never felt so confortable"
 
- Adding email, the bug that made me ask eveything about my life >:(
 */
