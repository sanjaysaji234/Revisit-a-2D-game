using UnityEngine;

public class GamePauseOnCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object is the player
        if (collision.CompareTag("Player"))
        {
            // Get the PlayerInput script to check if the player is crouching
            PlayerInput playerInput = collision.GetComponent<PlayerInput>();

            if (playerInput != null && !playerInput.isCrouch)
            {
                // Pause the game
                Time.timeScale = 0f;
                Debug.Log("Game Paused! Player was not crouching.");
            }
        }
    }
}
