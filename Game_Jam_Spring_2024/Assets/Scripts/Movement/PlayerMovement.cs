using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D body;

    float horizontal;
    float vertical;
    float moveLimit = 0.7f;

    public float runSpeed = 20.0f;

    public GameManagerScript gameManager;

    void Start ()
    {
        body = GetComponent<Rigidbody2D>(); 
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
    }

    void Update ()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical"); 
    }

    private void FixedUpdate()
    {  
        if (!gameManager.miniSceneActive)
        {
            //the below if statement is unnecessary if the player is using a controller as controllers already account for diagonal movement. 
            if (horizontal != 0 && vertical != 0)
            {
                horizontal *= moveLimit;
                vertical *= moveLimit;
            }

            body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
        }
    }


}
