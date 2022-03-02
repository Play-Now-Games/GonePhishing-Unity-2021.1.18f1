using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Phishy : MonoBehaviour
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

    // List of hints that Phishy has given already, and won't be repeated.
    private List<PhishyHint> _givenHints;

    // Reference to Phishy's image
    private Image _phishySprite;

    private Animator _phishyAnimator;

    // Start is called before the first frame update
    void Start()
    {
        _phishySprite = gameObject.GetComponent<Image>();
        _phishyAnimator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        // If Phishy dialogue is currently active, check for clicks to advance the dialogue



        // Debug testing thingy: press P to make phishy show up at a random location
        if (Input.GetKeyDown(KeyCode.P))
        {
            PhishyAppears(Random.Range(-500, 500), Random.Range(-500, 500));
        }

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

    private void CreateDialogueBubble()
    {

    }

    private void ChangeDialogueBubbleText(string text)
    {

    }

    private void RemoveDialogueBubble()
    {

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

        #endregion

        // Store hint
        _givenHints.Add(hint);

    }

    public void TriggerPhishyComment(bool isEvil)
    {



    }

}
