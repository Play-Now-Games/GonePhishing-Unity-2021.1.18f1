using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmailDisplayScript : MonoBehaviour
{

    public Image logo;
    [Space(10)]
    public Text title;
    public Text senderAddress;

    [Space(10)]

    public Text greeting;
    public Text body;
    public Text signoff;

    [Space(10)]

    public float internalSpacing = 50.0f;

    public float externalSpacing = 80.0f;


    private RectTransform _transform;
    private RectTransform _parentTransform;

    private Vector2 _logoPos;
    private Vector2 _logoSize;
    private Vector2 _titlePos;
    private Vector2 _titleSize;
    private Vector2 _senderPos;
    private Vector2 _senderSize;

    private Vector2 _greetingPos;
    private Vector2 _greetingTopPos;

    private float _wholeEmailTopPos;
    private float _wholeEmailBottomPos;

    private Vector2 _wholeEmailInitalPos;

    private int _numberOfUpdates = 0;
    private Email_Scriptable displayedEmail;

    private float _viewportHeight;

    // Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<RectTransform>();
        _parentTransform = _transform.parent.GetComponent<RectTransform>();

        #region Derive and store constants reguarding email layout
        _logoPos = logo.rectTransform.anchoredPosition;
        _logoSize = logo.rectTransform.sizeDelta;
        _titlePos = title.rectTransform.anchoredPosition;
        _titleSize = title.rectTransform.sizeDelta;
        _senderPos = senderAddress.rectTransform.anchoredPosition;
        _senderSize = senderAddress.rectTransform.sizeDelta;

        _greetingPos = greeting.rectTransform.anchoredPosition;
        _greetingTopPos = _greetingPos + new Vector2(0, greeting.rectTransform.sizeDelta.y / 2.0f);

        _wholeEmailTopPos = _logoPos.y + _logoSize.y / 2.0f;

        _wholeEmailInitalPos = _parentTransform.anchoredPosition;

        _viewportHeight = _parentTransform.parent.GetComponent<RectTransform>().sizeDelta.y;
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        if (_numberOfUpdates > 0)
        {
            UpdateDisplay();
            _numberOfUpdates--;
        }
    }

    public void UpdateDisplay(Email_Scriptable email)
    {
        displayedEmail = email;
        _numberOfUpdates = 3; //Why does it need to update twice?, TODO: figure out why, this should also fix a visual judder (third update only needed for after clicking answer/ignore?)

        logo.sprite = displayedEmail.logo;
        title.text = displayedEmail.tittle;
        senderAddress.text = displayedEmail.senderAdress;
        greeting.text = displayedEmail.greetings;
        body.text = displayedEmail.content;
        signoff.text = displayedEmail.bye;
    }

    private void UpdateDisplay()
    {
        #region Update layout based on current contents

        //check for special case of extra large address
        float longSenderAddressFactor = 0.0f;
        if (senderAddress.preferredHeight > _senderSize.y)
        {
            longSenderAddressFactor = senderAddress.preferredHeight - _senderSize.y;
        }



        _wholeEmailBottomPos = _greetingTopPos.y - greeting.preferredHeight - body.preferredHeight - signoff.preferredHeight - (internalSpacing * 2) - longSenderAddressFactor;

        _transform.sizeDelta = new Vector2(_transform.sizeDelta.x, _wholeEmailTopPos - _wholeEmailBottomPos);

        _parentTransform.sizeDelta = _transform.sizeDelta + new Vector2(0, externalSpacing);
        _parentTransform.anchoredPosition = new Vector2(_parentTransform.anchoredPosition.x, _wholeEmailTopPos - (_parentTransform.sizeDelta.y / 2.0f));

        Vector2 wholeEmailPosScale = _wholeEmailInitalPos - _parentTransform.anchoredPosition - new Vector2(0, externalSpacing / 2.0f);

        if (_viewportHeight > _parentTransform.sizeDelta.y)
        {
            wholeEmailPosScale.y += (_viewportHeight - _parentTransform.sizeDelta.y) / 2.0f;
        }

        logo.rectTransform.sizeDelta = _logoSize;
        logo.rectTransform.anchoredPosition = _logoPos + wholeEmailPosScale;

        title.rectTransform.sizeDelta = _titleSize;
        title.rectTransform.anchoredPosition = _titlePos + wholeEmailPosScale;

        senderAddress.rectTransform.sizeDelta = _senderSize;
        senderAddress.rectTransform.anchoredPosition = _senderPos + wholeEmailPosScale;
        if (senderAddress.preferredHeight > _senderSize.y)
        {
            senderAddress.rectTransform.sizeDelta += new Vector2(0, longSenderAddressFactor);
            senderAddress.rectTransform.anchoredPosition -= new Vector2(0, longSenderAddressFactor / 2.0f);
        }

        greeting.rectTransform.sizeDelta = new Vector2(greeting.rectTransform.sizeDelta.x, greeting.preferredHeight);
        greeting.rectTransform.anchoredPosition = _greetingTopPos - new Vector2(0, (greeting.preferredHeight / 2.0f) + longSenderAddressFactor) + wholeEmailPosScale;

        body.rectTransform.sizeDelta = new Vector2(body.rectTransform.sizeDelta.x, body.preferredHeight);
        body.rectTransform.anchoredPosition = greeting.rectTransform.anchoredPosition - new Vector2(0, ((greeting.preferredHeight + body.preferredHeight) / 2.0f) + internalSpacing);

        signoff.rectTransform.sizeDelta = new Vector2(signoff.rectTransform.sizeDelta.x, signoff.preferredHeight);
        signoff.rectTransform.anchoredPosition = body.rectTransform.anchoredPosition - new Vector2(0, ((body.preferredHeight + signoff.preferredHeight) / 2.0f) + internalSpacing);
        #endregion
    }
}
