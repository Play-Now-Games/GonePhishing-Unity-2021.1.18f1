using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DayTimer : MonoBehaviour
{

    public float CurrentTime;
    public bool timing;
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
    [Tooltip("This text will be updated to match the start time + elapsed ingame time.")]
    public Text DigitalClockText;
    [Tooltip("When the clock text updates, it will be rounded DOWN to the nearest X ingame minutes.")]
    public int RoundingAmount = 5;
    [Tooltip("Prefab for a notification object that gets created when the time limit is changed.")]
    public GameObject NotificationPrefab;
    [Tooltip("The computer GUI object which notifications will appear above if the time limit is changed.")]
    public GameObject ComputerClockObject;

    public UnityEvent onTimeLimit;

    // Start is called before the first frame update
    void Start()
    {
        CurrentTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {

        #region Increment time and check for time limit
        if (timing)
        {
            CurrentTime += Time.deltaTime;
            if (CurrentTime >= CurrentTimeLimit)
            {
                onTimeLimit.Invoke();
            }
        }
        #endregion

        DigitalClockText.text = GetDigitalTimeAsString();

    }

    // Get current elapsed time as a digital time
    public DigitalTime GetDigitalTime()
    {
        DigitalTime time = new DigitalTime();

        int inGameSeconds = Mathf.FloorToInt(CurrentTime * Timescale);
        int inGameMinutes = inGameSeconds / 60;

        time.hours = (inGameMinutes / 60 + StartTime.hours) % 24;
        time.minutes = inGameMinutes % 60 + StartTime.minutes;

        return time;
    }

    public DigitalTime GetRoundedDigitalTime()
    {
        DigitalTime time = GetDigitalTime();

        int roundDifference = time.minutes % RoundingAmount;
        time.minutes -= roundDifference;

        return time;
    }

    public string GetDigitalTimeAsString()
    {
        DigitalTime time = GetRoundedDigitalTime();

        string hoursString = time.hours.ToString();
        if (hoursString.Length == 1)
            hoursString = "0" + hoursString;

        string minutesString = time.minutes.ToString();
        if (minutesString.Length == 1)
            minutesString = "0" + minutesString;

        return hoursString + ":" + minutesString;
    }

    public void StopTiming()
    {
        timing = false;
    }

    public void StartTiming()
    {
        timing = true;
    }

    // If the time limit needs to be changed, do it through this function to trigger a notification.  Amount is in real seconds.
    public void AddOrSubtractToTimeLimit(float amount)
    {
        print("Adding to timelimit");
        CurrentTimeLimit += amount;

        int change = Mathf.FloorToInt((amount * Timescale) / 60);
        string notifyText;
        if (change > 0)
        {
            notifyText = "Your time limit has been extended by " + change + " minutes.";
            DigitalClockNotification(notifyText);
        }
        else if (change < 0)
        {
            notifyText = "Your time limit has been reduced by " + (-change) + " minutes.";
            DigitalClockNotification(notifyText);
        }
    }

    void DigitalClockNotification(string text)
    {
        print("Creating notification");
        GameObject notificationObject = Instantiate(NotificationPrefab, ComputerClockObject.transform);
        notificationObject.GetComponentInChildren<Text>().text = text;
    }

}
