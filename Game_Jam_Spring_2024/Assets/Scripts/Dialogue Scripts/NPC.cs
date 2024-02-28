using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPC : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private string[] dialogue;
    private int index;

    [SerializeField] private GameObject contButton;
    [SerializeField] private float wordSpeed = 0.0001f;
    private bool playerIsClose;
    private bool isTyping; // Flag to track if dialogue is currently typing
    private bool dialogueActive; // Flag to track if dialogue is active

    // Reference to the player movement script
    [SerializeField] private PlayerMovement playerMovementScript;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose && !isTyping && !dialogueActive)
        {
            StartDialogue();
        }

        if (dialogueText.text == dialogue[index])
        {
            contButton.SetActive(true);
        }
    }

    void StartDialogue()
    {
        dialoguePanel.SetActive(true);
        dialogueActive = true;
        // Disable player movement when dialogue starts
        playerMovementScript.enabled = false;
        // Stop player movement
        Rigidbody2D playerRigidbody = playerMovementScript.GetComponent<Rigidbody2D>();
        playerRigidbody.velocity = Vector2.zero;
        StartCoroutine(Typing());
    }

    public void ZeroText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
        isTyping = false; // Reset typing flag
        dialogueActive = false; // Reset dialogue active flag
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
            ZeroText();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
        }
    }
}
