using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPlaytest : MonoBehaviour
{

    private GameManagerScript gameManager;
    private int taskId = 9;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("GameManager") != null)
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Flag"))
        {
            Debug.Log("Hit flag pole");
            if (GameObject.Find("GameManager") != null)
            {
                Invoke("End", 1);
            }
        }
    }

    private void End()
    {
        gameManager.EndTask(taskId);
    }
}
