//Gabriel 'DiosMussolinos' Vergari
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmailHolder : MonoBehaviour
{

    ///////// PUBLIC /////////
    //Scriptable Object
    public Email_Scriptable holder;

    //UI
    public Image logo;
    public Text sender;
    public Text senderAddress;
    public Text tittle;
    public Text content;
    public Text timeHour;
    public Text timeMin;

    ///////// PUBLIC /////////

    ///////// PRIVATE /////////
    private Main mainScript;
    private EmailButtons answerButton;
    private EmailButtons ignoreButton;
    ///////// PRIVATE /////////


    private void Start()
    {
        #region Email Related

        logo = holder.logo;
        sender.text = holder.sender;
        StartTittle();
        StartContent();
        /*
        timeHour.text = holder.timeHour;
        timeMin.text = holder.timeMin;
        */
        #endregion

        #region Player Related

        GameObject player = GameObject.Find("====Character/Camera====");
        mainScript = player.GetComponent<Main>();
        
        #endregion

        #region Buttons Related
        
        //Answer
        GameObject answer = GameObject.Find("=Answer=");
        answerButton = answer.GetComponent<EmailButtons>();

        //Refuse
        GameObject refuse = GameObject.Find("=Refuse=");
        ignoreButton = refuse.GetComponent<EmailButtons>();

        #endregion
    }

    private void StartTittle()
    {
        #region Simulation of OutLook Behavior

        if (holder.tittle.Length > 24)
        {
            //New Tittle Empty
            string NewTittle = "";

            for (int i = 0; i < holder.tittle.Length; i++)
            {

                if (i < 21)
                {
                    NewTittle += holder.tittle[i];
                }
                else
                {
                    NewTittle += "...";
                    tittle.text = NewTittle;
                    break;
                }
            }
        }
        else
        {
            tittle.text = holder.tittle;
        }

        #endregion
    }

    private void StartContent()
    {
        #region Simulation of OutLook Behavior

        if (holder.content.Length > 32)
        {
            //New Content Empty
            string NewContent = "";

            for (int i = 0; i < holder.content.Length; i++)
            {

                if (i < 21)
                {
                    NewContent += holder.content[i];
                }
                else
                {
                    NewContent += "...";
                    content.text = NewContent;
                    break;
                }
            }
        }
        else
        {
            content.text = holder.content;
        }

        #endregion
    }

    public void ClickEmail()
    {
        //SetActive Issue fixed
        //make sure email is active so it can be updated
        mainScript.selected.SetActive(true);

        mainScript.selectedEmail = holder;

        ClickChangeInfo();

    }

    public void ClickChangeInfo()
    {

        #region Change Button References

        answerButton.holderCopy = holder;

        ignoreButton.holderCopy = holder;

        #endregion

        #region Get All Info And Change - IF's are mandatory to prevent crashes

        GameObject BodyLogo = GameObject.Find("=Body-Image/Logo=");
        if (BodyLogo)
        {
            Image Logo = BodyLogo.GetComponent<Image>();
            if (Logo)
            {
                Logo = holder.logo;
            }
        }


        GameObject BodyTittle = GameObject.Find("=Body-Tittle=");
        if (BodyTittle)
        {
            Text Tittle = BodyTittle.GetComponent<Text>();
            if (Tittle)
            {
                Tittle.text = holder.tittle;

            }
        }

        GameObject BodySenderAdress = GameObject.Find("=Body-SenderAdress=");
        if (BodySenderAdress)
        {
            Text Tittle = BodySenderAdress.GetComponent<Text>();
            if (Tittle)
            {
                Tittle.text = holder.senderAdress;

            }
        }

        GameObject BodyGreetins = GameObject.Find("=Body-Greetins=");
        if (BodyGreetins)
        {
            Text Greetings = BodyGreetins.GetComponent<Text>();
            if (Greetings)
            {
                Greetings.text = holder.greetings;
            }
        }

        GameObject BodyContent = GameObject.Find("=Body-Content=");
        if (BodyContent)
        {
            Text Content = BodyContent.GetComponent<Text>();
            if (Content)
            {
                Content.text = holder.content;
            }
        }

        GameObject BodyBye = GameObject.Find("=Body-Bye=");
        if (BodyBye)
        {
            Text Bye = BodyBye.GetComponent<Text>();
            if (Bye)
            {
                Bye.text = holder.bye;
            }
        }

        GameObject BodyHour = GameObject.Find("=Body-TimeHour=");
        if (BodyHour)
        {
            Text Hour = BodyHour.GetComponent<Text>();
            if (Hour)
            {
                Hour.text = holder.timeHour;
            }
        }

        GameObject BodyMin = GameObject.Find("=Body-TimeMin=");
        if (BodyMin)
        {
            Text Min = BodyMin.GetComponent<Text>();
            if (Min)
            {
                Min.text = holder.timeMin;
            }
        }

        #endregion
    
    }
}
