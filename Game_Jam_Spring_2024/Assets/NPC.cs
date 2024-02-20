using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPC : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public string[] dialogue;
    private int index;

    public GameObject contButton;
    public float wordSpeed = 0.001f;
    public bool playerIsClose;
    private bool isTyping; // Flag to track if dialogue is currently typing

    // Reference to the player movement script
    public PlayerMovement playerMovementScript;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose && !isTyping)
        {
            if (dialoguePanel.activeInHierarchy)
            {
                zeroText();
            }
            else
            {
                dialoguePanel.SetActive(true);
                StartCoroutine(Typing());
                // Disable player movement when dialogue starts
                playerMovementScript.enabled = false;
            }
        }

        if (dialogueText.text == dialogue[index])
        {
            contButton.SetActive(true);
        }
    }

    public void zeroText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
        isTyping = false; // Reset typing flag
        // Re-enable player movement when dialogue ends
        playerMovementScript.enabled = true;
    }


    IEnumerator Typing()
    {
        isTyping = true; // Set typing flag
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
        isTyping = false; // Reset typing flag after typing is done
    }


    public void NextLine()
    {
        contButton.SetActive(false);

        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            zeroText();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
            zeroText();
        }
    }
}
