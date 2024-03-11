using UnityEngine;

public class DotSpawner : MonoBehaviour
{
    public GameObject dotPrefab;
    public Vector2[] dotPositions; // Preset dot positions
    private int dotsSpawned = 0;

    void Start()
    {
        // Spawn the first dot at the initial position
        SpawnDot(dotPositions[0]);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Mouse"))
        {
            // If the collider of the mouse object hits a dot, spawn the next dot
            if (dotsSpawned < dotPositions.Length - 1)
            {
                dotsSpawned++;
                SpawnDot(dotPositions[dotsSpawned]);
                Debug.Log("Dot Spawned. Total Dots: " + (dotsSpawned + 1));
            }
            else
            {
                Debug.Log("All dots have been spawned.");
            }
        }
    }

    void SpawnDot(Vector2 position)
    {
        Instantiate(dotPrefab, position, Quaternion.identity);
    }
}
