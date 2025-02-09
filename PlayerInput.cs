using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private Rigidbody2D rb;
    public float movespeed = 2f;
    private float walkspeed;
    public Vector2 moveInput;
    private bool isRight = true;
    public bool onGround = true;
    public bool isJumping = false; // Track jump initiation
    [SerializeField] private float jumpForce = 50f;
    public bool isCrouch = false;
    private bool canCrouch = false; // Can only crouch when inside a "Hide" object

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            walkspeed = movespeed;
            movespeed *= 1.8f;
        }
        if (context.canceled)
        {
            movespeed = walkspeed;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && onGround && !isCrouch) // Prevent jumping while crouching
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); // Apply jump force
            isJumping = true; // Initiate jump
            onGround = false; // Player is no longer on the ground
        }
    }


    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.started && onGround && canCrouch) // Only crouch if inside a "Hide" object
        {
            isCrouch = true;
        }
        if (context.canceled)
        {
            isCrouch = false;
        }
    }

    private void HandleMovement()
    {
        if (moveInput.x > 0 && !isRight)
        {
            Flip();
        }
        if (moveInput.x < 0 && isRight)
        {
            Flip();
        }

        // Walk and Run
        Vector2 moveDirection = transform.right * moveInput;
        rb.linearVelocity = new Vector2(movespeed * moveDirection.x, rb.linearVelocity.y);
    }

    private void Flip()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
        isRight = !isRight;
    }

    // Detect Ground & Hide Areas
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
            if (rb.linearVelocity.y <= 0)
            {
                isJumping = false;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }
    }

    // Detect if the player is inside a Hide object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hide"))
        {
            canCrouch = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Hide"))
        {
            canCrouch = false;
            isCrouch = false; // Automatically stop crouching when leaving the hiding spot
        }
    }
}
