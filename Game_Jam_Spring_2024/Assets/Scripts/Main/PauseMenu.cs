using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public Canvas pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPauseButton()
    {
        Time.timeScale = 0;
        pauseMenu.enabled = true;
    }

    public void OnResumeButton()
    {
        Time.timeScale = 1;
        pauseMenu.enabled = false;
    }

    public void OnStartButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("StartScreen");
    }

    public void onQuitButton()
    {
        Application.Quit();
    }
}
