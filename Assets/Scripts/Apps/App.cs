using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class App : MonoBehaviour
{

    [Tooltip("Parent Object for everything that should dissapear when the App is minimised.")]
    public GameObject content;

    private RectTransform _transform;
    private float _currentHightMaximised;
    private float _currentYPositionMaximised;

    private const float MINIMISED_HEIGHT = 45.0f;

    private bool minimised;
    private bool selected;


    // Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<RectTransform>();
        _currentHightMaximised = _transform.rect.height;
        _currentYPositionMaximised = _transform.anchoredPosition.y;
        minimised = false;
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
        if (minimised) //maximise
        {
            newRectSize.y = _currentHightMaximised;
            _transform.sizeDelta = newRectSize;

            newPosition.y = _currentYPositionMaximised;
            _transform.anchoredPosition = newPosition;

            content.SetActive(true);
            minimised = false;
        }
        else //minimise
        {
            newRectSize.y = MINIMISED_HEIGHT;
            _transform.sizeDelta = newRectSize;

            newPosition.y = _currentYPositionMaximised + ((_currentHightMaximised - MINIMISED_HEIGHT) / 2);
            _transform.anchoredPosition = newPosition;

            content.SetActive(false);
            minimised = true;
        }
    }

    public void OnClick()
    {
        selected = true;
    }

    public void OnRelease()
    {
        selected = false;
    }
}
