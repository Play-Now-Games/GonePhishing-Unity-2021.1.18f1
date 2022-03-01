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
    public enum Type
    {
        Mouse,
        Touch
    }

    public Vector3 position;
    public ClickPhase phase;
    public Type type;
    public int touchID;
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

            Touch touch = Input.GetTouch(i);

            if (touch.phase == TouchPhase.Began)
                touchClick.phase = Click.ClickPhase.Begin;
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                touchClick.phase = Click.ClickPhase.End;
            else
                break; //Discard touches that are not begining or ending

            touchClick.type = Click.Type.Touch;
            touchClick.touchID = touch.fingerId;

            touchClick.position = touch.position;

            clicks.Add(touchClick);
        }

        // Get mouse clicks
        if (Input.GetMouseButtonDown(0))
        {
            Click mouseClick = new Click();

            mouseClick.phase = Click.ClickPhase.Begin;

            mouseClick.type = Click.Type.Mouse;

            mouseClick.position = Input.mousePosition;

            clicks.Add(mouseClick);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Click mouseClick = new Click();

            mouseClick.phase = Click.ClickPhase.End;

            mouseClick.type = Click.Type.Mouse;

            mouseClick.position = Input.mousePosition;

            clicks.Add(mouseClick);
        }

        return clicks;
    }
}
