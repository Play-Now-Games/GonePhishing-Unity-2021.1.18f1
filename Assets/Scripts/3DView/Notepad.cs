using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notepad : Interactable
{

    [Tooltip("A reference to the player (an object with a FirstPersonController script)." +
        " If none is specified, the first object with the tag \"Player\" is used.")]
    public FirstPersonController Player;
    [Tooltip("When the camera locks onto this notpad, it will be this distance away," +
        " in the notpad's negative Z direction (opposite to the blue arrow in editor).")]
    public float CameraOffset;

    [Tooltip("List of notes to be displayed on game start.")]
    public List<string> notes;


    [Tooltip("How long, in seconds, does it take to pickup the notepad")]
    public float pickupTime = 0.8f;

    private enum NotepadState
    {
        Resting,
        Held,
        MovingToHeld,
        MovingToRest
    }

    private NotepadState _currentState = NotepadState.Resting;
    private float _movingFor = 0.0f;

    private Text _notesDisplay;


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

        // link the camera to the notpad canvas
        try
        {
            GetComponentInChildren<Canvas>().worldCamera = Player.GetComponent<Camera>();
        }
        catch
        {
            Debug.LogError("Player lacks camera component or notepad lacks Canvas.");
        }

        try
        {
            _notesDisplay = GetComponentInChildren<Text>();
        }
        catch
        {
            Debug.LogError("Notepad has no text component!");
        }
        #endregion


        UpdateDisplay();

        _restingPosition = transform.position;
        _restingRotation = transform.rotation;
        _heldPosition = _whenHeld.position;
        _heldRotation = _whenHeld.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentState == NotepadState.MovingToHeld)
        {
            _movingFor += Time.deltaTime;

            if (Vector3.Distance(transform.position, _heldPosition) < 0.001 || _movingFor >= pickupTime)
            {
                transform.SetPositionAndRotation(_heldPosition, _heldRotation);
                _movingFor = 0.0f;
                _currentState = NotepadState.Held;
            }
            else
            {

                transform.SetPositionAndRotation(Vector3.Lerp(_restingPosition, _heldPosition, _movingFor / pickupTime),
                                          Quaternion.Lerp(_restingRotation, _heldRotation, _movingFor / pickupTime));
            }
        }
        else if (_currentState == NotepadState.MovingToRest)
        {
            _movingFor += Time.deltaTime;

            if (Vector3.Distance(transform.position, _restingPosition) < 0.001 || _movingFor >= pickupTime)
            {
                transform.SetPositionAndRotation(_restingPosition, _restingRotation);
                _movingFor = 0.0f;
                _currentState = NotepadState.Resting;
            }
            else
            {

                transform.SetPositionAndRotation(Vector3.Lerp(_heldPosition, _restingPosition, _movingFor / pickupTime),
                                          Quaternion.Lerp(_heldRotation, _restingRotation, _movingFor / pickupTime));
            }
        }

        if (!Player.IsPlayerCameraLocked() && _currentState == NotepadState.Held)
        {
            Player.ReturnCameraToOriginalPositionRotation();
            _currentState = NotepadState.MovingToRest;
            _movingFor = 0.0f;
        }
    }

    public override void OnClick()
    {
        if (_currentState == NotepadState.Resting)
        {
            Player.PointCameraAt(_whenHeld, CameraOffset);
            _currentState = NotepadState.MovingToHeld;
            _movingFor = 0.0f;
        }
    }


    public void UpdateDisplay()
    {
        string newDisplay = "";

        foreach (string note in notes)
        {
            newDisplay += note + "\n";
        }

        _notesDisplay.text = newDisplay;
    }


    #region Add and Remove note functions
    public void AddNote(string note)
    {
        notes.Add(note);

        UpdateDisplay();
    }

    public void AddNoteAt(string note, int index)
    {
        if (notes.Count > index)
        {
            notes.Add(note);
        }
        else
        {
            notes.Insert(index, note);
        }
    }

    public void RemoveNote(string note)
    {
        if (notes.Contains(note))
        {
            notes.Remove(note);
        }

        UpdateDisplay();
    }

    public void RemoveNoteAt(int index)
    {
        if (notes.Count >= index || index < 0)
        {
            //no note to remove
        }
        else
        {
            notes.RemoveAt(index);
        }
    }
    #endregion
}
