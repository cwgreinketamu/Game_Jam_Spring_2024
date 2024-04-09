using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityFlipperScript : MonoBehaviour
{
    public float delay = 1f;
    private float lastFlipTime;

    public AudioSource audioSource;
    public AudioClip sound;

    // Reference to the player's sprite renderer
    private SpriteRenderer playerSpriteRenderer;

    void Start()
    {
        lastFlipTime = Time.time - delay; // Ensure immediate flip is possible
        audioSource = GetComponent<AudioSource>();

        // Find the player's sprite renderer
        playerSpriteRenderer = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && Time.time - lastFlipTime > delay)
        {
            GravityFlip();
            audioSource.PlayOneShot(sound);
            lastFlipTime = Time.time;
        }
    }

    void GravityFlip()
    {
        // Reverse the gravity
        playerSpriteRenderer.flipY = !playerSpriteRenderer.flipY;
        ChangeFlipperColor();
        Physics2D.gravity *= -1f;

        // Flip the player's sprite
        // Change flipper color based on gravity direction

    }

    void ChangeFlipperColor()
    {
        GameObject[] gravityFlippers = GameObject.FindGameObjectsWithTag("GravityFlipper");
        foreach (GameObject flipper in gravityFlippers)
        {
            SpriteRenderer flipperSpriteRenderer = flipper.GetComponent<SpriteRenderer>();
            flipperSpriteRenderer.flipY = !flipperSpriteRenderer.flipY;
            if (flipperSpriteRenderer != null)
            {
                if (Physics2D.gravity.y > 0f)
                {
                    flipperSpriteRenderer.color = Color.blue; // Flipped upwards
                }
                else
                {
                    flipperSpriteRenderer.color = Color.red; // Flipped downwards
                }
            }
        }
    }
}
