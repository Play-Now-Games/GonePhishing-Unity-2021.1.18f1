//Gabriel 'DiosMussolinos' Vergari
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmailButtons : MonoBehaviour
{

    public Email_Scriptable holderCopy;

    [SerializeField]
    private Main mainScript;

    private void Start()
    {
        #region Player Related

        GameObject player = GameObject.Find("====Character/Camera====");
        mainScript = player.GetComponent<Main>();
        
        #endregion
    }

    public void ClickAnswer()
    {
        if (mainScript.selectedEmail) //ignore clicks when no seslected email
        {

            GameObject[] EmailsOnScene = GameObject.FindGameObjectsWithTag("Email");

            //Search the Email on the Array of Emails
            for (int i = 0; i < EmailsOnScene.Length; i++)
            {
                if (mainScript._totalEmails[i].ID == holderCopy.ID)
                {
                    //Email.isPhishing == True
                    if (holderCopy.isPhishing)
                    {
                        BadFeedBack();
                    }
                    else
                    {
                        GoodFeedBack();
                    }

                    //Remove Array from the original Array
                    mainScript.StartRemoveAt(ref mainScript._totalEmails, i);

                    //Destroy emails to Recreate in a new position
                    mainScript.DestroyAllEmails(EmailsOnScene);

                    //Restart the "Nothing Here"
                    RestartNothingHere();

                    //Recreate the emails
                    mainScript.StartUICreation();

                    //start animation
                    //mainScript.selectedAnimator.Animate(_position);

                    break;
                }
            }
        }
    }


    public void ClickIgnored()
    {

        if (mainScript.selectedEmail) //ignore clicks when no seslected email
        {

            GameObject[] EmailsOnScene = GameObject.FindGameObjectsWithTag("Email");

            //Search the Email on the Array of Emails
            for (int i = 0; i < EmailsOnScene.Length; i++)
            {
                if (mainScript._totalEmails[i].ID == holderCopy.ID)
                {
                    //Email.isPhishing == True
                    if (!holderCopy.isPhishing)
                    {
                        BadFeedBack();
                    }
                    else
                    {
                        GoodFeedBack();
                    }

                    //Remove Array from the original Array
                    mainScript.StartRemoveAt(ref mainScript._totalEmails, i);

                    //Destroy emails to Recreate in a new position
                    mainScript.DestroyAllEmails(EmailsOnScene);

                    //Restart the "Nothing Here"
                    RestartNothingHere();

                    //Recreate the emails
                    mainScript.StartUICreation();

                    //start animation
                    //mainScript.selectedAnimator.Animate(_position);

                    break;
                }
            }
        }
    }    

    private void RestartNothingHere()
    {

        #region Return the screen for the main one -> Set Active didn't work here.

        //Restart Position Text
        Vector3 newPos = new Vector3(mainScript.selected.transform.position.x, mainScript.selected.transform.position.y, -5000);
        mainScript.selected.transform.position = newPos;

        //Restart Nothing Here
        mainScript.selectedEmail = null;

        #endregion
    
    }

    private void GoodFeedBack()
    {
        #region Good Feeback
        mainScript.StrikeAdd(1);
        mainScript.GiveMoney(100);
        #endregion
    }

    private void BadFeedBack()
    {
        #region Bad feedBack
        mainScript.LoseHealth(1);
        mainScript.LoseMoney(200);
        #endregion
    }
}
