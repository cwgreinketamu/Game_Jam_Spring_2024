using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugSpawning : MonoBehaviour
{
    [SerializeField] private GameObject bugPrefab;
    [SerializeField] private int bugsToSpawn = 3; // Maximum number of enemies to spawn
    private int bugsKilled = 0;
    private GameManagerScript gameManager;
    private int taskId = 2;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        for (int i = 0; i < bugsToSpawn; ++i)
        {
            Vector3 randomSpawnPosition = new Vector3(Random.Range(-4.5f, 4.5f), Random.Range(-2.5f, 2.5f), 0);
            Instantiate(bugPrefab, randomSpawnPosition, Quaternion.identity);
        }
    }

    private void Update()
    {

    }
    
    public void KillBug()
    {
        ++bugsKilled;
        if (bugsKilled == bugsToSpawn)
        {
            Debug.Log("All bugs killed!");
            gameManager.EndTask(taskId);
            //close task and return to game successfully here
        }
    }
}
