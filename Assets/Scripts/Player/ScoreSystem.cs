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
    [Tooltip("How much score the player needs to earn 1 initial stars, if they fail to earn 2.")]
    private float _1StarScoreThreshold;
    [SerializeField]
    [Tooltip("How much score the player needs to earn both initial stars.")]
    private float _2StarScoreThreshold;

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

        // Un-set the perfect bonus if player never took action
        if (Score == 0)
            _isPerfect = false;

        #region Give stars
        Stars = 0;

        if (Score >= _2StarScoreThreshold)
        {
            Stars = 2;
        }
        else if (Score >= _1StarScoreThreshold)
        {
            Stars = 1;
        }
        else
        {
            Stars = 0;
        }

        if (_isPerfect)
        {
            Stars++;
        }
        #endregion
    }

}
