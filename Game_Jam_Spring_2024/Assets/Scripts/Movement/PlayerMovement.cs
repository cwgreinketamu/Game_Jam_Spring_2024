using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D body;

    float horizontal;
    float vertical;
    //float moveLimit = 0.7f;

    public float walkSpeed = 5.0f;
    public float sprintSpeed = 20.0f;
    public float acceleration = 0.1f;

    public GameManagerScript gameManager;
    private Animator anim;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        if (!gameManager.miniSceneActive)
        {
            // Slow acceleration
            Vector2 moveInput = new Vector2(horizontal, vertical).normalized;
            Vector2 currentVelocity = body.velocity;
            Vector2 targetVelocity = moveInput * (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed);
            Vector2 velocityDifference = targetVelocity - currentVelocity;
            Vector2 accelerationStep = velocityDifference * acceleration;
            Animate(moveInput, currentVelocity, accelerationStep);
            // Apply acceleration
            body.velocity += accelerationStep;

            // Limit velocity to the maximum sprint speed
            if (Input.GetKey(KeyCode.LeftShift))
            {
                body.velocity = Vector2.ClampMagnitude(body.velocity, sprintSpeed);
            }
        }
        else
        {
            body.velocity = Vector2.zero;
        }
    }

    private void Animate(Vector2 moveVector, Vector2 currentVelocity, Vector2 acceleration)
    {
        anim.SetFloat("MovementX", moveVector.x);
        anim.SetFloat("MovementY", moveVector.y);

        if(Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            anim.SetFloat("LastMoveX", acceleration.x);
            anim.SetFloat("LastMoveY", acceleration.y);
            //Debug.Log(Input.GetAxisRaw("Horizontal") + Input.GetAxisRaw("Vertical"));
        }

        
    }
}
