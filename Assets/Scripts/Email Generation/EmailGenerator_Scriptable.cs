using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Generator", menuName = "ScriptableObjects/EmailGenerator")]
public class EmailGenerator_Scriptable : ScriptableObject
{
    [Tooltip("Use #firstName, #lastName, #title, and #senderName in the text as apropriate.")]
    public string firstName, lastName, title, senderName, senderAddress;
    public string[] mediumFakeFirstNames, mediumFakeLastNames, mediumFakeTitles, mediumFakeSenderNames, mediumFakeSenderAddress;
    public string[] easyFakeFirstNames, easyFakeLastNames, easyFakeTitles, easyFakeSenderNames, easyFakeSenderAddress;

    private string _firstName, _lastName, _title, _senderName, _senderAddress;

    [Space(10)]

    public Sprite logo;
    public Sprite mediumFakeLogo;
    public Sprite easyFakeLogo;

    [Space(10)]

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
    public List<EmailBodyAndTitle> bodies;
    private List<EmailBodyAndTitle> _unusedBodies;

    public List<EmailBodyAndTitle> hardFakeBodies;
    private List<EmailBodyAndTitle> _unusedHardFakeBodies;

    public List<EmailBodyAndTitle> mediumFakeBodies;
    private List<EmailBodyAndTitle> _unusedMediumFakeBodies;

    public List<EmailBodyAndTitle> easyFakeBodies;
    private List<EmailBodyAndTitle> _unusedEasyFakeBodies;

    [Space(10)]

    [TextArea]
    public string[] signoffs;
    [TextArea]
    public string[] mediumFakeSignoffs;
    [TextArea]
    public string[] easyFakeSignoffs;

    [Space(15)]

    [SerializeField]
    [Tooltip("Chance of slightly fake components showing up in Hard Phishing Emails")]
    [Range(0.0f, 0.5f)]
    private float _chanceOfMediumInHard = 0.1f;

    [SerializeField]
    [Tooltip("Chance of real-appearing components showing up in Medium Phishing Emails")]
    [Range(0.0f, 0.5f)]
    private float _chanceOfHardInMedium = 0.1f;

    [SerializeField]
    [Tooltip("Chance of very fake components showing up in Medium Phishing Emails")]
    [Range(0.0f, 0.5f)]
    private float _chanceOfEasyInMedium = 0.1f;

    [SerializeField]
    [Tooltip("Chance of only slightly fake components showing up in Easy Phishing Emails")]
    [Range(0.0f, 0.5f)]
    private float _chanceOfMediumInEasy = 0.2f;

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
        if (!reuseBodies)
        {
            _unusedBodies = new List<EmailBodyAndTitle>(bodies);
            _unusedHardFakeBodies = new List<EmailBodyAndTitle>(hardFakeBodies);
            _unusedMediumFakeBodies = new List<EmailBodyAndTitle>(mediumFakeBodies);
            _unusedEasyFakeBodies = new List<EmailBodyAndTitle>(easyFakeBodies);
        }
    }

    public bool CanGenerate(bool isPhishing = false, int phishingDifficulty = 0)
    {
        if (reuseBodies)
        {
            if (!isPhishing)
            {
                if (bodies.Count > 0)
                {
                    return true;
                }
            }
            else
            {
                switch (phishingDifficulty)
                {
                    case 1:
                        if (easyFakeBodies.Count > 0)
                        {
                            return true;
                        }
                        break;
                    case 2:
                        if (mediumFakeBodies.Count > 0)
                        {
                            return true;
                        }
                        break;
                    case 3:
                        if (hardFakeBodies.Count > 0)
                        {
                            return true;
                        }
                        break;
                    default:
                        Debug.LogError("Invalid phishing difficulty: " + phishingDifficulty);
                        break;
                }

            }
        }
        else
        {
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
                        if (_unusedEasyFakeBodies.Count > 0)
                        {
                            return true;
                        }
                        break;
                    case 2:
                        if (_unusedMediumFakeBodies.Count > 0)
                        {
                            return true;
                        }
                        break;
                    case 3:
                        if (_unusedHardFakeBodies.Count > 0)
                        {
                            return true;
                        }
                        break;
                    default:
                        Debug.LogError("Invalid phishing difficulty: " + phishingDifficulty);
                        break;
                }

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
        if (easyFakeFirstNames.Length > 0)
        {
            _firstName = easyFakeFirstNames[Random.Range(0, easyFakeFirstNames.Length)];
        }
        else
        {
            if (mediumFakeFirstNames.Length > 0)
            {
                _firstName = mediumFakeFirstNames[Random.Range(0, mediumFakeFirstNames.Length)];
            }
            else
            {
                _firstName = firstName;
            }
        }

        if (easyFakeLastNames.Length > 0)
        {
            _lastName = easyFakeLastNames[Random.Range(0, easyFakeLastNames.Length)];
        }
        else
        {
            if (mediumFakeLastNames.Length > 0)
            {
                _lastName = mediumFakeLastNames[Random.Range(0, mediumFakeLastNames.Length)];
            }
            else
            {
                _lastName = lastName;
            }
        }

        if (easyFakeTitles.Length > 0)
        {
            _title = easyFakeTitles[Random.Range(0, easyFakeTitles.Length)];
        }
        else
        {
            if (mediumFakeTitles.Length > 0)
            {
                _title = mediumFakeTitles[Random.Range(0, mediumFakeTitles.Length)];
            }
            else
            {
                _title = title;
            }
        }

        if (easyFakeSenderNames.Length > 0)
        {
            _senderName = easyFakeSenderNames[Random.Range(0, easyFakeSenderNames.Length)];
        }
        else
        {
            if (mediumFakeSenderNames.Length > 0)
            {
                _senderName = mediumFakeSenderNames[Random.Range(0, mediumFakeSenderNames.Length)];
            }
            else
            {
                _senderName = senderName;
            }
        }

        if (easyFakeSenderAddress.Length > 0)
        {
            _senderAddress = easyFakeSenderAddress[Random.Range(0, easyFakeSenderAddress.Length)];
        }
        else
        {
            if (mediumFakeSenderAddress.Length > 0)
            {
                _senderAddress = mediumFakeSenderAddress[Random.Range(0, mediumFakeSenderAddress.Length)];
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
        if (mediumFakeFirstNames.Length > 0)
        {
            _firstName = mediumFakeFirstNames[Random.Range(0, mediumFakeFirstNames.Length)];
        }
        else
        {
            _firstName = firstName;
        }

        if (mediumFakeLastNames.Length > 0)
        {
            _lastName = mediumFakeLastNames[Random.Range(0, mediumFakeLastNames.Length)];
        }
        else
        {
            _lastName = lastName;
        }

        if (mediumFakeTitles.Length > 0)
        {
            _title = mediumFakeTitles[Random.Range(0, mediumFakeTitles.Length)];
        }
        else
        {
            _title = title;
        }

        if (mediumFakeSenderNames.Length > 0)
        {
            _senderName = mediumFakeSenderNames[Random.Range(0, mediumFakeSenderNames.Length)];
        }
        else
        {
            _senderName = senderName;
        }

        if (mediumFakeSenderAddress.Length > 0)
        {
            _senderAddress = mediumFakeSenderAddress[Random.Range(0, mediumFakeSenderAddress.Length)];
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


        if (reuseBodies && bodies.Count > 0)
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


        if (reuseBodies && easyFakeBodies.Count > 0)
        {
            int index = Random.Range(0, easyFakeBodies.Count);
            email.content = AddNames(easyFakeBodies[Random.Range(0, easyFakeBodies.Count)].body);
            email.tittle = easyFakeBodies[index].title;
        }
        else if (_unusedEasyFakeBodies.Count > 0)
        {
            int index = Random.Range(0, _unusedEasyFakeBodies.Count);
            email.content = AddNames(_unusedEasyFakeBodies[index].body);
            email.tittle = _unusedEasyFakeBodies[index].title;

            _unusedEasyFakeBodies.RemoveAt(index);
        }
        else
        {
            Debug.Log("Trying to generate an email without a suitable body.");
        }



        if (Random.Range(0, 1.0f) < _chanceOfMediumInEasy)
        {
            email.greetings = AddNames(fakeGreetings[Random.Range(0, fakeGreetings.Length)]);
        }
        else
        {
            email.greetings = AddNames(veryFakeGreetings[Random.Range(0, veryFakeGreetings.Length)]);
        }

        if (Random.Range(0, 1.0f) < _chanceOfMediumInEasy)
        {
            email.bye = AddNames(mediumFakeSignoffs[Random.Range(0, mediumFakeSignoffs.Length)]);
        }
        else
        {
            email.bye = AddNames(easyFakeSignoffs[Random.Range(0, easyFakeSignoffs.Length)]);
        }

        if (Random.Range(0, 1.0f) < _chanceOfMediumInEasy)
        {
            email.logo = mediumFakeLogo;
        }
        else
        {
            email.logo = easyFakeLogo;
        }



        email.isPhishing = true;
    }
    private void GenerateFakeMedium(ref Email_Scriptable email)
    {
        email.sender = _senderName;

        email.senderAdress = _senderAddress;

        email.tittle = "Placeholder Title";


        if (reuseBodies && mediumFakeBodies.Count > 0)
        {
            int index = Random.Range(0, mediumFakeBodies.Count);
            email.content = AddNames(mediumFakeBodies[Random.Range(0, mediumFakeBodies.Count)].body);
            email.tittle = mediumFakeBodies[index].title;
        }
        else if (_unusedMediumFakeBodies.Count > 0)
        {
            int index = Random.Range(0, _unusedMediumFakeBodies.Count);
            email.content = AddNames(_unusedMediumFakeBodies[index].body);
            email.tittle = _unusedMediumFakeBodies[index].title;

            _unusedMediumFakeBodies.RemoveAt(index);
        }
        else
        {
            Debug.Log("Trying to generate an email without a suitable body.");
        }



        float greetingRand = Random.Range(0, 1.0f);
        if (greetingRand < _chanceOfHardInMedium)
        {
            email.greetings = AddNames(greetings[Random.Range(0, greetings.Length)]);
        }
        else if (greetingRand < _chanceOfHardInMedium + _chanceOfEasyInMedium)
        {
            email.greetings = AddNames(veryFakeGreetings[Random.Range(0, veryFakeGreetings.Length)]);
        }
        else
        {
            email.greetings = AddNames(fakeGreetings[Random.Range(0, fakeGreetings.Length)]);
        }

        float signoffRand = Random.Range(0, 1.0f);
        if (signoffRand < _chanceOfHardInMedium)
        {
            email.bye = AddNames(signoffs[Random.Range(0, signoffs.Length)]);
        }
        else if (signoffRand < _chanceOfHardInMedium + _chanceOfEasyInMedium)
        {
            email.bye = AddNames(easyFakeSignoffs[Random.Range(0, easyFakeSignoffs.Length)]);
        }
        else
        {
            email.bye = AddNames(mediumFakeSignoffs[Random.Range(0, mediumFakeSignoffs.Length)]);
        }

        float logoRand = Random.Range(0, 1.0f);
        if (logoRand < _chanceOfHardInMedium)
        {
            email.logo = logo;
        }
        else if (logoRand < _chanceOfHardInMedium + _chanceOfEasyInMedium)
        {
            email.logo = easyFakeLogo;
        }
        else
        {
            email.logo = mediumFakeLogo;
        }



        email.isPhishing = true;
    }
    private void GenerateFakeHard(ref Email_Scriptable email)
    {
        email.sender = _senderName;

        email.senderAdress = _senderAddress;

        email.tittle = "Placeholder Title";


        if (reuseBodies && hardFakeBodies.Count > 0)
        {
            int index = Random.Range(0, hardFakeBodies.Count);
            email.content = AddNames(hardFakeBodies[Random.Range(0, hardFakeBodies.Count)].body);
            email.tittle = hardFakeBodies[index].title;
        }
        else if (_unusedHardFakeBodies.Count > 0)
        {
            int index = Random.Range(0, _unusedHardFakeBodies.Count);
            email.content = AddNames(_unusedHardFakeBodies[index].body);
            email.tittle = _unusedHardFakeBodies[index].title;

            _unusedHardFakeBodies.RemoveAt(index);
        }
        else
        {
            Debug.Log("Trying to generate an email without a suitable body.");
        }



        if (Random.Range(0, 1.0f) < _chanceOfMediumInHard)
        {
            email.greetings = AddNames(fakeGreetings[Random.Range(0, fakeGreetings.Length)]);
        }
        else
        {
            email.greetings = AddNames(greetings[Random.Range(0, greetings.Length)]);
        }

        if (Random.Range(0, 1.0f) < _chanceOfMediumInHard)
        {
            email.bye = AddNames(mediumFakeSignoffs[Random.Range(0, mediumFakeSignoffs.Length)]);
        }
        else
        {
            email.bye = AddNames(signoffs[Random.Range(0, signoffs.Length)]);
        }

        if (Random.Range(0, 1.0f) < _chanceOfMediumInHard)
        {
            email.logo = mediumFakeLogo;
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
