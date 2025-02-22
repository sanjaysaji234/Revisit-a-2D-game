using UnityEngine;

public class StringShake : MonoBehaviour
{
    public float shakeIntensity = 0.1f;
    public float shakeSpeed = 50f;
    public float decayRate = 2f;

    private string targetSequence = "ceafdb"; // Set the target sequence you want to check for
    private string currentInput = "";     // The player's current input
    private bool isTyping = false;        // Flag to check if the player is typing

    public GameObject player;
    private PlayerInput movement; 
    private Animator animator;
    private PlayerFootsteps footsteps;
    public Scene4to5 scene4To5;

    public GameObject stringA;
    public GameObject stringB;
    public GameObject stringC;
    public GameObject stringD;
    public GameObject stringE;
    public GameObject stringF;

    public AudioClip soundA;
    public AudioClip soundB;
    public AudioClip soundC;
    public AudioClip soundD;
    public AudioClip soundE;
    public AudioClip soundF;

    private AudioSource audioSource;

    private Vector3 originalPosA;
    private Vector3 originalPosB;
    private Vector3 originalPosC;
    private Vector3 originalPosD;
    private Vector3 originalPosE;
    private Vector3 originalPosF;

    void Start()
    {
        

        footsteps = player.GetComponentInChildren<PlayerFootsteps>();
        animator = player.GetComponentInChildren<Animator>();
        movement = player.GetComponent<PlayerInput>();
        audioSource = gameObject.AddComponent<AudioSource>();

        
        originalPosA = stringA.transform.localPosition;
        originalPosB = stringB.transform.localPosition;
        originalPosC = stringC.transform.localPosition;
        originalPosD = stringD.transform.localPosition;
        originalPosE = stringE.transform.localPosition;
        originalPosF = stringF.transform.localPosition;
    }

    void Update()
    {
        footsteps.enabled = false;
        animator.enabled = false;
        movement.enabled = false;

        if (Input.GetKeyDown(KeyCode.A)) { Shake(stringA, originalPosA); PlaySound(soundA); }
        if (Input.GetKeyDown(KeyCode.B)) { Shake(stringB, originalPosB); PlaySound(soundB); }
        if (Input.GetKeyDown(KeyCode.C)) { Shake(stringC, originalPosC); PlaySound(soundC); }
        if (Input.GetKeyDown(KeyCode.D)) { Shake(stringD, originalPosD); PlaySound(soundD); }
        if (Input.GetKeyDown(KeyCode.E)) { Shake(stringE, originalPosE); PlaySound(soundE); }
        if (Input.GetKeyDown(KeyCode.F)) { Shake(stringF, originalPosF); PlaySound(soundF); }

        // Listen for key presses and add them to currentInput
        if (Input.GetKeyDown(KeyCode.A))
        {
            AddToInput("a");
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            AddToInput("b");
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            AddToInput("c");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            AddToInput("d");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            AddToInput("e");
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            AddToInput("f");
        }

    }

    void Shake(GameObject stringObj, Vector3 originalPos)
    {
        if (stringObj != null)
        {
            StartCoroutine(ShakeCoroutine(stringObj, originalPos));
        }
    }

    System.Collections.IEnumerator ShakeCoroutine(GameObject stringObj, Vector3 originalPos)
    {
        float timer = 0.5f;
        while (timer > 0)
        {
            float offset = Mathf.Sin(Time.time * shakeSpeed) * shakeIntensity;
            stringObj.transform.localPosition = originalPos + new Vector3(0, offset, 0);
            timer -= Time.deltaTime;
            yield return null;
        }
        stringObj.transform.localPosition = originalPos;
    }

    void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
    private void AddToInput(string key)
    {
        if (!isTyping)
        {
            isTyping = true;  // Start the typing sequence
            currentInput = ""; // Clear the current input string
        }

        currentInput += key; // Add the pressed key to the input string

        // Check if the input matches the target sequence
        if (currentInput == targetSequence)
        {
            
            scene4To5.guitarplayed = true;
            ResetInput();  // Reset after success
            gameObject.SetActive(false);
        }
        else if (!targetSequence.StartsWith(currentInput))
        {
            Debug.Log("Wrong sequence, reset!");  // Incorrect order, reset
            ResetInput();  // Reset if the sequence is wrong
        }
    }

    private void ResetInput()
    {
        currentInput = "";  // Clear current input
        isTyping = false;    // Reset the typing flag
        Debug.Log("Reset the input due to incorrect sequence.");
    }
}
