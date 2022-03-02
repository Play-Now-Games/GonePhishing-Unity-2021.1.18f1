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

    private ClickManager clickManager;

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

        clickManager = new ClickManager();
    }

    // Update is called once per frame
    void Update()
    {
        if (_parentCanvasGroup.interactable)
        {

            // Get all beginning or ending mouse clicks or touch "clicks"
            List<Click> clicks = clickManager.GetClicks();

            for (int i = 0; i < clicks.Count; i++)
            {

                //Set up the new Pointer Event
                _PointerEventData = new PointerEventData(_EventSystem);
                //Set the Pointer Event Position to that of the mouse position
                _PointerEventData.position = clicks[i].position;

                //Create a list of Raycast Results
                List<RaycastResult> results = new List<RaycastResult>();

                //Raycast using the Graphics Raycaster and mouse click position
                _Raycaster.Raycast(_PointerEventData, results);

                // Click is beginning
                if (clicks[i].phase == Click.ClickPhase.Begin)
                {
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
                            //set sibling index to place this app at the top of the sorting order
                            result.gameObject.transform.SetSiblingIndex(apps.Length);

                            app.OnClick(clicks[i]);

                            //only select one app at a time
                            break;
                        }

                    }
                }
                // Click is ending
                if (clicks[i].phase == Click.ClickPhase.End)
                {
                    foreach (App app in apps)
                    {
                        app.OnRelease(clicks[i]);
                    }
                }
            }

        }

    }
}
