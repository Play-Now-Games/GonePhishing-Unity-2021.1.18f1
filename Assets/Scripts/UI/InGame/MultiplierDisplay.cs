using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiplierDisplay : MonoBehaviour
{
    [Tooltip("Item 0 should be a blank mulitpler, 1 is 1, 2 is 1.5, and 3 is 2")]
    public Sprite[] multiplierSprites;

    public void UpdateMuliplierSprite(uint streak)
    {
        uint multiplierIndex = streak;
        if (streak >= multiplierSprites.Length)
        {
            multiplierIndex = (uint)multiplierSprites.Length - 1;
        }

        Image healthDisplayImage = gameObject.GetComponent<Image>();
        healthDisplayImage.sprite = multiplierSprites[multiplierIndex];

    }
}
