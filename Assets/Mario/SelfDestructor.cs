using UnityEngine;

/// <summary>
/// Destroys the <c>GameObject</c> this script is attached to.
/// </summary>
/// <remarks>
/// Doesn't play any death animations.
/// </remarks>
public class SelfDestructor : MonoBehaviour
{
    public bool destroyBelowY = false;
    public float yValue;

    // Update is called once per frame
    void Update()
    {
        if (destroyBelowY && transform.position.y <= yValue)
        {
            SelfDestruction();
        }
    }

    /// <summary>
    /// Destoys the object this script is attched to and respawns it if possible
    /// </summary>
    public void SelfDestruction()
    {
        IRespawnable respawnable = GetComponent<IRespawnable>();
        if (respawnable != null)
        {
            respawnable.Respawn();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
