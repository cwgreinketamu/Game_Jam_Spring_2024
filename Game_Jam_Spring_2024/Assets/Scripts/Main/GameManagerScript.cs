using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManagerScript : MonoBehaviour
{
    private List<int> unusedTasks = new List<int>(){1,2,3,4,5,6,8,10};
    private List<int> inProgressTasks = new List<int>();
    private List<int> finishedTasks = new List<int>();
    private GameObject[] prompters;
    private int tasksCompleted = 0; //used to generate tasks on each individual day
    private static int totalTasksCompleted = 0; //used to track progress bar between days
    private float timer = 0.0f;

    public float dayLength = 60.0f;

    public bool miniSceneActive;

    public static ProgressBar progressBar;
    // Start is called before the first frame update
    void Start()
    {
        miniSceneActive = false;
        prompters = GameObject.FindGameObjectsWithTag("Prompter");
        ActivateTask(unusedTasks[Random.Range(0, 4)]); //replace 4 with unusedTasks.Count
        progressBar = GameObject.Find("ProgressBar").GetComponent<ProgressBar>();
        progressBar.IncreaseBar(0.1f * totalTasksCompleted); 
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > dayLength)
        {
            EndDay();
            timer = 0.0f;
        }
    }

    public void ActivateTask(int taskId)
    {
        for (int i = 0; i < prompters.Length; ++i)
        {
            if (prompters[i].GetComponent<TaskPrompterScript>().GetTaskID() == taskId)
            {
                prompters[i].GetComponent<TaskPrompterScript>().ActivateTask();
            }
        }
    }

    public void StartTask(int taskId)
    {
        if (!miniSceneActive)
        {
            for (int i = 0; i < unusedTasks.Count; i++)
            {
                if (unusedTasks[i] == taskId)
                {
                    inProgressTasks.Add(taskId);
                    unusedTasks.RemoveAt(i);
                    SceneManager.LoadScene(taskId, LoadSceneMode.Additive);
                    Debug.Log("Scene " + taskId + " loaded");
                    miniSceneActive = true;
                    break;
                }
            }
            if (!miniSceneActive)
            {
                Debug.Log("Scene not found! TaskId: " + taskId);
            }
        }
    }

    public void EndTask(int taskId)
    {
        if (miniSceneActive)
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
                    SceneManager.UnloadSceneAsync(taskId);
                    Debug.Log("Scene " + taskId + " unloaded");
                    miniSceneActive = false;
                    //task succeeded, add to progress bar here
                    progressBar.IncreaseBar(0.1f); //the argument passed in is the percentage of the progress bar to increase by
                    ++tasksCompleted;
                    ++totalTasksCompleted;
                    ActivateTask(unusedTasks[Random.Range(0, 4-tasksCompleted)]); //replace 5 with unusedTasks.Count
                    break;
                }
            }
            if (miniSceneActive)
            {
                Debug.Log("Scene not found! TaskId: " + taskId);
            }
        }
    }

    public void DeactivateTask(int taskId)
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
                ActivateTask(unusedTasks[Random.Range(0, 4-tasksCompleted)]); //replace 4 with unusedTasks.Count
                break;
            }
        }
    }

    public void EndDay()
    {
         SceneManager.LoadScene("EndOfDay", LoadSceneMode.Single);
    }
}
