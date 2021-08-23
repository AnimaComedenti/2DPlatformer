using UnityEngine;

/// <summary>
/// Checks if the player is on ground
/// </summary>
public class GroundCheck : MonoBehaviour
{
    public GameObject Player;
    public LayerMask ground;
    public bool killEnemy = false;

    private PlayerMovement pm;

    // Start is called before the first frame update
    void Start()
    {
        Player = gameObject.transform.parent.gameObject;
        pm = Player.GetComponent<PlayerMovement>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if collision with ground, payer is grounded
        if (ground == (ground | (1 << collision.collider.gameObject.layer)))
        {
            pm.isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // if no more collision with ground, player is not grounded
        if (ground == (ground | (1 << collision.collider.gameObject.layer)))
        {
            pm.isGrounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // kill enemies
        if (killEnemy && !pm.isGrounded && collision.CompareTag(Constants.ENEMY_TAG))
        {
            collision.GetComponent<IKillable>().Kill();
        }
    }
}
