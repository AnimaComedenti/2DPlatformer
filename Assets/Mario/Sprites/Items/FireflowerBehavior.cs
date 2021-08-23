using UnityEngine;

/// <summary>
/// Controls how the fireflower behaves
/// </summary>
public class FireflowerBehavior : MonoBehaviour
{
    public new AudioClip audio;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(Constants.PLAYER_TAG))
        {
            AudioSource.PlayClipAtPoint(audio, this.gameObject.transform.position);
            collision.collider.GetComponent<FireballUsage>().ActivateFireballUsage();
            Destroy(gameObject);
        }
    }
}
