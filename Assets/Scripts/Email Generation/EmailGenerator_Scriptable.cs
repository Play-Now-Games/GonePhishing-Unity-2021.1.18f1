using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Generator", menuName = "ScriptableObjects/EmailGenerator")]
public class EmailGenerator_Scriptable : ScriptableObject
{
    public string firstName, lastName, title, senderName;
    public string[] faintlyFakeFirstNames, faintlyFakeLastNames, faintlyFakeTitles, faintlyFakeSenderNames;
    public string[] fakeFirstNames, fakeLastNames, fakeTitles, fakeSenderNames;

    private string _firstName, _lastName, _title, _senderName;

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
    [TextArea]
    public string[] faintlyFakeBodies;
    [TextArea]
    public string[] fakeBodies;
    [TextArea]
    public string[] veryFakeBodies;

    [Space(10)]

    [Tooltip("Use #firstName, #lastName, #title, and #senderName in the text as apropriate.")]
    [TextArea]
    public string[] signoffs;
    [TextArea]
    public string[] fakeSignoffs;
    [TextArea]
    public string[] veryFakeSignoffs;

    [Space(15)]

    [SerializeField]
    [Tooltip("Chance of slightly fake components showing up in Hard Phishing Emails")]
    [Range(0.0f, 1.0f)]
    private float _hardPhishingFaintlyFakeChance = 0.1f;

    [SerializeField]
    [Tooltip("Chance of real-appearing components showing up in Medium Phishing Emails")]
    [Range(0.0f, 1.0f)]
    private float _mediumPhishingRealChance = 0.1f;

    [SerializeField]
    [Tooltip("Chance of very fake components showing up in Medium Phishing Emails")]
    [Range(0.0f, 1.0f)]
    private float _mediumPhishingVeryFakeChance = 0.1f;

    [SerializeField]
    [Tooltip("Chance of only slightly fake components showing up in Easy Phishing Emails")]
    [Range(0.0f, 1.0f)]
    private float _easyPhishingFaintlyFakeChance = 0.2f;

    public Email_Scriptable GenerateEmail(bool isPhishing, int phishingDifficulty)
    {
        Email_Scriptable email = ScriptableObject.CreateInstance<Email_Scriptable>();

        SetNames(isPhishing, phishingDifficulty);

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

    private void SetNames (bool isPhishing, int phishingDifficulty)
    {
        if (!isPhishing)
        {
            SetNamesReal();
        }
        else
        {
            switch (phishingDifficulty)
            {
                case 1:
                    SetNamesEasy();
                    break;
                case 2:
                    SetNamesMedium();
                    break;
                case 3:
                    SetNamesHard();
                    break;
                default:
                    Debug.LogError("Invalid phishing difficulty: " + phishingDifficulty);
                    break;
            }

        }
    }

    private void SetNamesReal()
    {
        _firstName = firstName;
        _lastName = lastName;
        _title = title;
        _senderName = senderName;
    }
    private void SetNamesEasy()
    {
        #region Set private name varibles to a fake option- defults to less fake option if none avalible
        if (fakeFirstNames.Length > 0)
        {
            _firstName = fakeFirstNames[Random.Range(0, fakeFirstNames.Length)];
        }
        else
        {
            if (faintlyFakeFirstNames.Length > 0)
            {
                _firstName = faintlyFakeFirstNames[Random.Range(0, faintlyFakeFirstNames.Length)];
            }
            else
            {
                _firstName = firstName;
            }
        }

        if (fakeLastNames.Length > 0)
        {
            _lastName = fakeLastNames[Random.Range(0, fakeLastNames.Length)];
        }
        else
        {
            if (faintlyFakeLastNames.Length > 0)
            {
                _lastName = faintlyFakeLastNames[Random.Range(0, faintlyFakeLastNames.Length)];
            }
            else
            {
                _lastName = lastName;
            }
        }

        if (fakeTitles.Length > 0)
        {
            _title = fakeTitles[Random.Range(0, fakeTitles.Length)];
        }
        else
        {
            if (faintlyFakeTitles.Length > 0)
            {
                _title = faintlyFakeTitles[Random.Range(0, faintlyFakeTitles.Length)];
            }
            else
            {
                _title = title;
            }
        }

        if (fakeSenderNames.Length > 0)
        {
            _senderName = fakeSenderNames[Random.Range(0, fakeSenderNames.Length)];
        }
        else
        {
            if (faintlyFakeSenderNames.Length > 0)
            {
                _senderName = faintlyFakeSenderNames[Random.Range(0, faintlyFakeSenderNames.Length)];
            }
            else
            {
                _senderName = senderName;
            }
        }
        #endregion
    }
    private void SetNamesMedium()
    {
        #region Set private name varibles to a faintly fake option- defults to less fake option if none avalible
        if (faintlyFakeFirstNames.Length > 0)
        {
            _firstName = faintlyFakeFirstNames[Random.Range(0, faintlyFakeFirstNames.Length)];
        }
        else
        {
            _firstName = firstName;
        }

        if (faintlyFakeLastNames.Length > 0)
        {
            _lastName = faintlyFakeLastNames[Random.Range(0, faintlyFakeLastNames.Length)];
        }
        else
        {
            _lastName = lastName;
        }

        if (faintlyFakeTitles.Length > 0)
        {
            _title = faintlyFakeTitles[Random.Range(0, faintlyFakeTitles.Length)];
        }
        else
        {
            _title = title;
        }

        if (faintlyFakeSenderNames.Length > 0)
        {
            _senderName = faintlyFakeSenderNames[Random.Range(0, faintlyFakeSenderNames.Length)];
        }
        else
        {
            _senderName = senderName;
        }
        #endregion
    }
    private void SetNamesHard()
    {
        _firstName = firstName;
        _lastName = lastName;
        _title = title;
        _senderName = senderName;
    }

    private void GenerateReal(ref Email_Scriptable email)
    {
        email.logo = logo;

        email.sender = _senderName;

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

        email.sender = _senderName;

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

        email.sender = _senderName;

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

        email.sender = _senderName;

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

        output = output.Replace("#firstName", _firstName);
        output = output.Replace("#lastName", _lastName);
        output = output.Replace("#title", _title);
        output = output.Replace("#senderName", _senderName);

        return output;
    }
}
