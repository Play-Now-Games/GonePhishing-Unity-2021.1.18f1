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

    private Button _button;
    private Vector2 _position;

    private void Start()
    {
        #region Player Related

        GameObject player = GameObject.Find("====Character/Camera====");
        mainScript = player.GetComponent<Main>();

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
                        //Less Health Points
                        mainScript.LoseHealth(1);
                    }
                    else
                    {
                        //ToDo: Do What the Team Decides

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
                    mainScript.selectedAnimator.Animate(_position);

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
                    if (holderCopy.isPhishing)
                    {
                        Debug.Log("Ignored the " + holderCopy);
                        //Todo: Do the same on ClickAnwer(), BUT GIVE THE CORRECT FEEDBACK
                    }
                    else
                    {
                        Debug.Log("Ignored the " + holderCopy);
                        //Todo: Do the same on ClickAnwer(), BUT GIVE THE CORRECT FEEDBACK

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
                    mainScript.selectedAnimator.Animate(_position);

                    break;
                }
            }
        }
    }   
    

    private void RestartNothingHere()
    {
        //SetActive Issue fixed

        //Restart Nothing Here
        mainScript.selectedEmail = null; //Main will set selected to inactive when selectedEmail is null 
    }
}
