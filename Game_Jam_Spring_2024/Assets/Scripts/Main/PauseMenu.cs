using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public Canvas pauseMenu;
    AudioSource clickSound;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.enabled = false;
        clickSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPauseButton()
    {
        clickSound.Play();
        Time.timeScale = 0;
        pauseMenu.enabled = true;
    }

    public void OnResumeButton()
    {
        clickSound.Play();
        Time.timeScale = 1;
        pauseMenu.enabled = false;
    }

    public void OnStartButton()
    {
        clickSound.Play();
        Time.timeScale = 1;
        SceneManager.LoadScene("StartScreen");
    }

    public void onQuitButton()
    {
        clickSound.Play();
        Application.Quit();
    }
}
