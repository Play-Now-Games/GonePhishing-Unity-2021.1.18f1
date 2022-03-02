using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedAnimator : MonoBehaviour
{
    [Tooltip("Value to devide size by.")]
    public float shrinkFactor = 8.0f;
    [Tooltip("The time to takes to finish shrinking.")]
    public float shrinkTime = 0.15f;
    [Tooltip("Delay before this starts moving.")]
    public float moveDelay = 0.15f;
    [Tooltip("The time it takes to finish moving.")]
    public float moveTime = 0.25f;


    [HideInInspector]
    public bool isAnimating = false;

    private float _timeAnimating = 0.0f;

    private Vector2 _moveTarget;

    private RectTransform _rectTransform;
    private Vector2 _InitalSize;
    private Vector2 _InitalPos;

    // Start is called before the first frame update
    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _InitalPos = _rectTransform.anchoredPosition;
        _InitalSize = _rectTransform.sizeDelta;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAnimating)
        {
            _timeAnimating += Time.deltaTime;

            //shrinking;
            if (Vector2.Distance(_rectTransform.sizeDelta, _InitalSize / shrinkFactor) < 0.001 || _timeAnimating >= shrinkTime)
            {
                _rectTransform.sizeDelta = _InitalSize / shrinkFactor;
            }
            else
            {
                _rectTransform.sizeDelta = Vector2.Lerp(_InitalSize, _InitalSize / shrinkFactor, _timeAnimating / shrinkTime);
            }

            //moving;
            if (Vector2.Distance(_rectTransform.anchoredPosition, _moveTarget) < 0.001 || _timeAnimating >= moveDelay + moveTime)
            {
                _rectTransform.anchoredPosition = _moveTarget;
            }
            else if (_timeAnimating >= moveDelay)
            {
                _rectTransform.anchoredPosition = Vector2.Lerp(_InitalPos, _moveTarget, (_timeAnimating - moveDelay) / moveTime);
            }

            //Check if finished
            if (_timeAnimating >= shrinkTime && _timeAnimating >= moveDelay + moveTime)
            {
                isAnimating = false;
                _rectTransform.anchoredPosition = _InitalPos;
                _rectTransform.sizeDelta = _InitalSize;
                this.gameObject.SetActive(false);
            }
        }
    }

    public void Animate(Vector2 targetPos)
        // targetPos needs to be in local anchored pos coords
    {
        _moveTarget = targetPos;

        isAnimating = true;
        _timeAnimating = 0.0f;
    }
}
