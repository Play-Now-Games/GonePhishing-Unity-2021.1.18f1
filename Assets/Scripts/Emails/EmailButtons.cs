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

        //Email.isPhishing == True
        if (holderCopy.isPhishing)
        {

            GameObject[] EmailsOnScene = GameObject.FindGameObjectsWithTag("Email");

            //Search the Email on the Array of Emails
            for (int i = 0; i < EmailsOnScene.Length; i++)
            {
                if (mainScript._totalEmails[i].ID == holderCopy.ID)
                {
                    //Less Health Points
                    mainScript.LoseHealth(1);

                    //Remove Array from the original Array
                    mainScript.StartRemoveAt(ref mainScript._totalEmails, i);

                    //Destroy emails to Recreate in a new position
                    mainScript.DestroyAllEmails(EmailsOnScene);

                    //Restart the "Nothing Here"
                    RestartNothingHere();

                    //Recreate the emails
                    mainScript.StartUICreation();

                    break;
                }
            }
        }
        else 
        {
            //ToDo: Do What the Team Decides
        
        }
    }


    public void ClickIgnored()
    {
        Debug.Log("Ignored the " + holderCopy);
        //Todo: Do the same on ClickAnwer(), BUT GIVE THE CORRECT FEEDBACK
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
}