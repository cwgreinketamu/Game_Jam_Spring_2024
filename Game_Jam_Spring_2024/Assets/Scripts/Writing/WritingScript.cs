using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class WritingScript : MonoBehaviour
{
    private int charCount = 0;
    public string sampleText = "This is sample text for a minigame. The player will type and this text will be produced. This will be replaced with writing that matches the game being created by larry. Until that text is written, this text will exist in its place, as a placeholder. Hopefully the player will be typing all of this out. I don't know what else to type. I've spent too much time doing this.";
    public TMPro.TMP_Text displayText;
    private HashSet<KeyCode> keys;
    public int typingSpeed = 2;

    private GameManagerScript gameManager;
    private int taskId = 10;

    private bool keyPressed = false;

    private bool waiting;
    AudioSource typingSound;

    // Start is called before the first frame update
    void Start()
    {
        typingSound = GetComponent<AudioSource>();
        displayText.text = "Start Typing...";
        if (GameObject.Find("GameManager") != null)
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        }
        typingSound.Play();
        typingSound.time = 1.0f;
        typingSound.Pause();        
    }

    private void Awake()
    {
        keys = new HashSet<KeyCode>((KeyCode[])Enum.GetValues(typeof(KeyCode)));
    }

    // Update is called once per frame
    void Update()
    {
        keyPressed = false;
        foreach (var key in keys)
        {
            if (Input.GetKeyDown(key))
            {
                keyPressed = true;
                charCount += typingSpeed;
                if (!typingSound.isPlaying)
                {
                    typingSound.UnPause();
                }
                if(waiting)
                {
                    StopCoroutine("WaitPause");
                    waiting = false;

                }
 
            }   
        //charCount += numKeys;
        if (charCount <= sampleText.Length && charCount > 0)
        {
            displayText.text = sampleText.Substring(0, charCount);
        }
        if (charCount > sampleText.Length)
        {
            if (GameObject.Find("GameManager") != null)
            {
                Invoke("End", 1);
            }
        }
        if (!waiting && !keyPressed)
        {
            waiting = true;
            StartCoroutine("WaitPause");
        }
    }
    }

    IEnumerator WaitPause()
    {
        yield return new WaitForSeconds(0.75f);
        typingSound.Pause();
        waiting = false;
    }

    private void End()
    {
        gameManager.EndTask(taskId);
    }
}
