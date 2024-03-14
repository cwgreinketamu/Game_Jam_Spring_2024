using System.Collections;
using UnityEngine;

public class PlaytestMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of player movement
    public float dashForce = 10f; // Force applied when dashing horizontally
    public float dashDuration = 0.1f; // Duration of the dash
    public float dashCooldown = 0.5f; // Cooldown time between dashes
    public Transform groundCheck; // Reference to a ground check object
    public LayerMask groundLayer; // LayerMask for identifying ground objects

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool canDash = true;
    private bool hasDashedInAir = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Check if the player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        Debug.Log("Grounded: " + isGrounded);

        // Move player horizontally
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Dashing
        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || !hasDashedInAir) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        // Disable gravity while dashing
        rb.gravityScale = 0f;

        // Prevent dashing until cooldown is over
        canDash = false;

        // Mark that the player has dashed in the air if they were not grounded
        if (!isGrounded)
            hasDashedInAir = true;

        // Calculate the direction of the dash
        float dashDirection = Mathf.Sign(rb.velocity.x);

        // Gradually increase the velocity over time for the duration of the dash
        float timer = 0f;
        while (timer < dashDuration)
        {
            // Calculate the current dash speed based on time
            float dashSpeed = Mathf.Lerp(0, dashForce, timer / dashDuration);

            // Apply dash force horizontally
            rb.velocity = new Vector2(rb.velocity.x + dashDirection * dashSpeed, rb.velocity.y);

            // Increment timer
            timer += Time.deltaTime;
            yield return null;
        }

        // Enable gravity again after dashing
        rb.gravityScale = 3f;

        // Wait for cooldown before allowing another dash
        yield return new WaitForSeconds(dashCooldown);

        // Allow dashing again
        canDash = true;

        // Reset dashed in air flag when grounded
        if (isGrounded)
            hasDashedInAir = false;
    }
}
