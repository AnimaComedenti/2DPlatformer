using UnityEngine;

/// <summary>
/// Controls the behavior of the fireflower block
/// </summary>
public class FireFlowerBlock : QuestionBlockBehaviiour
{
    private SpriteRenderer parentSpriteRender;
    public GameObject fireItem;

    private void Start()
    {
        parentSpriteRender = gameObject.GetComponentInParent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.transform.CompareTag(Constants.PLAYER_TAG))
        {
            return;
        }

        GetComponent<AudioSource>().Play();
        parentSpriteRender.sprite = usedBoxSprite;
        SpawnNotMoveItem(fireItem);
        Destroy(this);
    }
}
