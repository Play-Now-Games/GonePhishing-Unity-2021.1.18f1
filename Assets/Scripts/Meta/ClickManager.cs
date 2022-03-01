using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Click
{
    public enum ClickPhase
    {
        Begin,
        End
    }
    public Vector3 position;
    public ClickPhase phase;
}

public class ClickManager
{

    public List<Click> GetClicks()
    {
        List<Click> clicks = new List<Click>();

        // Get touch clicks
        for (int i = 0; i < Input.touchCount; i++)
        {
            Click touchClick = new Click();
            if (Input.GetTouch(i).phase == TouchPhase.Began)
                touchClick.phase = Click.ClickPhase.Begin;
            else if (Input.GetTouch(i).phase == TouchPhase.Ended)
                touchClick.phase = Click.ClickPhase.End;
            touchClick.position = Input.GetTouch(i).position;
            clicks.Add(touchClick);
        }

        // Get mouse clicks
        if (Input.GetMouseButtonDown(0))
        {
            Click mouseClick = new Click();
            mouseClick.phase = Click.ClickPhase.Begin;
            mouseClick.position = Input.mousePosition;
            clicks.Add(mouseClick);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Click mouseClick = new Click();
            mouseClick.phase = Click.ClickPhase.End;
            mouseClick.position = Input.mousePosition;
            clicks.Add(mouseClick);
        }

        return clicks;
    }
}
