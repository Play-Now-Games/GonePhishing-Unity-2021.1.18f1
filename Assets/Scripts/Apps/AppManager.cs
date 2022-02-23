using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AppManager : MonoBehaviour
{
    private Canvas _parentCanvas;
    private CanvasGroup _parentCanvasGroup;

    private GraphicRaycaster _Raycaster;
    private PointerEventData _PointerEventData;
    private EventSystem _EventSystem;

    private App[] apps;

    // Start is called before the first frame update
    void Start()
    {
        _parentCanvas = GetComponentInParent<Canvas>();
        _parentCanvasGroup = GetComponentInParent<CanvasGroup>();

        //Fetch the Raycaster from the Canvas
        _Raycaster = GetComponentInParent<GraphicRaycaster>();
        //Fetch the Event System from the Scene
        _EventSystem = GetComponentInParent<EventSystem>();


        //get apps and set their parent canvas components
        apps = GetComponentsInChildren<App>();
        foreach (App app in apps)
        {
            app.parentCanvas = _parentCanvas;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_parentCanvasGroup.interactable)
        {
            //Set up the new Pointer Event
            _PointerEventData = new PointerEventData(_EventSystem);
            //Set the Pointer Event Position to that of the mouse position
            _PointerEventData.position = Input.mousePosition;

            //Create a list of Raycast Results
            List<RaycastResult> results = new List<RaycastResult>();

            //Raycast using the Graphics Raycaster and mouse click position
            _Raycaster.Raycast(_PointerEventData, results);


            if (Input.GetMouseButtonDown(0))
            {

                //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
                foreach (RaycastResult result in results)
                {
                    if (result.gameObject.GetComponent<Button>())
                    {
                        //buttons take priority
                        break;
                    }

                    App app;
                    if (app = result.gameObject.GetComponent<App>())
                    {
                        app.OnClick();
                    }

                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
                foreach (RaycastResult result in results)
                {
                    App app;
                    if (app = result.gameObject.GetComponent<App>())
                    {
                        app.OnRelease();
                    }

                }
            }
        }
    }
}
