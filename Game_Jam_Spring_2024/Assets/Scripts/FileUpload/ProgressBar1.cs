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

    AudioSource progressBarSound;
    AudioSource errorSound;

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
        progressBarSound = GetComponents<AudioSource>()[0];
        errorSound = GetComponents<AudioSource>()[1];
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
        progressBarSound.Play();
        for (int i = 0; i < 5; ++i)
        {
            progressBar.fillAmount += 0.10f;
            yield return new WaitForSeconds(0.5f);
        }
        progressBarSound.Stop();
        errorSound.Play();
        panel.SetActive(true);
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
