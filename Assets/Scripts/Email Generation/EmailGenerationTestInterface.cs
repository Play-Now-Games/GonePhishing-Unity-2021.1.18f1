using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EmailGenerationTestInterface : MonoBehaviour
{

    public Email_Scriptable selectedEmail = null;
    public GameObject emailPrefab;
    public GameObject selected;


    public Email_Scriptable[] emails;


    // Start is called before the first frame update
    void Start()
    {

        UICreation();
    }

    // Update is called once per frame
    void Update()
    {

        if (selectedEmail)
        {
            selected.SetActive(true);
        }
        else
        {
            selected.SetActive(false);
        }
    }

    public void UICreation()
    {
        #region Spawn Emails

        //Find By Name
        GameObject Content = GameObject.Find("Content");

        //Vectors to spawn -- 110 is the 100 + offset
        Vector2 height = new Vector2(0, 155.0f);

        //-1 to work on
        for (int i = 0; i < emails.Length; i++)
        {
            //Email Pos Based on Pos in the array
            Vector2 Transfor = new Vector2(Content.transform.position.x, Content.transform.position.y);
            Vector2 emailNewPos = Transfor - (height * i);

            //Instantiate & Set Child
            GameObject ChildObject = Instantiate(emailPrefab, new Vector3(emailNewPos.x, emailNewPos.y, 0), Quaternion.identity);
            ChildObject.transform.parent = Content.transform;

            //Add Scriptable Object Here
            EmailHolderEG holder = ChildObject.GetComponent<EmailHolderEG>();
            holder.holder = emails[i];
        }
        #endregion

        #region Update Height - Scroll Bar

        RectTransform RectT = Content.GetComponent<RectTransform>();
        RectT.sizeDelta = new Vector2(RectT.sizeDelta.x, height.y * emails.Length);

        #endregion
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

    public Array AddArrayAtStart(object o, Array oldArray)
    {

        #region Add 'Array Object' At The Start Of the array

        Array NewArray = Array.CreateInstance(oldArray.GetType().GetElementType(), oldArray.Length + 1);

        for (int i = 1; i < oldArray.Length + 1; ++i)
        {
            NewArray.SetValue(oldArray.GetValue(i - 1), i);
        }

        NewArray.SetValue(o, 0);

        oldArray = NewArray;

        return oldArray;

        #endregion

    }

    public void AddEmail(Email_Scriptable email)
    {
        emails = (Email_Scriptable[])AddArrayAtStart(email, emails);

        GameObject[] EmailsOnScene = GameObject.FindGameObjectsWithTag("Email");
        DestroyAllEmails(EmailsOnScene);
        UICreation();
    }
}
