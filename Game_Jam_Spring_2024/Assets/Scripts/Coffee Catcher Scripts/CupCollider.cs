using UnityEngine;

public class CupCollider : MonoBehaviour
{
    [SerializeField] private int dropletsToCollect = 10; // Number of droplets to collect
    private int dropletsCollected = 0; // Counter for collected droplets
    private GameManagerScript gameManager;
    private int taskId = 3;
    public Sprite newSprite; // New sprite to change to
    private bool done = false;

    // Property to check if all droplets are collected
    public bool AllDropletsCollected => dropletsCollected >= dropletsToCollect;

    AudioSource drop;

    private void Start()
    {
        if (GameObject.Find("GameManager") != null)
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        }
        drop = GetComponent<AudioSource>();
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

            //play droplet sound
            //drop.time = 0.2f;
            drop.Play();

            // Check if the desired number of droplets is collected
            if (AllDropletsCollected && !done)
            {
                // Perform necessary actions when all droplets are collected
                Debug.Log("All droplets collected!");
                done = true;
                GetComponent<SpriteRenderer>().sprite = newSprite;
                if (GameObject.Find("GameManager") != null)
                {
                    End();
                }
            }
        }
    }

    private void End()
    {
        // Call GameManager to end task
        gameManager.EndTask(taskId);
    }
}
