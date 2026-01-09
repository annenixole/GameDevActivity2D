using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;  // Movement speed (units per sec)
    private float jumpingPower = 16f;  //upward velocity applied when jumping
    private bool isFacingRight = true;     //current facing direction of the player

    [SerializeField] private Rigidbody2D rb;  //physics-based movement
    [SerializeField] private Transform groundCheck; //check if the player is touching the ground
    [SerializeField] private LayerMask groundLayer; //ground checker


    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");  //(A/D or Left/Right arrows)

        if ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.UpArrow)) && IsGrounded()) //space & upArrow keyboard
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower); //x = horizontal speed
        }

        if ((Input.GetButtonUp("Jump") || Input.GetKeyUp(KeyCode.UpArrow)) && rb.linearVelocity.y > 0f) // for short jump
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }

        Flip(); 
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y); //horizontal velocity while maintain current vertical velocity
    }
   
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            // Toggle facing direction
            isFacingRight = !isFacingRight;
            
            // Flip the sprite by inverting the X scale
            Vector3 localScale = transform.localScale;
            localScale.x = localScale.x * -1; //Multiply X by -1 to flip
            transform.localScale = localScale;
        }
    }
}