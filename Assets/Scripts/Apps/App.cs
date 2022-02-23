using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class App : MonoBehaviour
{

    [Tooltip("Parent Object for everything that should dissapear when the App is minimised.")]
    public GameObject content;
    [HideInInspector] //set by AppManager
    public Canvas parentCanvas;

    private RectTransform _transform;
    private float _currentHightMaximised;
    private float _currentYPositionMaximised;

    private const float MINIMISED_HEIGHT = 45.0f;

    private bool _minimised;
    private bool _selected;

    private Vector2 _mouseOffsetOnClick;

    // Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<RectTransform>();
        _currentHightMaximised = _transform.rect.height;
        _currentYPositionMaximised = _transform.anchoredPosition.y;
        _minimised = false;
        _selected = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_selected)
        {
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvas.transform as RectTransform, Input.mousePosition, parentCanvas.worldCamera, out pos);
            _transform.position = parentCanvas.transform.TransformPoint(pos + _mouseOffsetOnClick);
        }
    }

    public void ToggleMinimised()
    {

        Vector2 newRectSize;
        newRectSize.x = _transform.rect.width;
        Vector2 newPosition;
        newPosition.x = _transform.anchoredPosition.x;

        //if minimised
        if (_minimised) //maximise
        {
            newRectSize.y = _currentHightMaximised;
            _transform.sizeDelta = newRectSize;

            newPosition.y = _currentYPositionMaximised;
            _transform.anchoredPosition = newPosition;

            content.SetActive(true);
            _minimised = false;
        }
        else //minimise
        {
            newRectSize.y = MINIMISED_HEIGHT;
            _transform.sizeDelta = newRectSize;

            newPosition.y = _currentYPositionMaximised + ((_currentHightMaximised - MINIMISED_HEIGHT) / 2);
            _transform.anchoredPosition = newPosition;

            content.SetActive(false);
            _minimised = true;
        }
    }

    public void OnClick()
    {
        _selected = true;

        //Find mouseOffset so we can maintain it while draging
        Vector2 mousePosOnClick;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvas.transform as RectTransform, Input.mousePosition, parentCanvas.worldCamera, out mousePosOnClick);
        _mouseOffsetOnClick = _transform.position - parentCanvas.transform.TransformPoint(mousePosOnClick);
    }

    public void OnRelease()
    {
        _selected = false;
    }
}
