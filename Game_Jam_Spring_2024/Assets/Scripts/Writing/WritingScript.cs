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

    // Start is called before the first frame update
    void Start()
    {
        displayText.text = "Start Typing...";
        if (GameObject.Find("GameManager") != null)
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        }
        
    }

    private void Awake()
    {
        keys = new HashSet<KeyCode>((KeyCode[])Enum.GetValues(typeof(KeyCode)));
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var key in keys)
        {
            if (Input.GetKeyDown(key))
            {
                charCount += typingSpeed;
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
    }

    private void End()
    {
        gameManager.EndTask(taskId);
    }
}
