using UnityEngine;

/// <summary>
/// Controls how question block behave in general
/// </summary>
public class QuestionBlockBehaviiour : MonoBehaviour
{
    public Sprite usedBoxSprite;

    /// <summary>
    /// Spawnes a moving item
    /// </summary>
    /// <param name="item">The item to be spawned</param>
    /// <param name="dir">The direction in which the item should move</param>
    public void SpawnMoveItem(GameObject item, float dir)
    {
        float positionY = transform.position.y + 2f;
        Vector3 position = new Vector3(transform.position.x,positionY,transform.position.z);
        GameObject go = Instantiate(item, position, Quaternion.identity);
        go.GetComponent<MushroomHandler>().SetStartDir(dir);
    }

    /// <summary>
    /// Spawns a non moving item
    /// </summary>
    /// <param name="item">The item to be spawned</param>
    public void SpawnNotMoveItem(GameObject item)
    {
        float positionY = transform.position.y + 2f;
        Vector3 position = new Vector3(transform.position.x, positionY, transform.position.z);
        Instantiate(item, position, Quaternion.identity);
    }
}
