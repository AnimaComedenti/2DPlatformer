using UnityEngine;

/// <summary>
/// Controls how the Gumba enemy behaves
/// </summary>
public class GumbaBehavior : LeftRightMovement, IKillable
{
    public float walkSpeedAnimation;
    public float walkSpeedMovement = 5f;
    public Transform groundCheck;
    public LayerMask ground;
    public float groundCheckRadius = 0.2f;
    public new AudioClip audio;

    private Animator animator;
    private bool isGrounded = true;
    

    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = Constants.ENEMY_TAG;

        animator = GetComponent<Animator>();
        if (walkSpeedAnimation <= 0)
        {
            walkSpeedAnimation = 1;
        }
        animator.SetFloat("WalkSpeed", walkSpeedAnimation);
        SetSpeed(walkSpeedMovement);
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, ground);
        SetShouldMove(isGrounded);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(Constants.PLAYER_TAG))
        {
            collision.collider.GetComponent<IKillable>().Kill();
            ChangeDir();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isGrounded && !collision.CompareTag(Constants.PLAYER_TAG))
        {
            ChangeDir();
        }
    }

    public void Kill()
    {
        isGrounded = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().isKinematic = true;
        animator.SetTrigger("Die");
        AudioSource.PlayClipAtPoint(audio, this.gameObject.transform.position);
    }

    /// <summary>
    /// Changes the walking direction and walking animation
    /// </summary>
    private void ChangeDir()
    {
        ChangeDirection();
        animator.SetTrigger("ChangeDirection");
    }
}
