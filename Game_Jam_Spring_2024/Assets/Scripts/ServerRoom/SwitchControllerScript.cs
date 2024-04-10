using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchControllerScript : MonoBehaviour
{

    public int counter = 0;
    public int switchCount = 6;

    private GameManagerScript gameManager;
    private int taskId = 7;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("GameManager") != null)
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseCounter()
    {
        ++counter;
        if (counter == switchCount)
        {
            Debug.Log("All switches on");
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
