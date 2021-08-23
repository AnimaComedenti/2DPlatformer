using UnityEngine;

/// <summary>
/// Controls how blocks with 
/// </summary>
public class ShroomBlockHandler : QuestionBlockBehaviiour
{
    private SpriteRenderer parentSpriteRender;
    public GameObject shroomItem;
    public GameObject fireItem;

    private void Update()
    {
        parentSpriteRender = gameObject.GetComponentInParent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag(Constants.PLAYER_TAG))
        {

            GetComponent<AudioSource>().Play();
            parentSpriteRender.sprite = usedBoxSprite;

            if (collision.collider.GetComponent<PlayerMovement>().LifeState > PlayerMovement.LS_SMALL)
            {
                SpawnNotMoveItem(fireItem);
            }
            else
            {
                SpawnMoveItem(shroomItem, collision.collider.GetComponent<PlayerMovement>().LookingDir);
            }

            Destroy(this);
        }
    }
}
