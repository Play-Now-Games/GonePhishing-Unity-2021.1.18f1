using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Email", menuName = "ScriptableObjects/Email")]
public class Email : ScriptableObject
{
    [TextArea]
    public string text;

    public Sprite[] sprites;

}
