using System.Collections;
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
    public List<string> Notes;



    private Text _notesDisplay;

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
            _notesDisplay = GetComponentInChildren<Text>();
        }
        catch
        {
            Debug.LogError("Notepad has no text component!");
        }


        UpdateDisplay();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void OnClick()
    {
        Player.PointCameraAt(transform, CameraOffset);
    }

    public void UpdateDisplay()
    {
        string newDisplay = "";

        foreach (string note in Notes)
        {
            newDisplay += note + "\n";
        }

        _notesDisplay.text = newDisplay;
    }


    public void AddNote(string note)
    {
        Notes.Add(note);

        UpdateDisplay();
    }

    public void AddNoteAt(string note, int index)
    {
        if (Notes.Count > index)
        {
            Notes.Add(note);
        }
        else
        {
            Notes.Insert(index, note);
        }
    }

    public void RemoveNote(string note)
    {
        if (Notes.Contains(note))
        {
            Notes.Remove(note);
        }

        UpdateDisplay();
    }

    public void RemoveNoteAt(int index)
    {
        if (Notes.Count >= index || index < 0)
        {
            //no note to remove
        }
        else
        {
            Notes.RemoveAt(index);
        }
    }
}
