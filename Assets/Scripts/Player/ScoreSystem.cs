using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{

    public float Score { get; private set; }
    public uint Stars { get; private set; }

    [SerializeField]
    private float _maxTimeBonus;
    [SerializeField]
    private float _2StarScoreThreshold;
    [SerializeField]
    private float _3StarScoreThreshold;

    private uint _streak = 0;
    private float _multiplier = 1.0f;
    private bool _isPerfect = true;

    [SerializeField]
    private DayTimer _timer;

    public void AddScore(float amount)
    {
        Score += amount * _multiplier;
    }

    public void ScoreMultiplierStreakAdd()
    {
        _streak++;
        #region Adjust multiplier
        switch (_streak)
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
        _streak = 0;
        _multiplier = 1.0f;
        _isPerfect = false;
    }

    float CalculateTimeBonus()
    {
        return (_maxTimeBonus / (_timer.CurrentTimeLimit * _timer.CurrentTimeLimit))
            * ((_timer.CurrentTime - _timer.CurrentTimeLimit) * (_timer.CurrentTime - _timer.CurrentTimeLimit));
    }

    void CalculateFinalScore()
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
