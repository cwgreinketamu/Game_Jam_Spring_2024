using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SquareScript : MonoBehaviour
{
    private BoxCollider2D coll;
    public GameManagerScript gameManager;
    public int taskId = 3;
    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        coll.enabled = true;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!coll.enabled)
            {
                coll.enabled = true;
            }
            else
            {
                coll.enabled = false;
            }
            
        }
    }

    private void OnMouseDown()
    {
        gameManager.StartTask(taskId);
    }
}
