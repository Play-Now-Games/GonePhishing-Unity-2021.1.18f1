using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayTimer : MonoBehaviour
{

    public float CurrentTime;

    [Tooltip("Time limit in real-world seconds.")]
    public float CurrentTimeLimit = 100.0f;

    [System.Serializable]
    public class DigitalTime
    {
        public int hours;
        public int minutes;
    }

    [Tooltip("What time of day the scene begins (just cosmetic).")]
    public DigitalTime StartTime;

    [Tooltip("How many in-game seconds pass per real-world second.")]
    public float Timescale = 60.0f;

    // Start is called before the first frame update
    void Start()
    {
        CurrentTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        CurrentTime += Time.deltaTime;
    }

    // Get current elapsed time as a digital time
    DigitalTime GetDigitalTime()
    {
        DigitalTime time = new DigitalTime();

        int inGameSeconds = Mathf.FloorToInt(CurrentTime * Timescale);
        int inGameMinutes = inGameSeconds / 60;

        time.hours = inGameMinutes / 60;
        time.minutes = inGameMinutes % 60;

        return time;
    }

}
