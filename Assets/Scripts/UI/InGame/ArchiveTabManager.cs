using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchiveTabManager : MonoBehaviour
{
    [HideInInspector]
    public Email_Scriptable selectedEmail = null;
    public GameObject emailPrefab;
    public GameObject selectedEmailDisplay;
    public GameObject blankDisplay;

    public Email_Scriptable[] archivedEmails;

    // Start is called before the first frame update
    void Start()
    {
        UICreation();
    }

    void Update()
    {
        if (selectedEmail)
        {
            blankDisplay.SetActive(false);
            selectedEmailDisplay.SetActive(true);
        }
        else
        {
            blankDisplay.SetActive(true);
            selectedEmailDisplay.SetActive(false);
        }
    }

    public void UICreation()
    {
        #region Spawn Emails

        //Find By Name
        GameObject archiveContent = GameObject.Find("ArchiveContent");

        float gapBetweenEmails = 300.0f;

        for (int i = 0; i < archivedEmails.Length; i++)
        {

            //Instantiate & Set Child
            GameObject childObject = Instantiate(emailPrefab, archiveContent.transform);

            //Email Pos Based on Pos in the array
            childObject.transform.localPosition = new Vector3(0, -(gapBetweenEmails * i), 0);

            //Add Scriptable Object Here
            EmailHolder holder = childObject.GetComponent<EmailHolder>();
            holder.holder = archivedEmails[i];

            //Tell email holder that is it in the archive tab
            holder.archiveEmail = true;
            holder.archiveTabManager = this;
        }
        #endregion

        #region Update Height - Scroll Bar

        RectTransform rectT = archiveContent.GetComponent<RectTransform>();
        rectT.sizeDelta = new Vector2(rectT.sizeDelta.x, gapBetweenEmails * archivedEmails.Length);

        #endregion
    }
}
