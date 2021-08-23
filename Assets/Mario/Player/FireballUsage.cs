using UnityEngine;

/// <summary>
/// Controls when the player can use fireballs
/// </summary>
public class FireballUsage : MonoBehaviour
{
    public float duration = 10f; // time in sec
    public GameObject fireball;
    public float cooldown = 1f;
    public float offest = 1f;
    public float fireballSpeed = 5f;

    private bool isActive = true;
    private float remainingDuration = 0f;
    private float remainingCooldown = 0f;
    private PlayerMovement pm;
    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        pm = GetComponent<PlayerMovement>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // don't do anything if not active
        if (!isActive)
        {
            return;
        }

        if (remainingDuration > 0)
        {
            remainingDuration -= Time.deltaTime;
        }
        else
        {
            DeactivateFireballUsage();
            return;
        }

        if (remainingCooldown > 0)
        {
            remainingCooldown -= Time.deltaTime;
            return;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            remainingCooldown = cooldown;
            SpawnFireball();
        }
    }

    /// <summary>
    /// Creates a new fireball and throws it zo the player's looking direction
    /// </summary>
    private void SpawnFireball()
    {
        // create fireball
        float xPos = transform.position.x + offest * pm.LookingDir;
        Vector3 spawnPos = new Vector3(xPos, transform.position.y-0.5f, transform.position.z);
        GameObject instance = Instantiate(fireball, spawnPos, Quaternion.identity);

        // throw to looking direction
        Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(fireballSpeed * pm.LookingDir, 0f);
    }

    /// <summary>
    /// Deactivates the usage of the fireball
    /// </summary>
    public void DeactivateFireballUsage()
    {
        isActive = false;
        sr.color = new Color(1, 1, 1);
        isActive = false;
    }

    /// <summary>
    /// Activates the usage of the fireball
    /// </summary>
    public void ActivateFireballUsage()
    {
        remainingDuration = duration;
        remainingCooldown = 0f;
        isActive = true;
        sr.color = new Color(1, 0.5f, 0.5f);
        pm.MakePowerUp();
    }
}
