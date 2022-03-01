using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{

    ////// PUBLIC /////////
    public Image companyImage;

    public Image osImage;
    public Text osText;

    public Image warningImage;
    public Text warningText;

    public GameObject Monday;
    ////// PUBLIC /////////

    ////// PRIVATE /////////
    private int _state = 0;
    ////// PRIVATE /////////

    // Update is called once per frame
    void Update()
    {
        switch(_state)
        {
            case 1:
                CompanyLogoOn();
                break;
            case 2:
                CompanyLogoWait();
                break;
            case 3:
                CompanyLogoOff();
                break;
            case 4:
                OSLogoOn();
                break;
            case 5:
                OSLogoWait();
                break;
            case 6:
                OSLogoOff();
                break;
            case 7:
                P18On();
                break;
            case 8:
                P18Wait();
                break;
            case 9:
                P18Off();
                break;
            case 10:
                MainMenu();
                break;

        }
    }



    public void CompanyLogoOn()
    {

    }

    public void CompanyLogoWait()
    {

    }

    public void CompanyLogoOff()
    {

    }

    public void OSLogoOn()
    {

    }

    public void OSLogoWait()
    {

    }

    public void OSLogoOff()
    {

    }

    public void P18On()
    {

    }

    public void P18Wait()
    {

    }

    public void P18Off()
    {

    }

    public void MainMenu()
    {

    }

}
