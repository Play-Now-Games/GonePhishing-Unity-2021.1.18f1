using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "EmailsExample", menuName = "ScriptableObjects/Emails")]
public class Email_Scriptable : ScriptableObject
{

    public int ID;

    public Image logo;
    
    public string sender;
    
    public string tittle;

    public string greetings;

    public string content;

    public string bye;

    public string timeHour;

    public string timeMin;

    public bool isPhishing;
}
