using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    private List<int> unusedTasks = new List<int>();
    private List<int> inProgressTasks = new List<int>();
    private List<int> finishedTasks = new List<int>();
    public bool miniSceneActive;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i < 5; i++)
        {
            unusedTasks.Add(i);
        }
        miniSceneActive = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
                    finishedTasks.Add(taskId);
                    inProgressTasks.RemoveAt(i);
                    SceneManager.UnloadSceneAsync(taskId);
                    Debug.Log("Scene " + taskId + " unloaded");
                    miniSceneActive = false;
                    break;
                }
            }
            if (miniSceneActive)
            {
                Debug.Log("Scene not found! TaskId: " + taskId);
            }
        }
    }
}
