using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmailGenerator : MonoBehaviour
{
    public EmailGenerationTestInterface emailInterface;

    public EmailGenerator_Scriptable[] personalEmailGenerators;
    public EmailGenerator_Scriptable[] corporateEmailGenerators;

    // Start is called before the first frame update
    void Start()
    {
        foreach (EmailGenerator_Scriptable gen in personalEmailGenerators)
        {
            gen.ResetBodies();
        }
        foreach (EmailGenerator_Scriptable gen in corporateEmailGenerators)
        {
            gen.ResetBodies();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            emailInterface.AddEmail(GenerateEmail());
        }
    }

    // type: 0 = real, 1-3 = phishing with difficulty 1-3
    public void GenerateEmailButton(int type)
    {
        if (type == 0)
        {
            emailInterface.AddEmail(GenerateEmail());
        }
        else if (type > 0 && type <=3)
        {
            emailInterface.AddEmail(GenerateEmail(true, type));
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
            emailInterface.AddEmail(GeneratePersonalEmail());
        }
        else if (type > 0 && type <= 3)
        {
            emailInterface.AddEmail(GeneratePersonalEmail(true, type));
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
            emailInterface.AddEmail(GenerateCorporateEmail());
        }
        else if (type > 0 && type <= 3)
        {
            emailInterface.AddEmail(GenerateCorporateEmail(true, type));
        }
        else
        {
            Debug.LogError("Invalid email type.");
        }
    }


    //Generate an email fully at random from avalibe generators
    public Email_Scriptable GenerateEmail(bool isPhishing = false, int phishingDifficulty = 0)
    {
        int generatorIndex = Random.Range(0, personalEmailGenerators.Length + corporateEmailGenerators.Length);

        if (generatorIndex < personalEmailGenerators.Length)
        {
            return personalEmailGenerators[generatorIndex].GenerateEmail(isPhishing, phishingDifficulty);
        }
        else
        {
            return corporateEmailGenerators[generatorIndex - personalEmailGenerators.Length].GenerateEmail(isPhishing, phishingDifficulty);
        }
    }

    public Email_Scriptable GeneratePersonalEmail(bool isPhishing = false, int phishingDifficulty = 0)
    {
        int generatorIndex = Random.Range(0, personalEmailGenerators.Length);
            
        return personalEmailGenerators[generatorIndex].GenerateEmail(isPhishing, phishingDifficulty);
    }
    public Email_Scriptable GeneratePersonalEmail(int byIndex, bool isPhishing = false, int phishingDifficulty = 0)
    {
        byIndex = Mathf.Max(0, Mathf.Min(byIndex, personalEmailGenerators.Length - 1));

        return personalEmailGenerators[byIndex].GenerateEmail(isPhishing, phishingDifficulty);
    }

    public Email_Scriptable GenerateCorporateEmail(bool isPhishing = false, int phishingDifficulty = 0)
    {
        int generatorIndex = Random.Range(0, corporateEmailGenerators.Length);

        return corporateEmailGenerators[generatorIndex].GenerateEmail(isPhishing, phishingDifficulty);
    }
    public Email_Scriptable GenerateCorporateEmail(int byIndex, bool isPhishing = false, int phishingDifficulty = 0)
    {
        byIndex = Mathf.Max(0, Mathf.Min(byIndex, corporateEmailGenerators.Length - 1));

        return corporateEmailGenerators[byIndex].GenerateEmail(isPhishing, phishingDifficulty);
    }

}
