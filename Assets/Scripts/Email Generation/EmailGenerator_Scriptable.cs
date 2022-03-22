using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Generator", menuName = "ScriptableObjects/EmailGenerator")]
public class EmailGenerator_Scriptable : ScriptableObject
{
    public string firstName, lastName, title, senderName, senderAddress;
    public string[] faintlyFakeFirstNames, faintlyFakeLastNames, faintlyFakeTitles, faintlyFakeSenderNames, faintlyFakeSenderAddress;
    public string[] fakeFirstNames, fakeLastNames, fakeTitles, fakeSenderNames, fakeSenderAddress;

    private string _firstName, _lastName, _title, _senderName, _senderAddress;

    [Space(10)]

    public Sprite logo;
    public Sprite fakeLogo;
    public Sprite veryFakeLogo;

    [Space(10)]

    [Tooltip("Use #firstName, #lastName, #title, and #senderName in the text as apropriate.")]
    public string[] greetings;
    public string[] fakeGreetings;
    public string[] veryFakeGreetings;

    [System.Serializable]
    public class EmailBodyAndTitle
    {
        public string title;
        [TextArea]
        public string body;
    }

    [Space(10)]

    public bool reuseBodies = false;
    [Tooltip("Use #firstName, #lastName, #title, and #senderName in the text as apropriate.")]

    public List<EmailBodyAndTitle> bodies;
    private List<EmailBodyAndTitle> _unusedBodies;

    public List<EmailBodyAndTitle> faintlyFakeBodies;
    private List<EmailBodyAndTitle> _unusedFaintlyFakeBodies;

    public List<EmailBodyAndTitle> fakeBodies;
    private List<EmailBodyAndTitle> _unusedFakeBodies;

    public List<EmailBodyAndTitle> veryFakeBodies;
    private List<EmailBodyAndTitle> _unusedVeryFakeBodies;

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
    [Range(0.0f, 0.5f)]
    private float _hardPhishingFaintlyFakeChance = 0.1f;

    [SerializeField]
    [Tooltip("Chance of real-appearing components showing up in Medium Phishing Emails")]
    [Range(0.0f, 0.5f)]
    private float _mediumPhishingRealChance = 0.1f;

    [SerializeField]
    [Tooltip("Chance of very fake components showing up in Medium Phishing Emails")]
    [Range(0.0f, 0.5f)]
    private float _mediumPhishingVeryFakeChance = 0.1f;

    [SerializeField]
    [Tooltip("Chance of only slightly fake components showing up in Easy Phishing Emails")]
    [Range(0.0f, 0.5f)]
    private float _easyPhishingFaintlyFakeChance = 0.2f;

    public Email_Scriptable GenerateEmail(bool isPhishing, int phishingDifficulty)
    {
        Email_Scriptable email = CreateInstance<Email_Scriptable>();

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

    public void ResetBodies()
    {
        _unusedBodies = new List<EmailBodyAndTitle>(bodies);
        _unusedFaintlyFakeBodies = new List<EmailBodyAndTitle>(faintlyFakeBodies);
        _unusedFakeBodies = new List<EmailBodyAndTitle>(fakeBodies);
        _unusedVeryFakeBodies = new List<EmailBodyAndTitle>(veryFakeBodies);
    }

    public bool CanGenerate(bool isPhishing, int phishingDifficulty)
    {
        if (reuseBodies)
        {
            return true;
        }

        if (!isPhishing)
        {
            if (_unusedBodies.Count > 0)
            {
                return true;
            }    
        }
        else
        {
            switch (phishingDifficulty)
            {
                case 1:
                    if (_unusedVeryFakeBodies.Count > 0)
                    {
                        return true;
                    }
                    break;
                case 2:
                    if (_unusedFakeBodies.Count > 0)
                    {
                        return true;
                    }
                    break;
                case 3:
                    if (_unusedFaintlyFakeBodies.Count > 0)
                    {
                        return true;
                    }
                    break;
                default:
                    Debug.LogError("Invalid phishing difficulty: " + phishingDifficulty);
                    break;
            }

        }

        return false;
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
        _senderAddress = senderAddress;
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

        if (fakeSenderAddress.Length > 0)
        {
            _senderAddress = fakeSenderAddress[Random.Range(0, fakeSenderAddress.Length)];
        }
        else
        {
            if (faintlyFakeSenderAddress.Length > 0)
            {
                _senderAddress = faintlyFakeSenderAddress[Random.Range(0, faintlyFakeSenderAddress.Length)];
            }
            else
            {
                _senderAddress = senderAddress;
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

        if (faintlyFakeSenderAddress.Length > 0)
        {
            _senderAddress = faintlyFakeSenderAddress[Random.Range(0, faintlyFakeSenderAddress.Length)];
        }
        else
        {
            _senderAddress = senderAddress;
        }
        #endregion
    }
    private void SetNamesHard()
    {
        _firstName = firstName;
        _lastName = lastName;
        _title = title;
        _senderName = senderName;
        _senderAddress = senderAddress;
    }

    private void GenerateReal(ref Email_Scriptable email)
    {
        email.sender = _senderName;

        email.senderAdress = _senderAddress;
         
        email.tittle = "Placeholder Title";


        if (reuseBodies)
        {
            int index = Random.Range(0, bodies.Count);
            email.content = AddNames(bodies[index].body);
            email.tittle = bodies[index].title;
        }
        else if (_unusedBodies.Count > 0)
        {
            int index = Random.Range(0, _unusedBodies.Count);
            email.content = AddNames(_unusedBodies[index].body);
            email.tittle = _unusedBodies[index].title;

            _unusedBodies.RemoveAt(index);
        }
        else
        {
            Debug.Log("Trying to generate an email without a suitable body.");
        }

        

        email.greetings = AddNames(greetings[Random.Range(0, greetings.Length)]);

        email.bye = AddNames(signoffs[Random.Range(0, signoffs.Length)]);

        email.logo = logo;



        email.isPhishing = false;
    }
    private void GenerateFakeEasy(ref Email_Scriptable email)
    {
        email.sender = _senderName;

        email.senderAdress = _senderAddress;

        email.tittle = "Placeholder Title";


        if (reuseBodies)
        {
            int index = Random.Range(0, veryFakeBodies.Count);
            email.content = AddNames(veryFakeBodies[Random.Range(0, veryFakeBodies.Count)].body);
            email.tittle = veryFakeBodies[index].title;
        }
        else if (_unusedVeryFakeBodies.Count > 0)
        {
            int index = Random.Range(0, _unusedVeryFakeBodies.Count);
            email.content = AddNames(_unusedVeryFakeBodies[index].body);
            email.tittle = _unusedVeryFakeBodies[index].title;

            _unusedVeryFakeBodies.RemoveAt(index);
        }
        else
        {
            Debug.Log("Trying to generate an email without a suitable body.");
        }



        if (Random.Range(0, 1.0f) < _easyPhishingFaintlyFakeChance)
        {
            email.greetings = AddNames(fakeGreetings[Random.Range(0, fakeGreetings.Length)]);
        }
        else
        {
            email.greetings = AddNames(veryFakeGreetings[Random.Range(0, veryFakeGreetings.Length)]);
        }

        if (Random.Range(0, 1.0f) < _easyPhishingFaintlyFakeChance)
        {
            email.bye = AddNames(fakeSignoffs[Random.Range(0, fakeSignoffs.Length)]);
        }
        else
        {
            email.bye = AddNames(veryFakeSignoffs[Random.Range(0, veryFakeSignoffs.Length)]);
        }

        if (Random.Range(0, 1.0f) < _easyPhishingFaintlyFakeChance)
        {
            email.logo = fakeLogo;
        }
        else
        {
            email.logo = veryFakeLogo;
        }



        email.isPhishing = true;
    }
    private void GenerateFakeMedium(ref Email_Scriptable email)
    {
        email.sender = _senderName;

        email.senderAdress = _senderAddress;

        email.tittle = "Placeholder Title";


        if (reuseBodies)
        {
            int index = Random.Range(0, fakeBodies.Count);
            email.content = AddNames(fakeBodies[Random.Range(0, fakeBodies.Count)].body);
            email.tittle = fakeBodies[index].title;
        }
        else if (_unusedFakeBodies.Count > 0)
        {
            int index = Random.Range(0, _unusedFakeBodies.Count);
            email.content = AddNames(_unusedFakeBodies[index].body);
            email.tittle = _unusedFakeBodies[index].title;

            _unusedFakeBodies.RemoveAt(index);
        }
        else
        {
            Debug.Log("Trying to generate an email without a suitable body.");
        }



        float greetingRand = Random.Range(0, 1.0f);
        if (greetingRand < _mediumPhishingRealChance)
        {
            email.greetings = AddNames(greetings[Random.Range(0, greetings.Length)]);
        }
        else if (greetingRand < _mediumPhishingRealChance + _mediumPhishingVeryFakeChance)
        {
            email.greetings = AddNames(veryFakeGreetings[Random.Range(0, veryFakeGreetings.Length)]);
        }
        else
        {
            email.greetings = AddNames(fakeGreetings[Random.Range(0, fakeGreetings.Length)]);
        }

        float signoffRand = Random.Range(0, 1.0f);
        if (signoffRand < _mediumPhishingRealChance)
        {
            email.bye = AddNames(signoffs[Random.Range(0, signoffs.Length)]);
        }
        else if (signoffRand < _mediumPhishingRealChance + _mediumPhishingVeryFakeChance)
        {
            email.bye = AddNames(veryFakeSignoffs[Random.Range(0, veryFakeSignoffs.Length)]);
        }
        else
        {
            email.bye = AddNames(fakeSignoffs[Random.Range(0, fakeSignoffs.Length)]);
        }

        float logoRand = Random.Range(0, 1.0f);
        if (logoRand < _mediumPhishingRealChance)
        {
            email.logo = logo;
        }
        else if (logoRand < _mediumPhishingRealChance + _mediumPhishingVeryFakeChance)
        {
            email.logo = veryFakeLogo;
        }
        else
        {
            email.logo = fakeLogo;
        }



        email.isPhishing = true;
    }
    private void GenerateFakeHard(ref Email_Scriptable email)
    {
        email.sender = _senderName;

        email.senderAdress = _senderAddress;

        email.tittle = "Placeholder Title";


        if (reuseBodies)
        {
            int index = Random.Range(0, faintlyFakeBodies.Count);
            email.content = AddNames(faintlyFakeBodies[Random.Range(0, faintlyFakeBodies.Count)].body);
            email.tittle = faintlyFakeBodies[index].title;
        }
        else if (_unusedFaintlyFakeBodies.Count > 0)
        {
            int index = Random.Range(0, _unusedFaintlyFakeBodies.Count);
            email.content = AddNames(_unusedFaintlyFakeBodies[index].body);
            email.tittle = _unusedFaintlyFakeBodies[index].title;

            _unusedFaintlyFakeBodies.RemoveAt(index);
        }
        else
        {
            Debug.Log("Trying to generate an email without a suitable body.");
        }



        if (Random.Range(0, 1.0f) < _hardPhishingFaintlyFakeChance)
        {
            email.greetings = AddNames(fakeGreetings[Random.Range(0, fakeGreetings.Length)]);
        }
        else
        {
            email.greetings = AddNames(greetings[Random.Range(0, greetings.Length)]);
        }

        if (Random.Range(0, 1.0f) < _hardPhishingFaintlyFakeChance)
        {
            email.bye = AddNames(fakeSignoffs[Random.Range(0, fakeSignoffs.Length)]);
        }
        else
        {
            email.bye = AddNames(signoffs[Random.Range(0, signoffs.Length)]);
        }

        if (Random.Range(0, 1.0f) < _hardPhishingFaintlyFakeChance)
        {
            email.logo = fakeLogo;
        }
        else
        {
            email.logo = logo;
        }



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

    private void StartRemoveAt<T>(ref T[] arr, int index)
    {
        #region Remove The Array Info

        for (int i = index; i < arr.Length - 1; i++)
        {
            //Move elements to fill the gap
            arr[i] = arr[i + 1];
        }

        //Remove the index
        System.Array.Resize(ref arr, arr.Length - 1);
        #endregion
    }
}
