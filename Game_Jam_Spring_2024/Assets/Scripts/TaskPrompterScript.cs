using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskPrompterScript : MonoBehaviour
{
    private static BoxCollider2D coll;
    public GameManagerScript gameManager;
    public int taskId = -1;
    private float timer;
    public float interval = 45f;
    private bool timerEnabled;
    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        coll.enabled = false;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        timerEnabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerEnabled)
        {
            if (Time.time - interval > timer)
            {
                coll.enabled = false;
                gameManager.DeactivateTask(taskId);
            }
        }
    }

    private void OnMouseDown()
    {
        gameManager.StartTask(taskId);
        timerEnabled = false;
    }

    public int GetTaskID()
    {
        return taskId;
    }

    public void ActivateTask()
    {
        coll.enabled = true;
        timer = Time.time;
        Debug.Log("task " + taskId + " started");
        timerEnabled = true;
    }

    public void ResetTimer()
    {
        timer = Time.time;
        timerEnabled = true;
    }

    public void StopTimer()
    {
        timerEnabled = false;
    }
}
