using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppManager : MonoBehaviour
{
    [Tooltip("Parent Canvas (needed for dragging code).")]
    public Canvas parentCanvas;

    private App[] apps;

    // Start is called before the first frame update
    void Start()
    {
        apps = GetComponentsInChildren<App>();
        foreach (App app in apps)
        {
            app.parentCanvas = parentCanvas;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
