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

    AudioSource bugSounds;
    AudioSource bugDeath;
    AudioSource bugCrawl1;
    AudioSource bugCrawl2;
    AudioSource bugCrawl3;


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

        bugDeath = GetComponents<AudioSource>()[0];
        bugCrawl1 = GetComponents<AudioSource>()[1];
        bugCrawl2 = GetComponents<AudioSource>()[2];
        bugCrawl3 = GetComponents<AudioSource>()[3];
        
        bugCrawl1.Play();
        bugCrawl2.Play();
        bugCrawl3.Play();
    }

    private void Update()
    {
        if (bugsKilled > 0)
        {
            bugCrawl1.Stop();
            if (bugsKilled > 1)
            {
                bugCrawl2.Stop();
                if (bugsKilled > 2)
                {
                    bugCrawl3.Stop();
                }
            }
        }
    }
    
    public void KillBug()
    {
        ++bugsKilled;
        bugDeath.volume = 0.2f;
        bugDeath.Play();
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
