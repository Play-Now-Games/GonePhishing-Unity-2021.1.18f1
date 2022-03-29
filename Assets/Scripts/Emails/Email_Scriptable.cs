//Gabriel 'DiosMussolinos' Vergari
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "EmailsExample", menuName = "ScriptableObjects/Emails")]
public class Email_Scriptable : ScriptableObject
{

    public int ID;

    public Sprite logo;
    
    public string sender;

    public string senderAdress;

    public string tittle;

    public string greetings;

    public string content;

    public string bye;

    public string timeHour;

    public string timeMin;

    public bool isPhishing;

    [Tooltip("Write here the difficulty desired for this email, being 0 = NORMAL, 1 = EASY, 2 = MEDIUM, OR 3 = HARD")]
    public int difficulty;
}
