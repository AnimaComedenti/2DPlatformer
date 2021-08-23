using UnityEngine;

public class Respawner : MonoBehaviour
{
    private float distance = 1f; // time in seconds
    private float remaining = 0f;
    private PlayerMovement pm;
    private PosCollection positions = new PosCollection();
    private Transform target;

    public GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.name = Constants.PLAYER_SPAWNER_NAME;
    }

    // Update is called once per frame
    // Keep track of the atteched GameObject position
    void Update()
    {
        if (remaining > 0)
        {
            remaining -= Time.deltaTime;
            return;
        }

        if (pm != null && pm.isGrounded)
        {
            positions.Add(target.position);
            remaining = distance;
        }
    }

    /// <summary>
    /// Attaches a GameObject to this Respawner
    /// </summary>
    /// <param name="goTarget">
    /// The gameobject to be attached to this Respawner
    /// </param>
    public void AttachToRespawner(GameObject goTarget)
    {
        target = goTarget.transform;
        pm = goTarget.GetComponent<PlayerMovement>();
        positions.Fill(target.position);
    }

    /// <summary>
    /// Creates a new instances of the assigned prefab
    /// </summary>
    public void Spawn()
    {
        Instantiate(prefab, positions.GetOldest(), Quaternion.identity);
    }

    /// <summary>
    /// Class to collect multiple Vetor3
    /// </summary>
    private class PosCollection
    {
        private readonly int size = 5;

        private int pointer = 0;
        private Vector3[] oldPos;

        public PosCollection()
        {
            oldPos = new Vector3[size];
        }

        /// <summary>
        /// Fills the Collection initially with a certain Vector3
        /// </summary>
        /// <param name="vector">
        /// The Vector3 to be used
        /// </param>
        public void Fill(Vector3 vector)
        {
            for (int i = 0; i < size; i++)
            {
                oldPos[i] = vector;
            }
        }

        /// <summary>
        /// Replaces the oldest Vector3 with the new one
        /// </summary>
        /// <param name="vector">
        /// The new Vector3
        /// </param>
        public void Add(Vector3 vector)
        {
            oldPos[pointer] = vector;
            pointer = (pointer + 1) % size;
        }

        /// <summary>
        /// Returns the oldest Vector3
        /// </summary>
        /// <returns>
        /// The oldest vector3
        /// </returns>
        public Vector3 GetOldest()
        {
            Vector3 vec = oldPos[(pointer + 1) % size];
            return new Vector3(vec.x, vec.y + 0.1f, vec.z);
        }
    }
}
