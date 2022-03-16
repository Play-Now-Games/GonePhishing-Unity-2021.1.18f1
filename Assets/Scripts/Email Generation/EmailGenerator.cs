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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            emailInterface.AddEmail(GenerateEmail());
        }
    }

    //for button based testing
    public void GenerateRealEmailButton()
    {
        emailInterface.AddEmail(GenerateEmail());
    }
    //for button based testing
    public void GenerateFakeEasyEmailButton()
    {
        emailInterface.AddEmail(GenerateEmail(true, 1));
    }
    //for button based testing
    public void GenerateFakeMediumEmailButton()
    {
        emailInterface.AddEmail(GenerateEmail(true, 2));
    }
    //for button based testing
    public void GenerateFakeHardEmailButton()
    {
        emailInterface.AddEmail(GenerateEmail(true, 3));
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
