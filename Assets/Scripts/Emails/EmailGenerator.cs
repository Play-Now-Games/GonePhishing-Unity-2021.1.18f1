using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmailGenerator : MonoBehaviour
{
    public List<Email> outputEmails;

    public string firstName, lastName, title, senderName;

    [Tooltip("Use #firstName, #lastName, #title, and #senderName in the text as apropriate.")]
    //[TextArea]
    public string[] greetings;
    [Tooltip("Use #firstName, #lastName, #title, and #senderName in the text as apropriate.")]
    [TextArea]
    public string[] bodies;
    [Tooltip("Use #firstName, #lastName, #title, and #senderName in the text as apropriate.")]
    [TextArea]
    public string[] signoffs;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GenerateEmail();
        }
    }

    public void GenerateEmail()
    {
        Email email = ScriptableObject.CreateInstance<Email>();

        // Add text //
        //greeting
        email.text += AddNames(greetings[Random.Range(0, greetings.Length)]) + "\n\n";
        //body
        email.text += AddNames(bodies[Random.Range(0, bodies.Length)]) + "\n\n";
        //signoff
        email.text += AddNames(signoffs[Random.Range(0, signoffs.Length)]);

        outputEmails.Add(email);
    }

    private string AddNames(string s)
    {
        string output = s;

        output = output.Replace("#firstName", firstName);
        output = output.Replace("#lastName", lastName);
        output = output.Replace("#title", title);
        output = output.Replace("#senderName", senderName);

        return output;
    }
}
