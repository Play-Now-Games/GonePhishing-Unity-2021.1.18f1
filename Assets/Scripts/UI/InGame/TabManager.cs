using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabManager : MonoBehaviour
{

    public GameObject[] tabs;

    public void OpenTab(int index)
    {
        if (tabs.Length > index)
        {
            tabs[index].transform.SetSiblingIndex(tabs.Length);
        }
        else
        {
            Debug.LogError("Trying to open a tab that does not exist.");
        }
    }
}
