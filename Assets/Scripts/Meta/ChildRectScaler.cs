using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildRectScaler : MonoBehaviour
{
    ///Assumes all child rects are center-middle justified///

    private RectTransform[] _childRects;
    private Vector2[] _initalSize;
    private Vector2[] _initalPos;

    private RectTransform _thisRect;
    private Vector2 _thisRectInitalSize;
    private Vector2 _thisRectCurrentSize;

    // Start is called before the first frame update
    void Start()
    {
        _childRects = GetComponentsInChildren<RectTransform>();

        _initalSize = new Vector2[_childRects.Length];
        _initalPos = new Vector2[_childRects.Length];

        for (int i = 0; i < _childRects.Length; i++)
        {
            _initalSize[i] = _childRects[i].sizeDelta;
            _initalPos[i] = _childRects[i].anchoredPosition;
        }


        _thisRect = GetComponent<RectTransform>();

        _thisRectInitalSize = _thisRect.sizeDelta;
        _thisRectCurrentSize = _thisRect.sizeDelta;
    }

    // Update is called once per frame
    void Update()
    {
        if (_thisRectCurrentSize != _thisRect.sizeDelta)
        {
            _thisRectCurrentSize = _thisRect.sizeDelta;

            Vector2 scaleFactor = _thisRectCurrentSize / _thisRectInitalSize;

            for (int i = 0; i < _childRects.Length; i++)
            {
                _childRects[i].sizeDelta = _initalSize[i] * scaleFactor;
                _childRects[i].anchoredPosition = _initalPos[i] * scaleFactor;
            }
        }
    }
}
