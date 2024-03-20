using System.Collections;
using UnityEngine;

public class PlaytestMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of player movement
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        // Move player horizontally
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

    }

}
