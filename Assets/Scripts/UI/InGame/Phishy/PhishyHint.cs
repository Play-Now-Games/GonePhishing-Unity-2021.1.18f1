using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPhishyHint", menuName = "ScriptableObjects/PhishyHint")]
public class PhishyHint : ScriptableObject
{

    [SerializeField]
    public string[] HintText;

    public bool IsEvil;

    public float xPosition;

    public float yPosition;

}
