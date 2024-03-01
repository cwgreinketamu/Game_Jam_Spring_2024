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
        if (GameObject.Find("GameManager") != null)
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        }
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
            if (GameObject.Find("GameManager") != null)
            {
                Invoke("End", 1);
            }
            //close task and return to game successfully here
        }
    }

    private void End()
    {
        gameManager.EndTask(taskId);
    }
}
