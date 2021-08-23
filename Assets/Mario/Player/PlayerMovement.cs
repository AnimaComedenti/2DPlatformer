using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls most player aspects, despite its name
/// </summary>
public class PlayerMovement : MonoBehaviour, IKillable, IRespawnable
{
    // states for the player life
    public static readonly int LS_DEAD = 0;
    public static readonly int LS_SMALL = 1;
    public static readonly int LS_BIG = 2;
    public static readonly int LS_POWERUP = 3;

    public new Camera camera;
    public float movementspeed;
    public float jumpHeight;
    public SpriteRenderer playSprite;
    public bool isGrounded = true;
    public Animator animator;
    private Rigidbody2D rg;
    public float gravity = -9.81f;
    public float poleSlideSpeed = 5f;
    public new AudioClip audio;

    private Respawner respawnControl;

    private float lookingDir = 1f; // -1 = left, 1 = right
    private int lifeState;
    private bool isOnPole = false;

    private Text lives;

    public float LookingDir { get => lookingDir; }
    public int LifeState { get => lifeState; }

    void Start()
    {
        gameObject.tag = Constants.PLAYER_TAG;
        rg = gameObject.GetComponent<Rigidbody2D>();
        lifeState = LS_SMALL;
        respawnControl = GameObject.Find(Constants.PLAYER_SPAWNER_NAME).GetComponent<Respawner>();
        respawnControl.AttachToRespawner(gameObject);
        lives = GameObject.Find("currentLives").GetComponent<Text>();
    }

    void Update()
    {
        camera.transform.position = new Vector3(transform.position.x, 5f, camera.transform.position.z);

        if (isOnPole || lifeState == LS_DEAD)
        {
            return;
        }

        //Jumping 
        if (isGrounded)
        {
            animator.SetBool("isJumping", false);
            Jump();
        }
        else
        {
            animator.SetBool("isJumping", true);
        }
    }

    private void FixedUpdate()
    {
        if (lifeState == LS_DEAD)
        {
            return;
        }

        if (!isGrounded && isOnPole)
        {
            rg.velocity = new Vector2(0, 0);
            float newYPos = transform.position.y - poleSlideSpeed * Time.fixedDeltaTime;
            transform.position = new Vector3(transform.position.x, newYPos, transform.position.z);
            return;
        }

        isOnPole = false;
        animator.SetBool("usePole", false);

        float axes = Input.GetAxis("Horizontal");

        //Animation and sprite rotation
        ChangeSpriteDirection(axes);
        animator.SetFloat("running", Mathf.Abs(axes * movementspeed));
        rg.velocity = new Vector2(movementspeed * axes, rg.velocity.y);
    }

    private void ChangeSpriteDirection(float direction)
    {
        if (direction < 0)
        {
            playSprite.flipX = true;
            lookingDir = -1;
        }
        if (direction > 0)
        {
            playSprite.flipX = false;
            lookingDir = 1;
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<AudioSource>().Play();
            rg.AddForce(Vector2.up * jumpHeight);
        }
    }

    public void Kill()
    {
        
        if (lifeState == LS_POWERUP)
        {
            GetComponent<FireballUsage>().DeactivateFireballUsage();
        }
        else if (lifeState == LS_BIG)
        {
            transform.localScale = new Vector3(2f, 2f, 1f);
        }
        else if (lifeState == LS_SMALL)
        {
            Respawn();
        }
        AudioSource.PlayClipAtPoint(audio, this.gameObject.transform.position);

        lifeState--;
    }

    public void Respawn()
    {
        int currentLives = int.Parse(lives.text);
        if (currentLives > 0)
        {
            lives.text = "" + --currentLives;
            respawnControl.Spawn();
            Destroy(gameObject);
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
            GameObject.Find("Canvas").transform.Find("PlayAgain").gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Increaes the player size and life state
    /// </summary>
    public void MakeBig()
    {
        lifeState = LS_BIG;
        transform.localScale = new Vector3(2.15f, 2.35f, 1f);
    }

    /// <summary>
    /// Increaes the player size and life state for Power Ups
    /// </summary>
    public void MakePowerUp()
    {
        lifeState = LS_POWERUP;
        transform.localScale = new Vector3(2.15f, 2.35f, 1f);
    }

    /// <summary>
    /// Lets the player use a pole
    /// </summary>
    /// <param name="xPosition">
    /// The approximate distance from collision point to where the player should be placed
    /// </param>
    public void UsePole(float xPosition)
    {
        transform.position = new Vector3(transform.position.x + xPosition, transform.position.y, transform.position.z);
        isOnPole = true;
        animator.SetBool("usePole", true);
        rg.velocity = new Vector2(0, 0);
    }
}
