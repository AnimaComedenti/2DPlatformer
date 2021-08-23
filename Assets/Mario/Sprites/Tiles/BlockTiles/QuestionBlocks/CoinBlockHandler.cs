using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the behavior of the coin block
/// </summary>
public class CoinBlockHandler : QuestionBlockBehaviiour
{
    private SpriteRenderer parentSpriteRender;
    private bool isUnactive = false;
    Text currentLives;
    Text score;
    public Animator animator;
    public float coinsMax = 1;
    private float coins = 0;

    private void Update()
    {
        parentSpriteRender = gameObject.GetComponentInParent<SpriteRenderer>();
        currentLives = GameObject.Find("currentLives").GetComponent<Text>();
        score = GameObject.Find("score").GetComponent<Text>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.transform.CompareTag(Constants.PLAYER_TAG) || isUnactive)
        {
            return;
        }

        GetComponent<AudioSource>().Play();
        coins++;
        if (coins == coinsMax)
        {
            isUnactive = true;
            parentSpriteRender.sprite = usedBoxSprite;
        }
        animator.SetTrigger("gotTriggerd");
        SetUI();
    }

    /// <summary>
    /// Updates the UI
    /// </summary>
    private void SetUI()
    {
        int currentScore = int.Parse(score.text);
        int livesATM = int.Parse(currentLives.text);
        currentScore++;
        if (currentScore == 100)
        {
            livesATM++;
            currentLives.text = "" + livesATM;
            score.text = "" + 0;
        }
        else
        {
            score.text = "" + currentScore;
        }
    }

}
