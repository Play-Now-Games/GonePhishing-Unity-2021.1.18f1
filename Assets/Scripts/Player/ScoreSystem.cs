using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{

    public float Score { get; private set; }

    [SerializeField]
    private float _2StarScoreThreshold;
    [SerializeField]
    private float _3StarScoreThreshold;

    private uint _streak = 0;
    private float _multiplier = 1.0f;
    private bool _isPerfect = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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

}
