using UnityEngine;

/// <summary>
/// Moves a object with a parallax effect
/// </summary>
public class Parallax : MonoBehaviour
{
    private float length;
    private float startpos;

    public Camera cam;
    public float parallaxEffect;

    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position.x;
        cam = Camera.main;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            length = sr.bounds.size.x;
        }
        else
        {
            length = 0;
        }
    }

    void Update()
    {
        // if player is dead cam will be null.
        if (cam == null)
        {
            cam = Camera.main;
            return;
        }

        float temp = cam.transform.position.x * (1 - parallaxEffect); // how far we have moved realtive to the camera
        float dist = cam.transform.position.x * parallaxEffect; // how far we have moved in world space

        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

        if (temp > startpos + length)
        {
            startpos += length;
        }
        else if (temp < startpos - length)
        {
            startpos -= length;
        }
    }
}
