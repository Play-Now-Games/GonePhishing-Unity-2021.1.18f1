using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class App : MonoBehaviour
{

    [Tooltip("Parent Object for everything that should dissapear when the App is minimised.")]
    public GameObject content;

    private RectTransform _transform;
    private float _currentHightMaximised;
    private float _currentYPositionMaximised;

    private const float MINIMISED_HEIGHT = 45.0f;


    // Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<RectTransform>();
        _currentHightMaximised = _transform.rect.height;
        _currentYPositionMaximised = _transform.anchoredPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ToggleMinimised()
    {

        Vector2 newRectSize;
        newRectSize.x = _transform.rect.width;
        Vector2 newPosition;
        newPosition.x = _transform.anchoredPosition.x;

        //if minimised
        if (_transform.rect.height == MINIMISED_HEIGHT) //maximise
        {
            newRectSize.y = _currentHightMaximised;
            _transform.sizeDelta = newRectSize;

            newPosition.y = _currentYPositionMaximised;
            _transform.anchoredPosition = newPosition;

            content.SetActive(true);
        }
        else //minimise
        {
            newRectSize.y = MINIMISED_HEIGHT;
            _transform.sizeDelta = newRectSize;

            newPosition.y = _currentYPositionMaximised + ((_currentHightMaximised - MINIMISED_HEIGHT) / 2);
            _transform.anchoredPosition = newPosition;

            content.SetActive(false);
        }
    }
}
