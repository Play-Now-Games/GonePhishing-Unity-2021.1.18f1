using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{

    // Max amount the player can look left and right, in degrees.
    public float MaxSwivel = 30.0f;
    // How close to the edge of the screen the player needs to mouseover/tap to start turning,
    // in % of screen width.
    public float SwivelControlMargin = 10.0f;
    // How fast the player turns, in degrees per second.
    public float SwivelSpeed;

    // How close to the edge of the screen the player needs to click/tap to exit a camera lock,
    // in % of screen width/height
    public float ExitCameraLockMargin = 10.0f;

    private enum PlayerState
    {
        FreeCamera,
        LockCamera
    }

    private PlayerState CurrentState = PlayerState.FreeCamera;

    private Camera MainCamera;
    private Quaternion DefaultRotation;
    private Vector3 DefaultPosition;

    private bool SwivelLeft = false;
    private bool SwivelRight = false;
    private int SwivelControlMarginPixels;

    private int ExitCameraLockMarginPixelsX;
    private int ExitCameraLockMarginPixelsY;

    // Start is called before the first frame update
    void Start()
    {

        MainCamera = Camera.main;
        DefaultRotation = MainCamera.transform.rotation;
        DefaultPosition = MainCamera.transform.position;

    }

    // Update is called once per frame
    void Update()
    {

        if (CurrentState == PlayerState.FreeCamera)
        {

            // Update the swivel control margin to the current screen size, in case screen size has changed
            SwivelControlMarginPixels = Mathf.RoundToInt(Screen.width / 100.0f * SwivelControlMargin);

            // Check if player wants to swivel
            if (Input.mousePosition.x < SwivelControlMarginPixels)
            {
                SwivelLeft = true;
                SwivelRight = false;
            }
            else if (Input.mousePosition.x > Screen.width - SwivelControlMarginPixels)
            {
                SwivelRight = true;
                SwivelLeft = false;
            }
            else
            {
                SwivelLeft = false;
                SwivelRight = false;
            }

            // Swivel if player wants to and is within swivel limits
            if (SwivelLeft && MainCamera.transform.rotation.eulerAngles.y > DefaultRotation.eulerAngles.y - MaxSwivel)
            {
                MainCamera.transform.Rotate(0, -SwivelSpeed * Time.deltaTime, 0);
            }
            else if (SwivelRight && MainCamera.transform.rotation.eulerAngles.y < DefaultRotation.eulerAngles.y + MaxSwivel)
            {
                MainCamera.transform.Rotate(0, SwivelSpeed * Time.deltaTime, 0);
            }

            // Check if player clicked an interactable
            if (Input.GetMouseButtonDown(0))
            {

                RaycastHit clicked;
                Ray mouseRay = MainCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(mouseRay, out clicked))
                {
                    if (clicked.transform.gameObject.GetComponent<Interactable>())
                    {
                        // Player clicked on an interactable
                        clicked.transform.gameObject.GetComponent<Interactable>().OnClick();
                    }
                }
            }

        }
        else
        {

            ExitCameraLockMarginPixelsX = Mathf.RoundToInt(Screen.width / 100.0f * ExitCameraLockMargin);
            ExitCameraLockMarginPixelsY = Mathf.RoundToInt(Screen.height / 100.0f * ExitCameraLockMargin);

            if (Input.GetMouseButtonDown(0))
            {
                
                // Check if player is trying to exit camera lock
                if (Input.mousePosition.x < ExitCameraLockMarginPixelsX
                    || Input.mousePosition.x > Screen.width - ExitCameraLockMarginPixelsX
                    || Input.mousePosition.y < ExitCameraLockMarginPixelsY
                    || Input.mousePosition.y > Screen.height - ExitCameraLockMarginPixelsY)
                {
                    // Exit camera lock
                    ReturnCameraToOriginalPositionRotation();
                }

            }

        }

    }

    public void PointCameraAt(Transform target, float offset)
    {
        MainCamera.transform.position = target.position;
        MainCamera.transform.rotation = target.rotation;
        MainCamera.transform.Translate(0, 0, -offset, Space.Self);

        CurrentState = PlayerState.LockCamera;
    }

    public void ReturnCameraToOriginalPositionRotation()
    {
        MainCamera.transform.position = DefaultPosition;
        MainCamera.transform.rotation = DefaultRotation;

        CurrentState = PlayerState.FreeCamera;
    }

    public bool CanPlayerInteract()
    {
        return CurrentState == PlayerState.FreeCamera;
    }

}
