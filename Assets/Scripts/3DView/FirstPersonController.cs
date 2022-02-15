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

    private bool IsLookingAtComputer = false;

    private Camera MainCamera;
    private Quaternion DefaultAngle;
    private Vector3 DefaultPosition;

    private bool SwivelLeft = false;
    private bool SwivelRight = false;
    private int SwivelControlMarginPixels;

    // Start is called before the first frame update
    void Start()
    {

        MainCamera = Camera.main;
        DefaultAngle = MainCamera.transform.rotation;
        DefaultPosition = MainCamera.transform.position;

    }

    // Update is called once per frame
    void Update()
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
        if (SwivelLeft && MainCamera.transform.rotation.eulerAngles.y > DefaultAngle.eulerAngles.y - MaxSwivel)
        {
            MainCamera.transform.Rotate(0, -SwivelSpeed * Time.deltaTime, 0);
        }
        else if (SwivelRight && MainCamera.transform.rotation.eulerAngles.y < DefaultAngle.eulerAngles.y + MaxSwivel)
        {
            MainCamera.transform.Rotate(0, SwivelSpeed * Time.deltaTime, 0);
        }

    }
}
