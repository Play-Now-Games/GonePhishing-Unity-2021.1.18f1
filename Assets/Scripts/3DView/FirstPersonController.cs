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
    [Tooltip("How long, in seconds, for the camera movement into or out of a lock to last.")]
    public float CameraTransitionTime = 0.8f;

    private enum PlayerState
    {
        FreeCamera,
        CameraMovingIntoLock,
        CameraReturningFromLock,
        LockCamera,
        DisableControl
    }

    private PlayerState CurrentState = PlayerState.FreeCamera;
    private PlayerState ReturnState;

    private float CameraMovingFor = 0.0f;

    private Camera MainCamera;
    private Quaternion DefaultRotation;
    private Vector3 DefaultPosition;
    // Rotation and position when the camera last started moving
    private Quaternion LastFixedRotation;
    private Vector3 LastFixedPosition;
    // Target rotation and position
    private Quaternion TargetRotation;
    private Vector3 TargetPosition;

    private bool SwivelLeft = false;
    private bool SwivelRight = false;
    private int SwivelControlMarginPixels;
    private float CameraSwivelAngle = 0;

    private int ExitCameraLockMarginPixelsX;
    private int ExitCameraLockMarginPixelsY;

    private Interactable Selection = null;
    private bool TryingToExitCameraLock = false;

    private ClickManager clickManager;

    private Main mainScript;

    // Start is called before the first frame update
    void Start()
    {

        MainCamera = Camera.main;
        DefaultRotation = MainCamera.transform.rotation;
        DefaultPosition = MainCamera.transform.position;

        clickManager = new ClickManager();

        mainScript = GetComponent<Main>();
    }

    // Update is called once per frame
    void Update()
    {

        if(!mainScript.dayEnded)
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
                if (SwivelLeft && CameraSwivelAngle > -MaxSwivel)
                {
                    CameraSwivelAngle += -SwivelSpeed * Time.deltaTime;
                }
                else if (SwivelRight && CameraSwivelAngle < MaxSwivel)
                {
                    CameraSwivelAngle += SwivelSpeed * Time.deltaTime;
                }

                MainCamera.transform.rotation = Quaternion.AngleAxis(CameraSwivelAngle, Vector3.up) * DefaultRotation;

                // Get all beginning or ending mouse clicks or touch "clicks"
                List<Click> clicks = clickManager.GetClicks();

                for (int i = 0; i < clicks.Count; i++)
                {
                    // Click is beginning
                    if (clicks[i].phase == Click.ClickPhase.Begin)
                    {
                        RaycastHit clicked;
                        Ray mouseRay = MainCamera.ScreenPointToRay(clicks[i].position);

                        if (Physics.Raycast(mouseRay, out clicked))
                        {
                            if (clicked.transform.gameObject.GetComponent<Interactable>())
                            {
                                // Player started clicking on an interactable
                                Selection = clicked.transform.gameObject.GetComponent<Interactable>();
                            }
                        }
                    }
                    // Click is ending
                    if (clicks[i].phase == Click.ClickPhase.End)
                    {
                        RaycastHit clicked;
                        Ray mouseRay = MainCamera.ScreenPointToRay(clicks[i].position);

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

            }
            else if (CurrentState == PlayerState.LockCamera)
            {

                ExitCameraLockMarginPixelsX = Mathf.RoundToInt(Screen.width / 100.0f * ExitCameraLockMargin);
                ExitCameraLockMarginPixelsY = Mathf.RoundToInt(Screen.height / 100.0f * ExitCameraLockMargin);

                List<Click> clicks = clickManager.GetClicks();

                for (int i = 0; i < clicks.Count; i++)
                {
                    if (clicks[i].phase == Click.ClickPhase.Begin)
                    {

                        // Check if player is trying to exit camera lock
                        if (clicks[i].position.x < ExitCameraLockMarginPixelsX
                            || clicks[i].position.x > Screen.width - ExitCameraLockMarginPixelsX
                            || clicks[i].position.y < ExitCameraLockMarginPixelsY
                            || clicks[i].position.y > Screen.height - ExitCameraLockMarginPixelsY)
                        {
                            TryingToExitCameraLock = true;
                        }

                    }

                    if (clicks[i].phase == Click.ClickPhase.End)
                    {
                        if (clicks[i].position.x < ExitCameraLockMarginPixelsX
                            || clicks[i].position.x > Screen.width - ExitCameraLockMarginPixelsX
                            || clicks[i].position.y < ExitCameraLockMarginPixelsY
                            || clicks[i].position.y > Screen.height - ExitCameraLockMarginPixelsY)
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
            else if (CurrentState == PlayerState.CameraMovingIntoLock || CurrentState == PlayerState.CameraReturningFromLock)
            {
                if (Vector3.Distance(MainCamera.transform.position, TargetPosition) < 0.001 || CameraMovingFor >= CameraTransitionTime)
                {
                    MainCamera.transform.SetPositionAndRotation(TargetPosition, TargetRotation);
                    if (CurrentState == PlayerState.CameraMovingIntoLock)
                        CurrentState = PlayerState.LockCamera;
                    else
                        CurrentState = PlayerState.FreeCamera;
                }
                else
                {
                    CameraMovingFor += Time.deltaTime;

                    MainCamera.transform.SetPositionAndRotation(Vector3.Lerp(LastFixedPosition, TargetPosition, CameraMovingFor / CameraTransitionTime),
                                                                Quaternion.Lerp(LastFixedRotation, TargetRotation, CameraMovingFor / CameraTransitionTime));
                }
            }
        }
    }

    public void PointCameraAt(Transform target, float offset)
    {
        StartMovingCamera(target.position + (target.rotation * (Vector3.back * offset)), target.rotation);
        CurrentState = PlayerState.CameraMovingIntoLock;
    }

    public void ReturnCameraToOriginalPositionRotation()
    {
        StartMovingCamera(DefaultPosition, DefaultRotation);

        CurrentState = PlayerState.CameraReturningFromLock;
    }

    public bool CanPlayerInteract()
    {
        return CurrentState == PlayerState.FreeCamera;
    }

    public bool IsPlayerCameraLocked()
    {
        if (CurrentState == PlayerState.LockCamera || CurrentState == PlayerState.CameraMovingIntoLock)
        {
            return true;
        }
        return false;
    }

    private void StartMovingCamera(Vector3 targetPos, Quaternion targetRot)
    {
        LastFixedPosition = MainCamera.transform.position;
        LastFixedRotation = MainCamera.transform.rotation;

        TargetPosition = targetPos;
        TargetRotation = targetRot;

        CameraMovingFor = 0.0f;
    }

    public void DisableControl()
    {
        ReturnState = CurrentState;
        CurrentState = PlayerState.DisableControl;
    }

    public void ReturnToPreviousStateAfterControlDisabled()
    {
        CurrentState = ReturnState;
    }

}
