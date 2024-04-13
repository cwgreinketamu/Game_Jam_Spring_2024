using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskPrompterScript : MonoBehaviour
{
    private BoxCollider2D coll;
    public GameManagerScript gameManager;
    public int taskId = -1;
    private float timer = 0f;
    public float interval = 45f;
    private bool timerEnabled;
    private ArrowScript arrowScript;
    private SpriteRenderer sprite;

    public static ProgressBar progressBar;

    AudioSource taskComplete;
    // Start is called before the first frame update
    void Awake()
    {
        coll = GetComponent<BoxCollider2D>();
        coll.enabled = false;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        timerEnabled = false;
        arrowScript = GetComponentInChildren<ArrowScript>();
        progressBar = GameObject.Find("ProgressBar").GetComponent<ProgressBar>();
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = Color.white;
        taskComplete = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timerEnabled)
        {
            timer += Time.deltaTime;
            if (timer > interval)
            {
                coll.enabled = false;
                gameManager.DeactivateTask(taskId);
                arrowScript.Disable();
                sprite.color = Color.white;
                timer = 0f;
            }
            else if (timer > 2 * interval / 3)
            {
                arrowScript.SetColor("Red");
            }
            else if (timer > interval / 3)
            {
                arrowScript.SetColor("Yellow");
            }
            else
            {
                arrowScript.SetColor("Green");
            }
        }
    }

    private void OnMouseDown()
    {
        gameManager.StartTask(taskId);
        arrowScript.Disable();
        timerEnabled = false;
        coll.enabled = false;
        sprite.color = Color.white;
    }

    public int GetTaskID()
    {
        return taskId;
    }

    public void ActivateTask()
    {
        coll.enabled = true;
        timer = 0f;
        //Debug.Log("task " + taskId + " started");
        timerEnabled = true;
        arrowScript.Enable();
        arrowScript.SetColor("Green");
        sprite.color = Color.magenta;
    }

    public void ResetTimer()
    {
        timer = 0f;
        timerEnabled = true;
    }

    public void StopTimer()
    {
        timerEnabled = false;
    }

    public void DisableArrow()
    {
        arrowScript.Disable();
    }

    public void PlayTaskCompleteSound()
    {
        taskComplete.Play();
    }
}
