using UnityEngine;
using UnityEngine.SceneManagement;

public class DotController : MonoBehaviour
{
    public GameObject dotPrefab; // Reference to the next dot prefab
    public Vector2[] dotPositions; // Preset dot positions
    public int currentIndex = 1; // Index of the current dot position
    SpriteRenderer sprite;

    private GameManagerScript gameManager;
    private int taskId = 5;

    private bool canSpawn = true; // Flag to control dot spawning

    private bool clicked = false;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        if (GameObject.Find("GameManager") != null)
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        }
    }

    void Update()
    {
        if (canSpawn && Input.GetMouseButton(0)) // Check if the left mouse button is held down
        {
            clicked = true;
            // Convert mouse position to world coordinates
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Check if the mouse position overlaps with any collider
            Collider2D[] colliders = Physics2D.OverlapPointAll(mousePos);
            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject == gameObject)
                {
                    canSpawn = false; // Prevent further spawning until next dot is clicked
                    SpawnNextDot();
                    DisableCollider(collider); // Disable the collider of the object hit by the raycast
                    break; // Exit loop once a valid collider is found
                }
            }
        }
        if (canSpawn && !Input.GetMouseButton(0) && clicked)
        {
            Debug.Log("failed");
            if (GameObject.Find("GameManager") != null)
            {
                gameManager.ResetMinigame(taskId);
            }
            
        }
    }

    void SpawnNextDot()
    {
        if (currentIndex < dotPositions.Length)
        {
            GameObject newDot = Instantiate(dotPrefab, dotPositions[currentIndex], Quaternion.identity);
            newDot.GetComponent<DotController>().currentIndex = currentIndex + 1; // Increment index for the new dot
            newDot.GetComponent<DotController>().clicked = true;
        }
        else
        {
            Debug.Log("All dots have been spawned.");
            if (GameObject.Find("GameManager") != null)
            {
                Invoke("End", 1);
            }
        }
    }

    void DisableCollider(Collider2D collider)
    {
        // Check if the collider is a CircleCollider2D
        CircleCollider2D circleCollider = collider.GetComponent<CircleCollider2D>();
        sprite.color = Color.blue;
        if (circleCollider != null)
        {
            circleCollider.enabled = false; // Disable the collider
        }
    }

    private void End()
    {
        gameManager.EndTask(taskId);
    }
}
