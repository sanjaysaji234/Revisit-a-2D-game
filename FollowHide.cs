using UnityEngine;

public class FollowPlayerWhenCrouching : MonoBehaviour
{
    public Transform player; // Assign the player in the Inspector
    private PlayerInput playerInput;

    private void Start()
    {
        if (player != null)
        {
            playerInput = player.GetComponent<PlayerInput>();
           
        }
    }

    private void Update()
    {
        if (playerInput != null && playerInput.isCrouch)
        {
            // Only change the X position if the player is moving
            float newX = player.position.x ;

            if (playerInput.moveInput.x != 0) // If the player is moving
            {
                // Update X position based on player movement
                transform.position = new Vector3(newX, transform.position.y, transform.position.z);
            }
            else
            {
                // Keep the object at the offset when the player stops moving
                transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
            }
        }
    }
}
