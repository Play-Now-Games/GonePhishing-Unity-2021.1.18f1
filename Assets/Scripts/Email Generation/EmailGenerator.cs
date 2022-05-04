using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmailGenerator : MonoBehaviour
{
    //for the test scene
    public EmailGenerationTestInterface emailInterface;

    public Email_Scriptable blankNoEmailToGenerate;

    public EmailGenerator_Scriptable[] personalEmailGenerators;
    public EmailGenerator_Scriptable[] corporateEmailGenerators;

    private List<int> validRealGenerators = new List<int>();
    private List<int> validFakeEasyGenerators = new List<int>();
    private List<int> validFakeMediumGenerators = new List<int>();
    private List<int> validFakeHardGenerators = new List<int>();
    private int totalGeneratorsLength;

    // Start is called before the first frame update
    void Awake()
    {
        foreach (EmailGenerator_Scriptable gen in personalEmailGenerators)
        {
            gen.ResetBodies();
        }
        foreach (EmailGenerator_Scriptable gen in corporateEmailGenerators)
        {
            gen.ResetBodies();
        }

        totalGeneratorsLength = personalEmailGenerators.Length + corporateEmailGenerators.Length;
        //fill validGenerators with reference to every generator at every type
        for (int i = 0; i < totalGeneratorsLength; i++)
        {
            validRealGenerators.Add(i);
            validFakeEasyGenerators.Add(i);
            validFakeMediumGenerators.Add(i);
            validFakeHardGenerators.Add(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GenerateEmail(out Email_Scriptable email);
            emailInterface.AddEmail(email);
        }
    }

    // Button functions for test scene
    // type: 0 = real, 1-3 = phishing with difficulty 1-3
    public void GenerateEmailButton(int type)
    {
        if (type == 0)
        {
            GenerateEmail(out Email_Scriptable email);
            emailInterface.AddEmail(email);
        }
        else if (type > 0 && type <=3)
        {
            GenerateEmail(out Email_Scriptable email, true, type);
            emailInterface.AddEmail(email);
        }
        else
        {
            Debug.LogError("Invalid email type.");
        }
    }

    public void GeneratePersonalEmailButton(int type)
    {
        if (type == 0)
        {
            GeneratePersonalEmail(out Email_Scriptable email);
            emailInterface.AddEmail(email);
        }
        else if (type > 0 && type <= 3)
        {
            GeneratePersonalEmail(out Email_Scriptable email, true, type);
            emailInterface.AddEmail(email);
        }
        else
        {
            Debug.LogError("Invalid email type.");
        }
    }

    public void GenerateCorporateEmailButton(int type)
    {
        if (type == 0)
        {
            GenerateCorporateEmail(out Email_Scriptable email);
            emailInterface.AddEmail(email);
        }
        else if (type > 0 && type <= 3)
        {
            GenerateCorporateEmail(out Email_Scriptable email, true, type);
            emailInterface.AddEmail(email);
        }
        else
        {
            Debug.LogError("Invalid email type.");
        }
    }


    //Generate an email fully at random from avalibe generators
    public bool GenerateEmail(out Email_Scriptable email, bool isPhishing = false, int phishingDifficulty = 0)
    {
        for (int i = 0; i < 10; i ++) //arbitary number of loops, TODO: change this to detect when we should give up
        {
            List<int> validGeneratorsThisDifficulty = new List<int>();

            switch (phishingDifficulty)
            {
                case 0:
                    validGeneratorsThisDifficulty = validRealGenerators;
                    break;
                case 1:
                    validGeneratorsThisDifficulty = validFakeEasyGenerators;
                    break;
                case 2:
                    validGeneratorsThisDifficulty = validFakeMediumGenerators;
                    break;
                case 3:
                    validGeneratorsThisDifficulty = validFakeHardGenerators;
                    break;
                default:
                    break;
            }

            int generatorIndex;
            if (validGeneratorsThisDifficulty.Count > 0)
            {
                generatorIndex = validGeneratorsThisDifficulty[Random.Range(0, validGeneratorsThisDifficulty.Count)];
            }
            else
            {
                email = blankNoEmailToGenerate;
                return false;
            }

            if (generatorIndex < personalEmailGenerators.Length)
            {
                if (personalEmailGenerators[generatorIndex].CanGenerate(isPhishing, phishingDifficulty))
                {
                    email = personalEmailGenerators[generatorIndex].GenerateEmail(isPhishing, phishingDifficulty);
                    return true;
                }
                else
                {
                    validGeneratorsThisDifficulty.Remove(generatorIndex);
                }
            }
            else
            {
                if (corporateEmailGenerators[generatorIndex - personalEmailGenerators.Length].CanGenerate(isPhishing, phishingDifficulty))
                {
                    email = corporateEmailGenerators[generatorIndex - personalEmailGenerators.Length].GenerateEmail(isPhishing, phishingDifficulty);
                    return true;
                }
                else
                {
                    validGeneratorsThisDifficulty.Remove(generatorIndex);
                }
            }
        }

        email = blankNoEmailToGenerate;
        return false;
    }

    public bool GeneratePersonalEmail(out Email_Scriptable email, bool isPhishing = false, int phishingDifficulty = 0)
    {
        for (int i = 0; i < 10; i++)
        {
            List<int> validGeneratorsThisDifficulty = new List<int>();

            switch (phishingDifficulty)
            {
                case 0:
                    validGeneratorsThisDifficulty = validRealGenerators;
                    break;
                case 1:
                    validGeneratorsThisDifficulty = validFakeEasyGenerators;
                    break;
                case 2:
                    validGeneratorsThisDifficulty = validFakeMediumGenerators;
                    break;
                case 3:
                    validGeneratorsThisDifficulty = validFakeHardGenerators;
                    break;
                default:
                    break;
            }

            int firstCorporateGenerator = validGeneratorsThisDifficulty.Count;
            for (int j = personalEmailGenerators.Length; j < totalGeneratorsLength; j++)
            {
                if (validGeneratorsThisDifficulty.Contains(j))
                {
                    firstCorporateGenerator = validGeneratorsThisDifficulty.IndexOf(j);
                    break;
                }
            }

            int generatorIndex;
            if (firstCorporateGenerator > 0)
            {
                generatorIndex = validGeneratorsThisDifficulty[Random.Range(0, firstCorporateGenerator)];
            }
            else
            {
                email = blankNoEmailToGenerate;
                return false;
            }

            if (personalEmailGenerators[generatorIndex].CanGenerate(isPhishing, phishingDifficulty))
            {
                email = personalEmailGenerators[generatorIndex].GenerateEmail(isPhishing, phishingDifficulty);
                return true;
            }
            else
            {
                validGeneratorsThisDifficulty.Remove(generatorIndex);
            }
        }

        email = blankNoEmailToGenerate;
        return false;
    }
    public Email_Scriptable GeneratePersonalEmail(int byIndex, bool isPhishing = false, int phishingDifficulty = 0)
    {
        byIndex = Mathf.Max(0, Mathf.Min(byIndex, personalEmailGenerators.Length - 1));

        return personalEmailGenerators[byIndex].GenerateEmail(isPhishing, phishingDifficulty);
    }

    public bool GenerateCorporateEmail(out Email_Scriptable email, bool isPhishing = false, int phishingDifficulty = 0)
    {
        for (int i = 0; i < 10; i++)
        {
            List<int> validGeneratorsThisDifficulty = new List<int>();

            switch (phishingDifficulty)
            {
                case 0:
                    validGeneratorsThisDifficulty = validRealGenerators;
                    break;
                case 1:
                    validGeneratorsThisDifficulty = validFakeEasyGenerators;
                    break;
                case 2:
                    validGeneratorsThisDifficulty = validFakeMediumGenerators;
                    break;
                case 3:
                    validGeneratorsThisDifficulty = validFakeHardGenerators;
                    break;
                default:
                    break;
            }

            int firstCorporateGenerator = validGeneratorsThisDifficulty.Count;
            for (int j = personalEmailGenerators.Length; j < totalGeneratorsLength; j++)
            {
                if (validGeneratorsThisDifficulty.Contains(j))
                {
                    firstCorporateGenerator = validGeneratorsThisDifficulty.IndexOf(j);
                    break;
                }
            }

            int generatorIndex;
            if (validGeneratorsThisDifficulty.Count > firstCorporateGenerator)
            {
                generatorIndex = validGeneratorsThisDifficulty[Random.Range(firstCorporateGenerator, validGeneratorsThisDifficulty.Count)];
            }
            else
            {
                email = blankNoEmailToGenerate;
                return false;
            }

            if (corporateEmailGenerators[generatorIndex - personalEmailGenerators.Length].CanGenerate(isPhishing, phishingDifficulty))
            {
                email = corporateEmailGenerators[generatorIndex - personalEmailGenerators.Length].GenerateEmail(isPhishing, phishingDifficulty);
                return true;
            }
            else
            {
                validGeneratorsThisDifficulty.Remove(generatorIndex);
            }
        }

        email = blankNoEmailToGenerate;
        return false;
    }
    public Email_Scriptable GenerateCorporateEmail(int byIndex, bool isPhishing = false, int phishingDifficulty = 0)
    {
        byIndex = Mathf.Max(0, Mathf.Min(byIndex, corporateEmailGenerators.Length - 1));

        return corporateEmailGenerators[byIndex].GenerateEmail(isPhishing, phishingDifficulty);
    }

}
