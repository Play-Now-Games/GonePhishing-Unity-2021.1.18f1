using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : Interactable
{

    public FirstPersonController Player;
    public float CameraOffset;

    private CanvasGroup ComputerGui;

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
    }

    public void LockGui()
    {
        ComputerGui.interactable = false;
    }

}
