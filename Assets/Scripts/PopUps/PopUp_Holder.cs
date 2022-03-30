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
    private ScoreSystem score;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.Find("====Character/Camera====");
        mainScript = player.GetComponent<Main>();
        GameObject scoreObject = GameObject.Find("====Score====");
        score = scoreObject.GetComponent<ScoreSystem>();
    }

    public void DeletePopUp()
    {
        mainScript.DestroyPopUps(ID);
        score.AddScore(10);
    }

    public void GivePenality()
    {
        Debug.Log("GivePenality");
        score.ScoreMultiplierStreakReset();
    }

}
