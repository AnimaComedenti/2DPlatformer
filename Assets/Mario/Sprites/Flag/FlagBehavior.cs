using UnityEngine;

public class FlagBehavior : MonoBehaviour
{
    public GameObject playAgain;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(Constants.PLAYER_TAG))
        {
            return;
        }

        PlayerMovement pm = collision.GetComponent<PlayerMovement>();
        pm.UsePole(collision.bounds.size.x / 1.5f);

        playAgain.SetActive(true);

        //Destroy this script instance
        Destroy(this);
    }
}
