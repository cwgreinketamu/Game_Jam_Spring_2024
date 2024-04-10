using System.Collections;
using UnityEngine;

public class PlaytestMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of player movement
    private Rigidbody2D rb;
    private GameManagerScript gameManager;
    private int taskId = 9;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (GameObject.Find("GameManager") != null)
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        }
    }

    void Update()
    {

        // Move player horizontally
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        //check player height to see if task complete
        if (transform.position.y > 57.5f)
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
