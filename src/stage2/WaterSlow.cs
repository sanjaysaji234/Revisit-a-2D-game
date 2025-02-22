using UnityEngine;

public class WaterSlow : MonoBehaviour
{
    private PlayerInput playerInput;  // Reference to the PlayerInput script
    private float originalSpeed;     // Store the original speed
    private float slowSpeed = 1.1f;  // The slow speed when in water

    private void Start()
    {
        // Get the PlayerInput component attached to the player
        playerInput = GetComponent<PlayerInput>();

        // Store the original movement speed
        originalSpeed = playerInput.movespeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Water"))
        {
            // When player enters the water, reduce speed
            playerInput.movespeed = originalSpeed / slowSpeed;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Water"))
        {
            // When player leaves the water, restore original speed
            playerInput.movespeed = originalSpeed;
        }
    }
}
