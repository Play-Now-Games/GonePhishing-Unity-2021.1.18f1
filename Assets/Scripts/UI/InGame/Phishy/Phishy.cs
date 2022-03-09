using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Phishy : MonoBehaviour, IPointerClickHandler
{

    [Tooltip("Comments Phishy will give when praising the player.")]
    public string[] PraiseComments;
    [Tooltip("Comments Phishy will give when mocking the player.")]
    public string[] MockComments;
    [Tooltip("Chance Phishy will make a comment when the player responds to an email.")]
    [Range(0, 100)]
    public float EmailResponseCommentChance;
    [Tooltip("Position on the screen where Phishy will appear when commenting on the player's email responses.")]
    public Vector2 EmailResponseCommentPosition;

    [Tooltip("An object that Phishy creates when talking, which displays text.")]
    public GameObject DialogueBubble;

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
    // Reference to the dialogue bubble
    private GameObject _dialogueBubble;

    // Start is called before the first frame update
    void Start()
    {
        _phishySprite = gameObject.GetComponent<Image>();
        _phishyAnimator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void LateUpdate()
    {

        if (_phishyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hidden"))
        {
            _phishySprite.enabled = false;
        }

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (gameObject.GetComponentInParent<CanvasGroup>().interactable)
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

        _phishyAnimator.Play("Disappear");

    }

    private void CreateDialogueBubble()
    {
        _dialogueBubble = Instantiate(DialogueBubble, transform);
        _dialogueBubble.GetComponentInChildren<Text>().text = _currentHint.HintText[_currentHintText];
    }

    public void AdvanceDialogueBubble()
    {

        _currentHintText++;

        #region If dialogue bubble ends
        if (_currentHintText >= _currentHint.HintText.Length)
        {
            Destroy(_dialogueBubble);
            PhishyDisappears();
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
        CreateDialogueBubble();

        #endregion

        // Store hint
        _givenHints.Add(hint);

    }

    public void TriggerPhishyComment(bool isEvil)
    {

        if (_phishySprite.enabled == false) {

            if (Random.Range(0, 100.0f) <= EmailResponseCommentChance)
            {
                #region Create the comment

                PhishyHint comment = ScriptableObject.CreateInstance<PhishyHint>();
                comment.IsEvil = isEvil;
                comment.HintText = new string[1];
                if (isEvil)
                {
                    comment.HintText[0] = MockComments[Random.Range(0, MockComments.Length - 1)];
                }
                else
                {
                    comment.HintText[0] = PraiseComments[Random.Range(0, MockComments.Length - 1)];
                }

                #endregion

                #region Display the comment

                _phishyAnimator.SetBool("IsEvil", comment.IsEvil);
                PhishyAppears(EmailResponseCommentPosition.x, EmailResponseCommentPosition.y);

                _currentHint = comment;
                _currentHintText = 0;
                CreateDialogueBubble();

                #endregion
            }

        }

    }

}
