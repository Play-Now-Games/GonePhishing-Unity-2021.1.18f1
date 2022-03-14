//Gabriel 'DiosMussolinos' Vergari
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Camera))]
public class StartMenu : MonoBehaviour
{

    ////// PUBLIC /////////
    public GameObject companyObject;
    public GameObject osObject;
    public GameObject warningObject;
    public GameObject MainMenuObject;

    public float fadeSpeed;
    public float angle = 0;

    [HideInInspector]
    public float _waitTime = 2;

    public float _waitTimeInit = 2;
    ////// PUBLIC /////////

    ////// PRIVATE /////////
    private int _state = 1;
    ////// PRIVATE /////////

    private void Start()
    {
        MainMenuObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        #region Switch Statement _state
        switch (_state)
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
        #endregion

    }

    public void CompanyLogoOn()
    {
        //////Company Logo
        GameObject Company = companyObject.transform.GetChild(0).gameObject;
        RawImage CompanyLogo = Company.GetComponent<RawImage>();

        Color AlphaLogo = CompanyLogo.color;
        float rad = angle * Mathf.Deg2Rad;
        AlphaLogo.a = Mathf.Sin(rad);
        CompanyLogo.color = AlphaLogo;

        angle += fadeSpeed * Time.deltaTime;

        if(angle > 90)
        {
            _state = 2;
        }
    }

    public void CompanyLogoWait()
    {
        _waitTime -= Time.deltaTime;

        if(_waitTime < 0)
        {
            _waitTime = _waitTimeInit;
            _state = 3;
        }
    }

    public void CompanyLogoOff()
    {
        //////Company Logo
        GameObject Company = companyObject.transform.GetChild(0).gameObject;
        RawImage CompanyLogo = Company.GetComponent<RawImage>();

        Color AlphaLogo = CompanyLogo.color;
        float rad = angle * Mathf.Deg2Rad;
        AlphaLogo.a = Mathf.Sin(rad);
        CompanyLogo.color = AlphaLogo;


        angle += fadeSpeed * Time.deltaTime;

        if(angle > 180)
        {
            angle = 0;
            _state = 4;
            AlphaLogo.a = 0;
            CompanyLogo.color = AlphaLogo;
        }
    }

    public void OSLogoOn()
    {
        //////OS LOGO
        GameObject osObjectLogo = osObject.transform.GetChild(0).gameObject;
        RawImage OsLogo = osObjectLogo.GetComponent<RawImage>();

        Color AlphaLogo = OsLogo.color;
        float rad = angle * Mathf.Deg2Rad;
        AlphaLogo.a = Mathf.Sin(rad);
        OsLogo.color = AlphaLogo;

        //////OS TEXT
        GameObject osObjectText = osObject.transform.GetChild(1).gameObject;
        Text OsText = osObjectText.GetComponent<Text>();

        Color AlphaOSText = OsText.color;
        float rad2 = angle * Mathf.Deg2Rad;
        AlphaOSText.a = Mathf.Sin(rad2);
        OsText.color = AlphaOSText;

        angle += fadeSpeed * Time.deltaTime;

        if (angle > 90)
        {
            _state = 5;
        }
    }

    public void OSLogoWait()
    {
        _waitTime -= Time.deltaTime;

        if (_waitTime < 0)
        {
            _waitTime = _waitTimeInit;
            _state = 6;
        }
    }

    public void OSLogoOff()
    {
        //////OS LOGO
        GameObject osObjectLogo = osObject.transform.GetChild(0).gameObject;
        RawImage OsLogo = osObjectLogo.GetComponent<RawImage>();

        Color AlphaLogo = OsLogo.color;
        float rad = angle * Mathf.Deg2Rad;
        AlphaLogo.a = Mathf.Sin(rad);
        OsLogo.color = AlphaLogo;

        //////OS TEXT
        GameObject osObjectText = osObject.transform.GetChild(1).gameObject;
        Text OsText = osObjectText.GetComponent<Text>();

        Color AlphaOSText = OsText.color;
        float rad2 = angle * Mathf.Deg2Rad;
        AlphaOSText.a = Mathf.Sin(rad2);
        OsText.color = AlphaOSText;

        angle += fadeSpeed * Time.deltaTime;

        if (angle > 180)
        {
            angle = 0;
            _state = 7;
            AlphaLogo.a = 0;
            OsText.color = AlphaLogo;
            OsLogo.color = AlphaLogo;
        }
    }

    public void P18On()
    {
        //////OS LOGO
        GameObject WarningObjectLogo = warningObject.transform.GetChild(0).gameObject;
        RawImage WarningLogo = WarningObjectLogo.GetComponent<RawImage>();

        Color AlphaLogo = WarningLogo.color;
        float rad = angle * Mathf.Deg2Rad;
        AlphaLogo.a = Mathf.Sin(rad);
        WarningLogo.color = AlphaLogo;

        //////OS TEXT
        GameObject WarningObjectText = warningObject.transform.GetChild(1).gameObject;
        Text WarningText = WarningObjectText.GetComponent<Text>();

        Color AlphaWarningText = WarningText.color;
        float rad2 = angle * Mathf.Deg2Rad;
        AlphaWarningText.a = Mathf.Sin(rad2);
        WarningText.color = AlphaWarningText;

        angle += fadeSpeed * Time.deltaTime;

        if (angle > 90)
        {
            _state = 8;
        }
    }

    public void P18Wait()
    {
        _waitTime -= Time.deltaTime;

        if (_waitTime < 0)
        {
            _waitTime = _waitTimeInit;
            _state = 9;
        }
    }

    public void P18Off()
    {
        //////OS LOGO
        GameObject WarningObjectLogo = warningObject.transform.GetChild(0).gameObject;
        RawImage WarningLogo = WarningObjectLogo.GetComponent<RawImage>();

        Color AlphaLogo = WarningLogo.color;
        float rad = angle * Mathf.Deg2Rad;
        AlphaLogo.a = Mathf.Sin(rad);
        WarningLogo.color = AlphaLogo;

        //////OS TEXT
        GameObject WarningObjectText = warningObject.transform.GetChild(1).gameObject;
        Text WarningText = WarningObjectText.GetComponent<Text>();

        Color AlphaWarningText = WarningText.color;
        float rad2 = angle * Mathf.Deg2Rad;
        AlphaWarningText.a = Mathf.Sin(rad2);
        WarningText.color = AlphaWarningText;

        angle += fadeSpeed * Time.deltaTime;

        if (angle > 180)
        {
            angle = 0;
            _state = 10;
            AlphaLogo.a = 0;
            WarningLogo.color = AlphaLogo;
            WarningText.color = AlphaLogo;
        }

    }

    public void MainMenu()
    {
        MainMenuObject.SetActive(true);
    }

}
