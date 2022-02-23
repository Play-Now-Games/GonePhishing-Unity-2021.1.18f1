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

    private bool minimised;
    private bool selected;


    // Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<RectTransform>();
        _currentHightMaximised = _transform.rect.height;
        _currentYPositionMaximised = _transform.anchoredPosition.y;
        minimised = false;
        selected = true; //temporally true for testing
    }

    // Update is called once per frame
    void Update()
    {
        //if (selected)
        //{
        //    Vector2 offset = Vector2.zero;
        //    if (!minimised)
        //    {
        //        //offset position to hold app my the top bar even when minimised
        //        offset.y = -((_currentHightMaximised - MINIMISED_HEIGHT) / 2);
        //    }

        //    Vector2 pos;
        //    RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvas.transform as RectTransform, Input.mousePosition, parentCanvas.worldCamera, out pos);
        //    _transform.position = parentCanvas.transform.TransformPoint(pos + offset);
        //}
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
