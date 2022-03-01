using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{

    [Tooltip("Images for each health point.  Array size should match max HP (plus one more sprite for zero HP).")]
    public Sprite[] healthStages;
    [Tooltip("Reference to Main script on a character.")]
    public Main MainScript;

    public void UpdateHealthSprite()
    {

        if (healthStages.Length - 1 != MainScript.maxHealthPoints)
        {
            print("Warning: number of health sprites is not the same as the number of health points");
        }
        
        Image healthDisplayImage = gameObject.GetComponent<Image>();
        healthDisplayImage.sprite = healthStages[MainScript.healthPoints];

    }
}
