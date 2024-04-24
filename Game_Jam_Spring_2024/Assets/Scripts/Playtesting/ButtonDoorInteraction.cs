using UnityEngine;

public class ButtonDoorInteraction : MonoBehaviour
{
    public GameObject door; // Reference to the door GameObject
    public SpriteRenderer buttonSpriteRenderer; // Reference to the button's SpriteRenderer component
    private SpriteRenderer doorSpriteRenderer; // Reference to the door's SpriteRenderer component
    private BoxCollider2D doorCollider; // Reference to the door's BoxCollider2D component
    public bool isDoorOpen = false; // Flag to track whether the door is open or closed
    private bool done = false;

    AudioSource buttonSound;
    
    void Start()
    {
        // Get the SpriteRenderer and BoxCollider2D components of the door
        doorSpriteRenderer = door.GetComponent<SpriteRenderer>();
        doorCollider = door.GetComponent<BoxCollider2D>();
        buttonSound = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !done)
        {
            buttonSound.Play();
            // Toggle door state
            ToggleDoor();
            done = true;
            // Disable button sprite
            if (buttonSpriteRenderer != null)
                buttonSpriteRenderer.enabled = false;
        }
    }

    void ToggleDoor()
    {
        // Toggle the door's state
        isDoorOpen = !isDoorOpen;

        // If the door is open, deactivate the sprite renderer and collider
        if (isDoorOpen)
        {
            doorSpriteRenderer.enabled = false;
            doorCollider.enabled = false;
        }
        else // If the door is closed, activate the sprite renderer and collider
        {
            doorSpriteRenderer.enabled = true;
            doorCollider.enabled = true;
        }
    }
}
