using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravityFlipperScript : MonoBehaviour
{
    private float timer;
    public float delay = 1;
    private GameObject[] gravityFlippers;
    public AudioSource audioSource;
    public AudioClip sound;

    void Start()
    {
        timer = 0;
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (timer == 0 || Time.time - timer > delay)
            {
                GravityFlip();
                audioSource.PlayOneShot(sound);
                ChangeFlipperColor();
            }
            timer = Time.time;
        }
    }

    void GravityFlip()
    {
        Physics2D.gravity *= -1f;
    }

    void ChangeFlipperColor()
    {
        gravityFlippers = GameObject.FindGameObjectsWithTag("GravityFlipper");
        foreach (GameObject flipper in gravityFlippers)
        {
            SpriteRenderer flipperSpriteRenderer = flipper.GetComponent<SpriteRenderer>();
            if (flipperSpriteRenderer != null)
            {
                Vector3 localScaleFlipper = flipper.transform.localScale;
                localScaleFlipper.y *= -1f;
                flipper.transform.localScale = localScaleFlipper;
                if (localScaleFlipper.y > 0f)
                {
                    flipperSpriteRenderer.color = Color.blue;
                }
                else
                {
                    flipperSpriteRenderer.color = Color.red;
                }
            }
        }
    }
}
