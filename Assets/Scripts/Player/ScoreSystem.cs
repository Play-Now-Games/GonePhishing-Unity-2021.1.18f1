using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{

    public float Score { get; private set; }
    public uint Stars { get; private set; }

    [SerializeField]
    [Tooltip("The initial time bonus before it drops off.")]
    private float _maxTimeBonus;
    [SerializeField]
    [Tooltip("How much score the player needs to earn 2 initial stars, if they fail to earn 3.")]
    private float _2StarScoreThreshold;
    [SerializeField]
    [Tooltip("How much score the player needs to earn all 3 initial stars.")]
    private float _3StarScoreThreshold;

    [HideInInspector]
    public uint streak = 0;
    private float _multiplier = 1.0f;

    private bool _isPerfect = true;

    [SerializeField]
    [Tooltip("Reference to the timer object.")]
    private DayTimer _timer;

    void Start()
    {
        if (_timer == null)
        {
            Debug.LogWarning("No timer assigned to a ScoreSystem!");
        }
    }

    public void AddScore(float amount)
    {
        Score += amount * _multiplier;
    }

    public void ScoreMultiplierStreakAdd()
    {
        streak++;
        #region Adjust multiplier
        switch (streak)
        {
            case 0:
            case 1:
                _multiplier = 1.0f;
                break;
            case 2:
                _multiplier = 1.5f;
                break;
            default:
                _multiplier = 2.0f;
                break;
        }
        #endregion
    }

    public void ScoreMultiplierStreakReset()
    {
        streak = 0;
        _multiplier = 1.0f;
        _isPerfect = false;
    }

    float CalculateTimeBonus()
    {
        return ((_maxTimeBonus - 1)
            / (_timer.CurrentTimeLimit * _timer.CurrentTimeLimit)
            * ((_timer.CurrentTime - _timer.CurrentTimeLimit) * (_timer.CurrentTime - _timer.CurrentTimeLimit))) + 1;
    }

    public void CalculateFinalScore()
    {
        Score *= CalculateTimeBonus();

        #region Give stars
        Stars = 0;

        if (Score >= _3StarScoreThreshold)
        {
            Stars = 3;
        }
        else if (Score >= _2StarScoreThreshold)
        {
            Stars = 2;
        }
        else
        {
            Stars = 1;
        }

        if (_isPerfect)
        {
            Stars++;
        }
        #endregion
    }

}
