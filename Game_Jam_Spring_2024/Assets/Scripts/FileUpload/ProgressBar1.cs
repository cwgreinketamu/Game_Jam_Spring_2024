using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar1 : MonoBehaviour
{
    public Image progressBar;
    private GameManagerScript gameManager;
    private int taskId = 6;
    public GameObject eventSystem;
    public GameObject panel;

    // Start is called before the first frame update
    void Start()
    {
        progressBar = GetComponent<Image>();
        panel.SetActive(false);
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
        for (int i = 0; i < 5; ++i)
        {
            progressBar.fillAmount += 0.10f;
            yield return new WaitForSeconds(1);
        }
        panel.SetActive(true);
        if (GameObject.Find("GameManager") != null)
        {
            Invoke("End", 3);
        }
    }

    private void End()
    {
        gameManager.EndTask(taskId);
    }

}
