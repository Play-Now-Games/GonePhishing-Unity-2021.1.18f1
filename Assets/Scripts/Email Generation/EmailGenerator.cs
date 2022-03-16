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

}
