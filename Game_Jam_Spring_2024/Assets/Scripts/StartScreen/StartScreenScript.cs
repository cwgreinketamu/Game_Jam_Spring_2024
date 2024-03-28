using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenScript : MonoBehaviour
{
    AudioSource yipi;
    // Start is called before the first frame update
    void Start()
    {
        yipi = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onStartButton()
    {
        StartCoroutine(PlaySoundThenLoadScene());
    }
    

    public void onQuitButton()
    {
        Application.Quit();
    }

    private IEnumerator PlaySoundThenLoadScene()
    {
        yipi.Play();
        while (yipi.isPlaying)
        {
            yield return null;
        }
        SceneManager.LoadScene("Main");
    }

}
