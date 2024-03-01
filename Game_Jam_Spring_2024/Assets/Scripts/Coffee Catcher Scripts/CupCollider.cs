using UnityEngine;

public class CupCollider : MonoBehaviour
{
    [SerializeField] private int dropletsToCollect = 10; // Number of droplets to collect
    private int dropletsCollected = 0; // Counter for collected droplets
    private GameManagerScript gameManager;
    private int taskId = 3;

    // Property to check if all droplets are collected
    public bool AllDropletsCollected => dropletsCollected >= dropletsToCollect;
    private bool done = false;

    private void Start()
    {
        if (GameObject.Find("GameManager") != null)
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object is a coffee droplet
        if (collision.CompareTag("CoffeeDroplet"))
        {
            // Destroy the coffee droplet GameObject
            Destroy(collision.gameObject);

            // Increment the droplet counter
            dropletsCollected++;

            // Check if the desired number of droplets is collected
            if (AllDropletsCollected && !done)
            {
                // Perform necessary actions when all droplets are collected
                Debug.Log("All droplets collected!");
                done = true;
                if (GameObject.Find("GameManager") != null)
                {
                    Invoke("End", 1);
                }
            }
        }
    }

    private void End()
    {
        gameManager.EndTask(taskId);
    }
}
