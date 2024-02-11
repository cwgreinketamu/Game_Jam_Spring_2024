using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpSpawner : MonoBehaviour
{
    [SerializeField] private float spawnRate = 1f;
    [SerializeField] private GameObject[] windowPrefabs;
    [SerializeField] private int maxWindowsToSpawn = 10; // Maximum number of enemies to spawn
    private int windowsSpawned = 0; // Counter for enemies spawned
    private bool canSpawn = true;

    private void Start()
    {
        StartCoroutine(Spawner());
        maxWindowsToSpawn = Random.Range(10, 16);
    }

    private IEnumerator Spawner()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);

        while (canSpawn && windowsSpawned < maxWindowsToSpawn) // Check if still allowed to spawn and not reached max
        {
            yield return wait;

            int rand = Random.Range(0, windowPrefabs.Length);

            GameObject windowToSpawn = windowPrefabs[rand];

            Vector3 randomSpawnPosition = new Vector3(Random.Range(-4.5f, 4.5f), Random.Range(-2.3f, 2.5f), 0);

            Instantiate(windowToSpawn, randomSpawnPosition, Quaternion.identity);

            windowsSpawned++; // Increment the counter
        }
    }
}
