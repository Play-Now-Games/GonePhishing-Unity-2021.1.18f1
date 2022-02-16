using UnityEngine;

public class Main : MonoBehaviour
{

    ///////// PUBLIC /////////
    public Email_Scriptable selectedEmail = null;

    public GameObject emailPrefab;

    public GameObject selected;
    public GameObject noEmail;
    ///////// PUBLIC /////////

    ///////// PRIVATES /////////
    [SerializeField]
    private int _phishingAmount;

    [SerializeField]
    private int _playerState = 0;

    [SerializeField]
    private int[] _emailOnTESTDONTDELETE;

    [SerializeField]
    private Email_Scriptable[] _totalEmails;

    [SerializeField]
    private Email_Scriptable[] _normalEmails;

    [SerializeField]
    private Email_Scriptable[] _phishing;
    ///////// PRIVATES /////////

    //Do its commands BEFORE the first frame
    private void Awake()
    {
        AwakeRandomizationEmail();
    }

    private void AwakeRandomizationEmail()
    {
        #region Email Randomization

        int proportion = _emailOnTESTDONTDELETE.Length / _phishingAmount;

        for (int i = 0; i < _emailOnTESTDONTDELETE.Length; i++)
        {
            proportion--;

            if (proportion > 0)
            {
                //Set the normal email

                //Change with the "ToDo:"
                _emailOnTESTDONTDELETE[i] = 1;
                //ToDo: _totalEmails[i] = _normalEmails[random.range(0, _normalEmails.lenght())]
                //ToDo: Remove selected email and reorganize array
            }
            else
            {
                //Set the phishing email

                //Change with the "ToDo:"
                _emailOnTESTDONTDELETE[i] = 999999999;
                //ToDo:_totalEmails[i] = _phishing[random.range(0, _phishing.lenght())]
                //ToDo: Remove selected email and reorganize array
                proportion = _emailOnTESTDONTDELETE.Length / _phishingAmount;
            }
        }
        #endregion
    }

    // Start is called before the first frame update
    void Start()
    {
        StartUICreation();
    }


    private void StartUICreation()
    {
        #region Spawn Emails

        //Find By Name
        GameObject Content = GameObject.Find("Content");

        //Vectors to spawn -- 110 is the 100 + offset
        Vector2 height = new Vector2(0, 110);

        for (int i = 0; i < _totalEmails.Length; i++)
        {
            //Email Pos Based on Pos in the array
            Vector2 Transfor = new Vector2(Content.transform.position.x - 25, Content.transform.position.y);
            Vector2 emailNewPos = Transfor - (height * i);

            //Instantiate & Set Child
            GameObject ChildObject = Instantiate(emailPrefab, emailNewPos, Quaternion.identity);
            ChildObject.transform.parent = Content.transform;

            //Add Scriptable Object Here
            EmailHolder holder = ChildObject.GetComponent<EmailHolder>();
            holder.holder = _totalEmails[i];
        }
        #endregion

        #region Update Height - Scroll Bar

        RectTransform RectT = Content.GetComponent<RectTransform>();
        RectT.sizeDelta = new Vector2(RectT.sizeDelta.x, height.y * _totalEmails.Length);

        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        #region Player State
        switch (_playerState)
        {
            case 0:
                //ToDo: Move Camera. Outside Computer
                break;
            case 1:
                //ToDo: Move Mouse. Inside Computer
                break;
        }
        #endregion

        
        if(selectedEmail)
        {
            noEmail.SetActive(false);
        }
        
    }
}
