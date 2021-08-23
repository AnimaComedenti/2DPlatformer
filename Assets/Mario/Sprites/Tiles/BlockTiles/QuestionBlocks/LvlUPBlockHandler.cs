using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// Contrls how the level up block behaves
/// </summary>
public class LvlUPBlockHandler : QuestionBlockBehaviiour
{
    private SpriteRenderer parentSpriteRender;
    public GameObject shroomItem;
    private Tilemap tilemap;
    private Vector3 parentLocation;
    private Vector3Int parentTilemapLocation;
    public TileBase usedQuestionblock;

    void Start()
    {
        parentSpriteRender = gameObject.GetComponentInParent<SpriteRenderer>();
        tilemap = GameObject.Find("Lvl_ground_blocks").GetComponent<Tilemap>();
        parentLocation = transform.parent.gameObject.transform.position;
        parentTilemapLocation = new Vector3Int((int)parentLocation.x, (int)parentLocation.y, 0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(Constants.PLAYER_TAG) && collision.transform.position.y < parentLocation.y)
        {
            GetComponent<AudioSource>().Play();
            parentSpriteRender.sprite = usedBoxSprite;
            SpawnMoveItem(shroomItem, collision.GetComponent<PlayerMovement>().LookingDir);
            tilemap.SetTile(parentTilemapLocation, usedQuestionblock);
            Destroy(this);
        }
    }
}
