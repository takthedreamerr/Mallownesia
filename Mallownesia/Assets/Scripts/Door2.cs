using UnityEngine;

public class Door2 : MonoBehaviour
{
    [Header("Door Animation")]
    public Animator animator;
    private string isOpenParam = "IsOpen";

    [Header("References")]
    public Collider doorCollider; // Main blocking collider
    public Collider openTrigger; // Trigger to open the door
    public Collider closeTrigger; // Trigger to close the door

    [Header("UI Prompt")]
    public GameObject pressEText; // "Press E to open" text

    private bool isOpen = false;
    private bool playerNear = false;

    private void Start()
    {
        // Initialize UI
        if (pressEText != null)
            pressEText.SetActive(false);

        // Setup colliders
        if (doorCollider != null)
            doorCollider.isTrigger = false;

        if (openTrigger != null)
            openTrigger.isTrigger = true;

        if (closeTrigger != null)
            closeTrigger.isTrigger = true;

        // Auto-find animator
        if (animator == null)
            animator = GetComponent<Animator>();

        // Ensure door starts closed
        if (animator != null)
            animator.SetBool(isOpenParam, false);
    }

    private void Update()
    {
        // Open door when E is pressed and player is near open trigger
        if (playerNear && Input.GetKeyDown(KeyCode.E))
        {
            OpenDoor();
        }
    }

    public void OpenDoor()
    {
        if (isOpen) return;

        if (animator != null)
        {
            animator.SetBool(isOpenParam, true);
            Debug.Log("SimpleDoor: Playing open animation");
        }

        // Disable blocking collider
        if (doorCollider != null)
        {
            doorCollider.enabled = false;
        }

        // Hide press E text
        if (pressEText != null)
        {
            pressEText.SetActive(false);
        }

        isOpen = true;
        SoundManager.PlaySound(SoundType.Door);
        Debug.Log("SimpleDoor: Door opened!");
    }

    public void CloseDoor()
    {
        if (!isOpen) return;

        if (animator != null)
        {
            animator.SetBool(isOpenParam, false);
            Debug.Log("SimpleDoor: Playing close animation");
        }

        // Re-enable blocking collider
        if (doorCollider != null)
        {
            doorCollider.enabled = true;
        }

        isOpen = false;
        SoundManager.PlaySound(SoundType.Door);
        Debug.Log("SimpleDoor: Door closed!");
    }

    public bool IsOpen()
    {
        return isOpen;
    }

    // Open trigger - for opening the door
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isOpen)
        {
            playerNear = true;

            if (pressEText != null)
            {
                pressEText.SetActive(true);
            }

            Debug.Log("SimpleDoor: Player near open trigger");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;

            if (pressEText != null)
            {
                pressEText.SetActive(false);
            }

            Debug.Log("SimpleDoor: Player left open trigger");
        }
    }

    // Close trigger - for closing the door (separate method)
    public void OnCloseTriggerEnter()
    {
        CloseDoor();
        Debug.Log("SimpleDoor: Close trigger activated");
    }
}
