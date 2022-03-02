using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Phishy : MonoBehaviour, IPointerClickHandler
{

    [SerializeField]
    [Tooltip("Comments Phishy will give when praising the player.")]
    public string[] PraiseComments;
    [SerializeField]
    [Tooltip("Comments Phishy will give when mocking the player.")]
    public string[] MockComments;
    [Tooltip("Chance Phishy will make a comment when the player responds to an email.")]
    [Range(0, 100)]
    public float EmailResponseCommentChance;

    [Tooltip("An object that Phishy creates when talking, which displays text.")]
    public GameObject DialogueBubble;

    public PhishyHint testHint;

    // List of hints that Phishy has given already, and won't be repeated.
    private List<PhishyHint> _givenHints = new List<PhishyHint>();
    // Which hint is currently being displayed
    private PhishyHint _currentHint;
    // Which text is currently displayed from the current hint
    private int _currentHintText;

    // Reference to Phishy's image
    private Image _phishySprite;
    // Reference to Phishy's animation system
    private Animator _phishyAnimator;
    // Reference to the canvas that contains Phishy
    private RectTransform _phishyCanvas;
    // Reference to the dialogue bubble
    private GameObject _dialogueBubble;

    // Start is called before the first frame update
    void Start()
    {
        _phishySprite = gameObject.GetComponent<Image>();
        _phishyAnimator = gameObject.GetComponent<Animator>();
        _phishyCanvas = gameObject.GetComponentInParent<Canvas>().gameObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {

        // Debug testing thingy: press P to make phishy show up at a random location
        if (Input.GetKeyDown(KeyCode.P))
        {
            TriggerPhishyHint(testHint);
        }

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        AdvanceDialogueBubble();
    }

    private void PhishyAppears(float x, float y)
    {

        // Make sure sprite is hidden
        _phishySprite.enabled = false;

        // Move to location
        gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);

        // Show sprite (display animation of phishy appearing)
        _phishyAnimator.Play("Appear");
        _phishySprite.enabled = true;

    }

    private void PhishyDisappears()
    {

        // Show animation of phishy disappearing

        // Hide sprite

    }

    private void CreateDialogueBubble(string text)
    {
        _dialogueBubble = Instantiate(DialogueBubble, transform);
        _dialogueBubble.GetComponentInChildren<Text>().text = text;
    }

    public void AdvanceDialogueBubble()
    {

        _currentHintText++;

        #region If dialogue bubble ends
        if (_currentHintText >= _currentHint.HintText.Length)
        {
            Destroy(_dialogueBubble);
        }
        #endregion
        #region If dialogue bubble continues
        else
        {
            _dialogueBubble.GetComponentInChildren<Text>().text = _currentHint.HintText[_currentHintText];
        }
        #endregion

    }

    public void TriggerPhishyHint(PhishyHint hint)
    {

        #region Check if hint has been given already
        foreach (PhishyHint h in _givenHints)
        {
            if (h == hint)
                return;
        }
        #endregion

        #region Give the hint

        _phishyAnimator.SetBool("IsEvil", hint.IsEvil);
        PhishyAppears(hint.xPosition, hint.yPosition);

        _currentHint = hint;
        _currentHintText = 0;
        CreateDialogueBubble(hint.HintText[0]);

        #endregion

        // Store hint
        _givenHints.Add(hint);

    }

    public void TriggerPhishyComment(bool isEvil)
    {



    }

}
