using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar2 : MonoBehaviour
{
    public Image progressBar;
    private GameManagerScript gameManager;
    private int taskId = 8;
    public GameObject eventSystem;

    // Start is called before the first frame update
    void Start()
    {
        progressBar = GetComponent<Image>();
        if (GameObject.Find("GameManager") != null)
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        }
        else
        {
            Instantiate(eventSystem);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ButtonPressed()
    {
        StartCoroutine("FillBar");
    }

    IEnumerator FillBar()
    {
        for (int i = 0; i < 10; ++i)
        {
            progressBar.fillAmount += 0.10f;
            yield return new WaitForSeconds(1);
        }
        if (GameObject.Find("GameManager") != null)
        {
            Invoke("End", 2);
        }
    }

    private void End()
    {
        gameManager.EndTask(taskId);
    }
}
