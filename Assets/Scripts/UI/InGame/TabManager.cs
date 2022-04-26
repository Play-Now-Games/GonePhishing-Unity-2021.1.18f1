using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabManager : MonoBehaviour
{

    public GameObject[] tabs;


    private SoundsHolder _audioScript;

    public void Start()
    {
        //GetAudio Source
        GameObject speakers = GameObject.FindGameObjectWithTag("Speakers");
        _audioScript = speakers.GetComponent<SoundsHolder>();
    }

    public void OpenTab(int index)
    {
        if (tabs.Length > index)
        {
            tabs[index].transform.SetSiblingIndex(tabs.Length);

            _audioScript.PlayClick();
        }
        else
        {
            Debug.LogError("Trying to open a tab that does not exist.");
        }
    }
}
