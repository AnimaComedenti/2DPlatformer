using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls how the coins behave
/// </summary>
public class CoinHandler : MonoBehaviour
{
    private Text currentLives;
    private Text score;

    public new AudioClip audio;

    void Start()
    {
        currentLives = GameObject.Find("currentLives").GetComponent<Text>();
        score = GameObject.Find("score").GetComponent<Text>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Constants.PLAYER_TAG))
        {
            AudioSource.PlayClipAtPoint(audio, this.gameObject.transform.position);
            SetUI();
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Updates the coin and lives fields in the UI
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
