using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onStartButton()
    {
        SceneManager.LoadScene("Main");
    }

    public void onQuitButton()
    {
        Application.Quit();
    }
}
