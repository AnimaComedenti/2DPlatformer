using UnityEngine;

/// <summary>
/// Lets an object move only to the left and right.
/// Changes direction if needed
/// </summary>
public class LeftRightMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float direction = -1f;
    private float speed = 10f;
    private bool shouldMove = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!shouldMove)
        {
            return;
        }
        Vector2 dir = new Vector2(10 * direction, 0);
        Vector2 newPos = Vector2.MoveTowards(rb.position, rb.position + dir, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);
    }

    /// <summary>
    /// Sets the speed at wich this object should move
    /// </summary>
    /// <param name="newSpeed">The new speed</param>
    protected void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    /// <summary>
    /// Sets if this object should move
    /// </summary>
    /// <param name="shouldMove">Should this object move?</param>
    protected void SetShouldMove(bool shouldMove)
    {
        this.shouldMove = shouldMove;
    }

    /// <summary>
    /// Sets the direction in which this object should move
    /// </summary>
    /// <param name="newDirection">The new direction</param>
    protected void SetDirection(float newDirection)
    {
        direction = newDirection;
    }

    /// <summary>
    /// Changes the direction
    /// </summary>
    protected void ChangeDirection()
    {
        direction *= -1;
    }
}
