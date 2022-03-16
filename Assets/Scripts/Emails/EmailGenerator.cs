using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmailGenerator : MonoBehaviour
{
    public EmailGenerationTestInterface emailInterface;

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
            emailInterface.AddEmail(GenerateEmail());
        }
    }

    public Email_Scriptable GenerateEmail(bool isPhishing = false)
    {
        Email_Scriptable email = ScriptableObject.CreateInstance<Email_Scriptable>();

        email.sender = senderName;

        email.tittle = "Placeholder Title";

        // Add text //
        //greeting
        email.greetings = AddNames(greetings[Random.Range(0, greetings.Length)]);
        //body
        email.content = AddNames(bodies[Random.Range(0, bodies.Length)]);
        //signoff
        email.bye = AddNames(signoffs[Random.Range(0, signoffs.Length)]);

        email.isPhishing = isPhishing;

        return email;
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
