using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : Interactable
{

    [Tooltip("A reference to the player (an object with a FirstPersonController script)." +
        " If none is specified, the first object with the tag \"Player\" is used.")]
    public FirstPersonController Player;
    [Tooltip("When the camera locks onto this computer, it will be this distance away," +
        " in the computer's negative Z direction (opposite to the blue arrow in editor).")]
    public float CameraOffset;

    private CanvasGroup ComputerGui;


    private DayTimer _timer;
    private bool _clickedBefore = false;

    // Start is called before the first frame update
    void Start()
    {

        if (Player == null)
        {
            try
            {
                Player = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
            }
            catch
            {
                Debug.LogError("No player object found!");
            }
        }

        try
        {
            _timer = Player.gameObject.GetComponent<DayTimer>();
        }
        catch
        {
            Debug.LogError("Could not find timer script on player object!");
        }

        try
        {
            ComputerGui = gameObject.GetComponentInChildren<CanvasGroup>();
        }
        catch
        {
            Debug.LogError("Could not find computer GUI canvas group");
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Player.CanPlayerInteract())
        {
            LockGui();
        }
    }

    public override void OnClick()
    {
        Player.PointCameraAt(transform, CameraOffset);
        UnlockGui();
    }

    public void UnlockGui()
    {
        ComputerGui.interactable = true;
        if (!_clickedBefore)
        {
            _clickedBefore = true;
            _timer.StartTiming();
        }
    }

    public void LockGui()
    {
        ComputerGui.interactable = false;
    }

}
