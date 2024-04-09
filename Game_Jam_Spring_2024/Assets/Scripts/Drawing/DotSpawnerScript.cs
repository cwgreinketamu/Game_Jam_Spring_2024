using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks.Sources;
using Unity.VisualScripting;
using UnityEngine;

public class DotSpawnerScript : MonoBehaviour
{
    private Vector3[] positions = {
        new Vector3(0, 3),
        new Vector3(-1, 2),
        new Vector3(0, 1),
        new Vector3(-3, 1),
        new Vector3(-0.5f, 0),
        new Vector3(-1.5f, -3.5f),
        new Vector3(0, -1),
        new Vector3(1.5f, -3.5f),
        new Vector3(0.5f, 0),
        new Vector3(3, 1),
        new Vector3(0, 1),
        new Vector3(1, 2),
        new Vector3(0, 3),
    };

    private List<GameObject> Dots = new List<GameObject>();
    private int dotCount = 0;
    public GameObject Dot;
    private GameObject tempDot;
    private bool clicked;
    private bool finished;
    private GameManagerScript gameManager;
    private int taskId = 5;
    private bool waiting;
    // Start is called before the first frame update
    void Start()
    {
        clicked = false;
        finished = false;
        waiting = true;
        SpawnNextDot();
        if (GameObject.Find("GameManager") != null)
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        }
        StartCoroutine("WaitPause");
    }

    // Update is called once per frame
    void Update()
    {
        if (!waiting)
        {
            if (!Input.GetMouseButton(0))
            {
                if (!finished && clicked)
                {
                    //UnityEngine.Debug.Log("failed task");
                    ResetTask();
                }
            }
            else
            {
                if (Input.GetMouseButton(0))
                {
                    clicked = true;
                }
            }
        }
    }

    public void SpawnNextDot()
    {
        UnityEngine.Debug.Log(dotCount + " dot spawned");
        if (dotCount < 13)
        {
            tempDot = Instantiate(Dot, positions[dotCount], Quaternion.identity, this.transform);
            Dots.Add(tempDot);
            ++dotCount;
        }
        else
        {
            UnityEngine.Debug.Log("task complete");
            if (GameObject.Find("GameManager") != null)
            {
                finished = true;
                Invoke("End", 1);
            }
        }
    }

    private void OnMouseDown()
    {
        clicked = true;
        UnityEngine.Debug.Log("clicked is true");
    }

    private void End()
    {
        gameManager.EndTask(taskId);
    }

    private void ResetTask()
    {
        if (GameObject.Find("GameManager") != null)
        {
            gameManager.ResetMinigame(taskId);
        }
    }

    IEnumerator WaitPause()
    {
        yield return new WaitForSeconds(0.75f);
        waiting = false;
    }

    public bool DotDrawn()
    {
        if (clicked)
        {
            SpawnNextDot();
            return true;
        }
        else
        {
            return false;
        }
    }
}
