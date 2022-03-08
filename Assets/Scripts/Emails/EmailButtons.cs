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

                    //UI Creation
                    mainScript.UICreation();

                    //start animation
                    mainScript.selectedAnimator.Animate(_position);

                    
                    Debug.Log("aa");

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

                    mainScript.UICreation();

                    //start animation
                    mainScript.selectedAnimator.Animate(_position);

                    Debug.Log("aa");

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

        int rand = UnityEngine.Random.Range(0, 2);

        if (rand == 0)
        {
            mainScript.AddNormalEmails();
        }
        else
        {
            mainScript.AddPhishingEmails();
        }

        #endregion
    }
}
