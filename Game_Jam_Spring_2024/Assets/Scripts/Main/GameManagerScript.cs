using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManagerScript : MonoBehaviour
{
    private List<int> unusedTasks = new List<int>(){1,2,3,4,5,9,10,-2,-4}; //add 5, 6, and 9 when minigames are done, 7 and 8 are pt2/pt3 of 6
    //-1 is coffee catcher pt2, memo pt1/pt2 is -2/-3
    private List<int> activatedTasks = new List<int>();
    private List<int> inProgressTasks = new List<int>();
    private List<int> finishedTasks = new List<int>();
    private List<int> reusableTasks = new List<int>() { 1, 2, 3, 4, 5, 9, 10, -2};

    private GameObject[] prompters;
    private static float totalProgress = 0; //used to track progress bar between days
    
    private float timer;
    private bool counting = false; //used to track if the timer is counting down

    public float dayLength = 60.0f;

    public bool miniSceneActive;

    public ProgressBar progressBar;

    public TMPro.TMP_Text timerText;

    public GameObject clipboard;

    // Start is called before the first frame update
    void Start()
    {
        timer = dayLength;
        miniSceneActive = false;
        prompters = GameObject.FindGameObjectsWithTag("Prompter");
        ActivateTask(-4);
        progressBar = GameObject.Find("ProgressBar").GetComponent<ProgressBar>();
        progressBar.SetBar(totalProgress); 
        timerText.text = string.Format("");
    }

    // Update is called once per frame
    void Update()
    {
        if (counting)
        {
            timer -= Time.deltaTime;
            updateTimer(timer);

            if (timer < 0)
            {
                EndDay();
                timer = dayLength;
            }
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
        clipboard.GetComponent<ClipboardScript>().AddText(taskId);
        unusedTasks.Remove(taskId);
        activatedTasks.Add(taskId);

    }

    public void StartTask(int taskId) //prompter is clicked
    {
        if (!miniSceneActive)
        {
            if (taskId > 0)
            {
                for (int i = 0; i < activatedTasks.Count; i++)
                {
                    if (activatedTasks[i] == taskId)
                    {
                        inProgressTasks.Add(taskId);
                        activatedTasks.RemoveAt(i);
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
                if (taskId == -2 || taskId == -4)
                {
                    activatedTasks.Remove(taskId);
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
                        prompters[j].GetComponent<TaskPrompterScript>().PlayTaskCompleteSound();
                    }
                }
                finishedTasks.Add(taskId);
                inProgressTasks.RemoveAt(i);
                Debug.Log("Calling removeText cuz finished");
                clipboard.GetComponent<ClipboardScript>().RemoveText(taskId);
                Debug.Log("Calling removeText successful");
                if (taskId > 0)
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
                    if (taskId == -4)
                    {
                        counting = true;
                        ActivateTask(unusedTasks[Random.Range(0, unusedTasks.Count)]);
                        ActivateTask(unusedTasks[Random.Range(0, unusedTasks.Count)]);
                    }
                    if (unusedTasks.Count > 0)
                    {
                        ActivateTask(unusedTasks[Random.Range(0, unusedTasks.Count)]);
                    }
                    else
                    {
                        Debug.Log("All tasks completed");
                        bool flag = true;
                        while (flag)
                        {
                            int reusedTaskIndex = Random.Range(0, finishedTasks.Count); //index of task to reuse by moving to unusedTasks then activating
                            if (finishedTasks[reusedTaskIndex] == taskId) //makes sure it doesnt instantly generate the task that was just finished
                            {
                                continue;
                            }
                            for (int k = 0; k < reusableTasks.Count; ++k)
                            {
                                if (finishedTasks[reusedTaskIndex] == reusableTasks[k]) //checks that the randomly choosen finished task is reusable
                                {
                                    unusedTasks.Add(finishedTasks[reusedTaskIndex]);
                                    ActivateTask(finishedTasks[reusedTaskIndex]);
                                    finishedTasks.RemoveAt(reusedTaskIndex);
                                    flag = false;
                                    break;
                                }
                            }
                        }
                    }
                }
                //task succeeded, add to progress bar here
                progressBar.IncreaseBar(taskId); //the argument passed in is the percentage of the progress bar to increase by
            }
        }
        if (miniSceneActive)
        {
            Debug.Log("Scene not found! TaskId: " + taskId);
        }
    }

    public void DeactivateTask(int taskId) //timer ran out
    {
        for (int i = 0; i < activatedTasks.Count; i++)
        {
            if (activatedTasks[i] == taskId)
            {
                finishedTasks.Add(taskId);
                activatedTasks.RemoveAt(i);
                clipboard.GetComponent<ClipboardScript>().RemoveText(taskId);
                Debug.Log("Task " + taskId + " failed");
                //task failed, subtract from progress bar here
                progressBar.DecreaseBar(taskId); //The argument passed in is the percentage of the progress bar to reduce
                if (unusedTasks.Count > 0)
                {
                    ActivateTask(unusedTasks[Random.Range(0, unusedTasks.Count)]);
                }
                else
                {
                    Debug.Log("All tasks completed");
                    bool flag = true;
                    while (flag)
                    {
                        int reusedTaskIndex = Random.Range(0, finishedTasks.Count); //index of task to reuse by moving to unusedTasks then activating
                        if (finishedTasks[reusedTaskIndex] == taskId) //makes sure it doesnt instantly generate the task that was just finished
                        {
                            continue;
                        }
                        for (int k = 0; k < reusableTasks.Count; ++k)
                        {
                            if (finishedTasks[reusedTaskIndex] == reusableTasks[k]) //checks that the randomly choosen finished task is reusable
                            {
                                unusedTasks.Add(finishedTasks[reusedTaskIndex]);
                                ActivateTask(finishedTasks[reusedTaskIndex]);
                                finishedTasks.RemoveAt(reusedTaskIndex);
                                flag = false;
                                break;
                            }
                        }
                    }
                }
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

    public void ResetMinigame(int taskId)
    {
        SceneManager.UnloadSceneAsync(taskId);
        SceneManager.LoadScene(taskId, LoadSceneMode.Additive);
    }

    public void IncreaseProgress(float value)
    {
        totalProgress += value;
    }

    public void DecreaseProgress(float value)
    {
        totalProgress -= value;
    }

    public void SetProgess(float value)
    {
        totalProgress = value;
    }
}
