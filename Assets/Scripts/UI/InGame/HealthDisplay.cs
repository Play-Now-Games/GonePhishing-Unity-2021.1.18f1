using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{

    [Tooltip("Images for each health point.  Array size should match max HP (plus one more sprite for zero HP).")]
    public Sprite[] healthStages;

    public bool toAnimate;
    public int animationTotal;
    public float angleAdd;

    private float angle = 180;
    public int animationTimes;
    private Main _mainScript;


    void Start()
    {
        #region Player Related

        GameObject player = GameObject.Find("====Character/Camera====");
        _mainScript = player.GetComponent<Main>();

        #endregion
    }

    private void Update()
    {
        HealthAnimation();
    }

    public void UpdateHealthSprite()
    {

        //if (healthStages.Length - 1 != _mainScript.maxHealthPoints)
        //{
        //    print("Warning: number of health sprites is not the same as the number of health points");
        //}

        //Image healthDisplayImage = gameObject.GetComponent<Image>();
        //healthDisplayImage.sprite = healthStages[_mainScript.healthPoints];

    }


    private void HealthAnimation()
    {

        //I did as a while loop, and it didnt work, so, to Update it goes
        if (toAnimate)
        {
            if (animationTimes < animationTotal)
            {

                Image ImageCol = GetComponent<Image>();
                Color AlphaColor = ImageCol.color;
                float rad = angle * Mathf.Deg2Rad;
                AlphaColor.a = Mathf.Cos(rad);
                ImageCol.color = AlphaColor;

                angle += angleAdd;

                if (angle > 360)
                {
                    angle = 0;
                    animationTimes++;
                }

            }
            else
            {

                angle = 180;
                animationTimes = 0;
                toAnimate = false;

            }
        }
    }
}
