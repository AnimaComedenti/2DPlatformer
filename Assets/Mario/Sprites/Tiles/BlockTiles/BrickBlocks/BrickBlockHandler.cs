using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// Controls how BrickBlock behave
/// </summary>
public class BrickBlockHandler : MonoBehaviour
{
    private Tilemap tilemap;
    private Vector3 parentLocation;
    private Vector3Int parentTilemapLocation;
    public new AudioClip audio;

    private void Start()
    {
        tilemap = GameObject.Find("Lvl_ground_blocks").GetComponent<Tilemap>();
        parentLocation = transform.parent.gameObject.transform.position;
        parentTilemapLocation = new Vector3Int((int)parentLocation.x, (int)parentLocation.y, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.transform.CompareTag(Constants.PLAYER_TAG))
        {
            return;
        }

        // The player must have collected a power up to destroy this block
        if (collision.collider.GetComponent<PlayerMovement>().LifeState > PlayerMovement.LS_SMALL)
        {
            AudioSource.PlayClipAtPoint(audio, this.gameObject.transform.position);
            tilemap.SetTile(parentTilemapLocation, null);
            Destroy(transform.parent.gameObject);
        }
    }
}
