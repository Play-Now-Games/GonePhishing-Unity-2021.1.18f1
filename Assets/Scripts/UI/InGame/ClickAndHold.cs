using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClickAndHold : MonoBehaviour
{

    [Tooltip("How long to click and hold before OnClickAndHold fires.")]
    public float HoldTime;
    
    public UnityEvent OnClickAndHold;

    private ClickManager _clickManager;
    private float _timer;

    // Start is called before the first frame update
    void Start()
    {
        _clickManager = new ClickManager();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
