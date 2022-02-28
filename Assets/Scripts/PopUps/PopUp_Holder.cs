//Gabriel 'DiosMussolinos' Vergari
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp_Holder : MonoBehaviour
{
    ///////// PUBLIC /////////
    public int ID;

    ///////// PRIVATE /////////
    private Main mainScript;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.Find("====Character/Camera====");
        mainScript = player.GetComponent<Main>();
    }

    public void DeletePopUp()
    {
        mainScript.DestroyPopUps(ID);
    }

}
