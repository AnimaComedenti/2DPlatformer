using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls how the the muhrooms behave
/// </summary>
public class MushroomHandler : LeftRightMovement
{
    public Transform groundCheck1;
    public Transform groundCheck2;
    public LayerMask ground;
    public float groundCheckRadius = 0.5f;
    private bool isGrounded = true;
    private float startDir = 1f;
    private Text lives;
    public new AudioClip audio;

    private void Start()
    {
        lives = GameObject.Find("currentLives").GetComponent<Text>();
        SetSpeed(2f);
        SetDirection(startDir);
        Invoke(nameof(ShroomDestroy), 20);
    }


    private void Update()
    {
        bool groundCheck1Result = Physics2D.OverlapCircle(groundCheck1.position, groundCheckRadius, ground);
        bool groundcheck2Result = Physics2D.OverlapCircle(groundCheck2.position, groundCheckRadius, ground);
        isGrounded = groundCheck1Result || groundcheck2Result;
        SetShouldMove(isGrounded);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag(Constants.PLAYER_TAG))
        {
            AudioSource.PlayClipAtPoint(audio, this.gameObject.transform.position);
            string spriteName = GetComponent<SpriteRenderer>().sprite.name;
            // if is red mushroom
            if (spriteName == "MarioItems_0")
            {
                PlayerMovement pm = collision.collider.GetComponent<PlayerMovement>();
                pm.MakeBig();
            }
            // if is green mushroom
            if (spriteName == "MarioItems_1")
            {
                int livesATM = int.Parse(lives.text);
                livesATM++;
                lives.text = "" + livesATM;
            }
            ShroomDestroy();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isGrounded && ground == (ground | (1 << collision.gameObject.layer)))
        {
            ChangeDirection();
        }
    }

    /// <summary>
    /// Destroy this object
    /// </summary>
    private void ShroomDestroy()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// Sets the direction in which this mushroom should move
    /// </summary>
    /// <param name="dir">
    /// The direction to move
    /// </param>
    public void SetStartDir(float dir)
    {
        startDir = dir;
    }
}
