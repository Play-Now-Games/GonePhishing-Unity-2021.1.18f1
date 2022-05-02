using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Examinable : Interactable
{

    [Tooltip("A reference to the player (an object with a FirstPersonController script)." +
        " If none is specified, the first object with the tag \"Player\" is used.")]
    public FirstPersonController Player;
    [Tooltip("When the camera locks onto this object, it will be this distance away," +
        " in the notpad's negative Z direction (opposite to the blue arrow in editor).")]
    public float CameraOffset;


    [Tooltip("How long, in seconds, does it take to pickup the notepad")]
    public float pickupTime = 0.8f;

    private enum ObjectState
    {
        Resting,
        Held,
        MovingToHeld,
        MovingToRest
    }

    private ObjectState _currentState = ObjectState.Resting;
    private float _movingFor = 0.0f;



    //Resting position and location, assumed to be the same as inital position/rotation
    private Vector3 _restingPosition;
    private Quaternion _restingRotation;
    [SerializeField]
    private Transform _whenHeld;
    private Vector3 _heldPosition;
    private Quaternion _heldRotation;

    // Start is called before the first frame update
    void Start()
    {
        #region Try to find unset components
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
        #endregion

        _restingPosition = transform.position;
        _restingRotation = transform.rotation;
        _heldPosition = _whenHeld.position;
        _heldRotation = _whenHeld.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentState == ObjectState.MovingToHeld)
        {
            _movingFor += Time.deltaTime;

            if (Vector3.Distance(transform.position, _heldPosition) < 0.001 || _movingFor >= pickupTime)
            {
                transform.SetPositionAndRotation(_heldPosition, _heldRotation);
                _movingFor = 0.0f;
                _currentState = ObjectState.Held;
            }
            else
            {

                transform.SetPositionAndRotation(Vector3.Lerp(_restingPosition, _heldPosition, _movingFor / pickupTime),
                                          Quaternion.Lerp(_restingRotation, _heldRotation, _movingFor / pickupTime));
            }
        }
        else if (_currentState == ObjectState.MovingToRest)
        {
            _movingFor += Time.deltaTime;

            if (Vector3.Distance(transform.position, _restingPosition) < 0.001 || _movingFor >= pickupTime)
            {
                transform.SetPositionAndRotation(_restingPosition, _restingRotation);
                _movingFor = 0.0f;
                _currentState = ObjectState.Resting;
            }
            else
            {

                transform.SetPositionAndRotation(Vector3.Lerp(_heldPosition, _restingPosition, _movingFor / pickupTime),
                                          Quaternion.Lerp(_heldRotation, _restingRotation, _movingFor / pickupTime));
            }
        }

        if (!Player.IsPlayerCameraLocked() && _currentState == ObjectState.Held)
        {
            Player.ReturnCameraToOriginalPositionRotation();
            _currentState = ObjectState.MovingToRest;
            _movingFor = 0.0f;
        }
    }

    public override void OnClick()
    {
        if (_currentState == ObjectState.Resting)
        {
            Player.PointCameraAt(_whenHeld, CameraOffset);
            _currentState = ObjectState.MovingToHeld;
            _movingFor = 0.0f;
        }
    }
}
