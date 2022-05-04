//Gabriel 'DiosMussolinos' Vergari
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeAnimation : MonoBehaviour
{

    public Text _text;
    private Color _tColor;
    private float _constAdd;
    private float _angle;

    private DayTimer _time;

    private int _count = 0;
    
    public AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        #region Player Related

        GameObject player = GameObject.Find("====Character/Camera====");
        _time = player.GetComponent<DayTimer>();

        #endregion


        #region Text Related
        _text = GetComponent<Text>();
        _tColor = _text.color;
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
        if (_time.CurrentTime > _time.CurrentTimeLimit * 0.7f)
        {
            _constAdd = Mathf.Log(_time.CurrentTime, 1.5f) + (_time.CurrentTimeLimit * 0.15f) * 1.7f;

            _angle += _constAdd / 200;

            Color AlphaText = _text.color;
            float rad = _angle * Mathf.Deg2Rad;
            AlphaText.a = Mathf.Sin(rad);
            _text.color = AlphaText;

            if (_angle > 170)
            {
                _angle = 0;
            }
        }

        if (_time.CurrentTimeLimit - _time.CurrentTime < 8.0f && _count == 0)
        {
            sound.Play();
            _count++;
        }

    }

}
