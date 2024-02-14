using UnityEngine;

public class CupCollider : MonoBehaviour
{
    [SerializeField] private int dropletsToCollect = 10; // Number of droplets to collect
    private int dropletsCollected = 0; // Counter for collected droplets

    // Property to check if all droplets are collected
    public bool AllDropletsCollected => dropletsCollected >= dropletsToCollect;
    private bool done = false;

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
            }
        }
    }
}
