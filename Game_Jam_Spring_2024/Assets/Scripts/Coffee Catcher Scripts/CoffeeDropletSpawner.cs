using System.Collections;
using UnityEngine;

public class CoffeeDropletSpawner : MonoBehaviour
{
    [SerializeField] private float spawnRate = 1f;
    [SerializeField] private GameObject coffeeDropPrefab;
    [SerializeField] private int totalDropletsToSpawn = 50; // Total number of droplets to spawn
    private int dropletsSpawned = 0; // Counter for spawned droplets
    private CupCollider cupCollider; // Reference to the cup collider script

    private void Start()
    {
        cupCollider = FindObjectOfType<CupCollider>(); // Find the CupCollider script in the scene
        StartCoroutine(SpawnCoffeeDroplets());
    }

    private IEnumerator SpawnCoffeeDroplets()
    {
        while (dropletsSpawned < totalDropletsToSpawn && !cupCollider.AllDropletsCollected)
        {
            // Calculate a random spawn position within the defined spawn area
            Vector3 spawnPosition = new Vector3(
                Random.Range(-5f, 5f),
                transform.position.y,
                0f
            );

            // Spawn a coffee droplet at the calculated position
            GameObject droplet = Instantiate(coffeeDropPrefab, spawnPosition, Quaternion.identity);

            // Wait for the defined spawn rate before spawning the next droplet
            yield return new WaitForSeconds(spawnRate);

            dropletsSpawned++; // Increment the counter
        }
    }
}
