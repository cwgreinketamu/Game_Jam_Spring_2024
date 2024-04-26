using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
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
    public TaskCollScript taskCollScript;
    AudioSource taskComplete;
    public Sprite normalSprite;
    public Sprite highlightedSprite;
    public Animator anim;
    private bool clicked;

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
        taskComplete = GetComponent<AudioSource>();
        taskCollScript = GetComponentInChildren<TaskCollScript>();
        if (taskId == -1)
        {
            anim = GetComponent<Animator>();
            anim.SetBool("Highlight", false);
        }
        clicked = false;
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
                taskCollScript.SetColl(false);
                gameManager.DeactivateTask(taskId);
                arrowScript.Disable();
                if (taskId == -1)
                {
                    anim.SetBool("Highlight", false);
                }
                else
                {
                    sprite.sprite = normalSprite;
                }
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
        //Debug.Log("task collider worked");
        Clicked();
    }

    public void Clicked()
    {
        if (!clicked)
        {
            clicked = true;
        }
        else
        {
            return;
        }
        if (taskCollScript.inRange)
        {
            gameManager.StartTask(taskId);
            arrowScript.Disable();
            timerEnabled = false;
            coll.enabled = false;
            if (taskId == -1)
            {
                anim.SetBool("Highlight", false);
            }
            else
            {
                sprite.sprite = normalSprite;
            }
            taskCollScript.SetColl(false);
        }
        else
        {
            Debug.Log("not in range");
        }
    }

    public int GetTaskID()
    {
        return taskId;
    }

    public void ActivateTask()
    {
        coll.enabled = true;
        taskCollScript.SetColl(true);
        timer = 0f;
        //Debug.Log("task " + taskId + " started");
        timerEnabled = true;
        arrowScript.Enable();
        arrowScript.SetColor("Green");
        if (taskId == -1)
        {
            anim.SetBool("Highlight", true);
        }
        else
        {
            sprite.sprite = highlightedSprite;
        }
        clicked = false;
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
