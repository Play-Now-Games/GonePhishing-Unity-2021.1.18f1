using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Generator", menuName = "ScriptableObjects/EmailGenerator")]
public class EmailGenerator_Scriptable : ScriptableObject
{
    public string firstName, lastName, title, senderName;

    [Space(10)]

    public Sprite logo;
    public Sprite fakeLogo;
    public Sprite veryFakeLogo;

    [Space(10)]

    [Tooltip("Use #firstName, #lastName, #title, and #senderName in the text as apropriate.")]
    public string[] greetings;
    public string[] fakeGreetings;
    public string[] veryFakeGreetings;

    [Space(10)]

    [Tooltip("Use #firstName, #lastName, #title, and #senderName in the text as apropriate.")]
    [TextArea]
    public string[] bodies;
    public string[] faintlyFakeBodies;
    public string[] fakeBodies;
    public string[] veryFakeBodies;

    [Space(10)]

    [Tooltip("Use #firstName, #lastName, #title, and #senderName in the text as apropriate.")]
    [TextArea]
    public string[] signoffs;
    public string[] fakeSignoffs;
    public string[] veryFakeSignoffs;

    public Email_Scriptable GenerateEmail(bool isPhishing, int phishingDifficulty)
    {
        Email_Scriptable email = ScriptableObject.CreateInstance<Email_Scriptable>();

        if (!isPhishing)
        {
            GenerateReal(ref email);
        }
        else
        {
            switch (phishingDifficulty)
            {
                case 1:
                    GenerateFakeEasy(ref email);
                    break;
                case 2:
                    GenerateFakeMedium(ref email);
                    break;
                case 3:
                    GenerateFakeHard(ref email);
                    break;
                default:
                    Debug.LogError("Invalid phishing difficulty: " + phishingDifficulty);
                    break;
            }

        }

        return email;
    }

    private void GenerateReal(ref Email_Scriptable email)
    {
        email.logo = logo;

        email.sender = senderName;

        email.tittle = "Placeholder Title";

        // Add text //
        //greeting
        email.greetings = AddNames(greetings[Random.Range(0, greetings.Length)]);
        //body
        email.content = AddNames(bodies[Random.Range(0, bodies.Length)]);
        //signoff
        email.bye = AddNames(signoffs[Random.Range(0, signoffs.Length)]);

        email.isPhishing = false;
    }

    private void GenerateFakeEasy(ref Email_Scriptable email)
    {
        email.logo = veryFakeLogo;

        email.sender = senderName;

        email.tittle = "Placeholder Title";

        // Add text //
        //greeting
        email.greetings = AddNames(veryFakeGreetings[Random.Range(0, veryFakeGreetings.Length)]);
        //body
        email.content = AddNames(veryFakeBodies[Random.Range(0, veryFakeBodies.Length)]);
        //signoff
        email.bye = AddNames(veryFakeSignoffs[Random.Range(0, veryFakeSignoffs.Length)]);

        email.isPhishing = true;
    }

    private void GenerateFakeMedium(ref Email_Scriptable email)
    {
        email.logo = fakeLogo;

        email.sender = senderName;

        email.tittle = "Placeholder Title";

        // Add text //
        //greeting
        email.greetings = AddNames(fakeGreetings[Random.Range(0, fakeGreetings.Length)]);
        //body
        email.content = AddNames(fakeBodies[Random.Range(0, fakeBodies.Length)]);
        //signoff
        email.bye = AddNames(fakeSignoffs[Random.Range(0, fakeSignoffs.Length)]);

        email.isPhishing = true;
    }

    private void GenerateFakeHard(ref Email_Scriptable email)
    {
        email.logo = logo;

        email.sender = senderName;

        email.tittle = "Placeholder Title";

        // Add text //
        //greeting
        email.greetings = AddNames(greetings[Random.Range(0, greetings.Length)]);
        //body
        email.content = AddNames(faintlyFakeBodies[Random.Range(0, faintlyFakeBodies.Length)]);
        //signoff
        email.bye = AddNames(signoffs[Random.Range(0, signoffs.Length)]);

        email.isPhishing = true;
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
