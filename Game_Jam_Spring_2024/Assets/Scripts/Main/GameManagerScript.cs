using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManagerScript : MonoBehaviour
{
    private List<int> unusedTasks = new List<int>(){1,2,3,4,10,-2}; //add 5, 6, and 9 when minigames are done, 7 and 8 are pt2/pt3 of 6
    //-1 is coffee catcher pt2, memo pt1/pt2 is -2/-3
    private List<int> inProgressTasks = new List<int>();
    private List<int> finishedTasks = new List<int>();
    private GameObject[] prompters;
    private int tasksCompleted = 0; //used to generate tasks on each individual day
    private static int totalTasksCompleted = 0; //used to track progress bar between days
    private float timer;

    public float dayLength = 60.0f;

    public bool miniSceneActive;

    public static ProgressBar progressBar;

    public TMPro.TMP_Text timerText;
    // Start is called before the first frame update
    void Start()
    {
        timer = dayLength;
        miniSceneActive = false;
        prompters = GameObject.FindGameObjectsWithTag("Prompter");
        ActivateTask(unusedTasks[Random.Range(0, unusedTasks.Count)]);
        progressBar = GameObject.Find("ProgressBar").GetComponent<ProgressBar>();
        progressBar.IncreaseBar(0.1f * totalTasksCompleted); 
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        updateTimer(timer);
        if (timer < 0)
        {
            EndDay();
            timer = dayLength;
        }
    }

    public void ActivateTask(int taskId) //task is given to player
    {
        Debug.Log("Task " + taskId + " started");
        for (int i = 0; i < prompters.Length; ++i)
        {
            if (prompters[i].GetComponent<TaskPrompterScript>().GetTaskID() == taskId)
            {
                prompters[i].GetComponent<TaskPrompterScript>().ActivateTask();
            }
        }

    }

    public void StartTask(int taskId) //prompter is clicked
    {
        if (!miniSceneActive)
        {
            if (taskId > 0)
            {
                for (int i = 0; i < unusedTasks.Count; i++)
                {
                    if (unusedTasks[i] == taskId)
                    {
                        inProgressTasks.Add(taskId);
                        unusedTasks.RemoveAt(i);
                        SceneManager.LoadScene(taskId, LoadSceneMode.Additive);
                        miniSceneActive = true;
                        Debug.Log("Scene " + taskId + " loaded");
                        break;
                    }
                }
                if (!miniSceneActive)
                {
                    Debug.Log("Scene not found! TaskId: " + taskId);
                }
            }
            else
            {
                if (taskId == -2)
                {
                    unusedTasks.Remove(taskId);
                }
                inProgressTasks.Add(taskId);
                EndTask(taskId);
            }
        }
    }

    public void EndTask(int taskId) //minigame completed
    {
        for (int i = 0; i < inProgressTasks.Count; i++)
        {
            if (inProgressTasks[i] == taskId)
            {
                for (int j = 0; j < prompters.Length; ++j)
                {
                    if (prompters[j].GetComponent<TaskPrompterScript>().GetTaskID() == taskId)
                    {
                        prompters[j].GetComponent<TaskPrompterScript>().DisableArrow();
                    }
                }
                finishedTasks.Add(taskId);
                inProgressTasks.RemoveAt(i);
                if(taskId > 0)
                {
                    SceneManager.UnloadSceneAsync(taskId);
                }
                Debug.Log("Scene " + taskId + " unloaded");
                miniSceneActive = false;
                //if task is a part 1 of 2, start part 2
                if(taskId == 3) //coffee
                {
                    ActivateTask(-1);
                }
                else if(taskId == -2) //memo
                {   
                    ActivateTask(-3);
                }
                else
                {
                    ActivateTask(unusedTasks[Random.Range(0, unusedTasks.Count)]);
                }
                //task succeeded, add to progress bar here
                progressBar.IncreaseBar(0.1f); //the argument passed in is the percentage of the progress bar to increase by
                ++tasksCompleted;
                ++totalTasksCompleted;
            }
        }
        if (miniSceneActive)
        {
            Debug.Log("Scene not found! TaskId: " + taskId);
        }
    }

    public void DeactivateTask(int taskId) //timer ran out
    {
        for (int i = 0; i < unusedTasks.Count; i++)
        {
            if (unusedTasks[i] == taskId)
            {
                finishedTasks.Add(taskId);
                unusedTasks.RemoveAt(i);
                Debug.Log("Task " + taskId + " failed");
                //task failed, subtract from progress bar here
                progressBar.DecreaseBar(0.1f); //The argument passed in is the percentage of the progress bar to reduce
                ++tasksCompleted;
                ++totalTasksCompleted;
                ActivateTask(unusedTasks[Random.Range(0, unusedTasks.Count)]);
                break;
            }
        }
    }

    public void EndDay()
    {
         SceneManager.LoadScene("EndOfDay", LoadSceneMode.Single);
    }

    public void updateTimer(float currentTime)
    {
        currentTime += 1;
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        timerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
