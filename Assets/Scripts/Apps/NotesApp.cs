using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotesApp : App
{
    public List<string> notes;

    [SerializeField]
    private Text notesDisplay;
    
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();


        UpdateDisplay();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }

    public void UpdateDisplay()
    {
        string newDisplay = "";

        foreach (string note in notes)
        {
            newDisplay += note + "\n";
        }

        notesDisplay.text = newDisplay;
    }

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
}
