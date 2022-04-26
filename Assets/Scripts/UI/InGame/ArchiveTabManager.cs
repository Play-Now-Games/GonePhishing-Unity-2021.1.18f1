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

        Vector2 gapBetweenEmails = new Vector2(0, 2.5f);

        for (int i = 0; i < archivedEmails.Length; i++)
        {
            //Email Pos Based on Pos in the array
            Vector2 transfrom = new Vector2(archiveContent.transform.position.x - 15, archiveContent.transform.position.y);
            Vector2 emailNewPos = transfrom - (gapBetweenEmails * i);

            //Instantiate & Set Child
            GameObject childObject = Instantiate(emailPrefab, new Vector3(emailNewPos.x, emailNewPos.y, 0), Quaternion.identity);
            childObject.transform.parent = archiveContent.transform;

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
        rectT.sizeDelta = new Vector2(rectT.sizeDelta.x, gapBetweenEmails.y * archivedEmails.Length);

        #endregion
    }
}
