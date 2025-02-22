using UnityEngine;
using System.Collections;

public class DemonRise : MonoBehaviour
{
    [SerializeField] private float moveDistance = 2f; // Distance to move up
    [SerializeField] private float moveSpeed = 2f;    // Speed of movement
    [SerializeField] private float moveDelay = 2f;    // Time before moving up
    [SerializeField] private AudioSource audioSource; // AudioSource to play sound

    private Vector3 originalPosition;
    private Vector3 targetPosition;

    private void Start()
    {
        originalPosition = transform.position;
        targetPosition = new Vector3(originalPosition.x, originalPosition.y + moveDistance, originalPosition.z);

        // Start the movement after a delay
        StartCoroutine(StartMovement());
    }

    private IEnumerator StartMovement()
    {
        yield return new WaitForSeconds(moveDelay);

        // Play the audio when the object starts moving
        if (audioSource != null)
        {
            audioSource.Play();
        }

        StartCoroutine(MoveObject(targetPosition, moveSpeed));
    }

    private IEnumerator MoveObject(Vector3 destination, float speed)
    {
        float step = speed * Time.deltaTime;

        // Move smoothly to the target position
        while (Vector3.Distance(transform.position, destination) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, step);
            yield return null; // Wait until the next frame
        }
    }
}
