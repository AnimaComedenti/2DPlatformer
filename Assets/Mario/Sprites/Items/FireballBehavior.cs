using UnityEngine;

/// <summary>
/// Controls how the fireball behaves
/// </summary>
public class FireballBehavior : MonoBehaviour
{
    public float duration = 10f;
    public int maxBounces = 10;
    public new AudioClip audio;

    // Update is called once per frame
    void Update()
    {
        if (duration < 0)
        {
            SelfDestruction();
        }
        duration -= Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D collider = collision.collider;
        AudioSource.PlayClipAtPoint(audio, this.gameObject.transform.position);

        // Kill enemy
        if (collider.CompareTag(Constants.ENEMY_TAG))
        {
            collider.GetComponent<IKillable>().Kill();
            SelfDestruction();
        }

        if (--maxBounces <= 0)
        {
            SelfDestruction();
        }
    }

    /// <summary>
    /// Destroys this object
    /// </summary>
    private void SelfDestruction()
    {
        GetComponent<SelfDestructor>().SelfDestruction();
    }
}
