using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockTime : MonoBehaviour
{
    //Script
    public DayTimer digitalTime;
    
    //Object To Animate
    public GameObject hourParent;
    public GameObject minuteParent;

    public float angleHours;
    public float angleMin;
    public float addAngle = 0;



    // Start is called before the first frame update
    void Start()
    {
        #region Set Angle
        angleHours = hourParent.transform.localEulerAngles.z;

        angleMin = minuteParent.transform.localEulerAngles.z;
        #endregion
    }

    // Update is called once per frame
    void Update()
    {

        if(digitalTime.timing)
        {
            angleHours -= (Time.deltaTime * addAngle) / 12;
            angleMin -= Time.deltaTime * addAngle;

            hourParent.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, angleHours);
            minuteParent.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, angleMin);

        }

    }
}
