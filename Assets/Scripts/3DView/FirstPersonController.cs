using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{

    [Tooltip("Max amount the player can turn left or right, in degrees, during free camera mode.")]
    public float MaxSwivel = 30.0f;
    [Tooltip("How close to the edge of the screen the player needs to mouseover/tap" +
        " to start turning, in percentage of screen width, during free camera mode.")]
    public float SwivelControlMargin = 10.0f;
    [Tooltip("How fast the player turns, in degrees per second, during free camera mode.")]
    public float SwivelSpeed;
    [Tooltip("How close to the edge of the screen the player needs to click/tap" +
        " to exit a camera lock, in percentage of screen width or height, during locked camera mode.")]
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

    private Interactable Selection = null;
    private bool TryingToExitCameraLock = false;

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

            // Begin a click
            if (Input.GetMouseButtonDown(0))
            {

                RaycastHit clicked;
                Ray mouseRay = MainCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(mouseRay, out clicked))
                {
                    if (clicked.transform.gameObject.GetComponent<Interactable>())
                    {
                        // Player started clicking on an interactable
                        Selection = clicked.transform.gameObject.GetComponent<Interactable>();
                    }
                }
            }
            // End a click
            if (Input.GetMouseButtonUp(0))
            {
                RaycastHit clicked;
                Ray mouseRay = MainCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(mouseRay, out clicked))
                {
                    if (Selection != null && clicked.transform.gameObject.GetComponent<Interactable>() == Selection)
                    {
                        // Player clicked fully on a specific interactable
                        Selection.OnClick();
                        Selection = null;
                    }
                }

                Selection = null;
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
                    TryingToExitCameraLock = true;
                }

            }

            if (Input.GetMouseButtonUp(0))
            {
                if (Input.mousePosition.x < ExitCameraLockMarginPixelsX
                    || Input.mousePosition.x > Screen.width - ExitCameraLockMarginPixelsX
                    || Input.mousePosition.y < ExitCameraLockMarginPixelsY
                    || Input.mousePosition.y > Screen.height - ExitCameraLockMarginPixelsY)
                {
                    if (TryingToExitCameraLock)
                    {
                        ReturnCameraToOriginalPositionRotation();
                    }
                }
                TryingToExitCameraLock = false;
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
