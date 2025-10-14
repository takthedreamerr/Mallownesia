using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{
    [Header("Rotation Parameters")]
    public Vector3 openRotation = new Vector3(-90, 0, 0);
    public float animationSpeed = 2f;

    [Header("Key Requirement")]
    public KeyItem requiredKey;

    [Header("Door Type Settings")]
    public bool isKeypadDoor = false;

    private Quaternion closedRotation;
    private Quaternion openRotationQuaternion;
    private bool isOpen = false;

    // --- Player detection ---
    private bool playerNear = false;
    private Inventory playerInventory;

    // Optional UI prompt
    public GameObject pressEText;

    private void Start()
    {
        closedRotation = transform.localRotation;
        openRotationQuaternion = closedRotation * Quaternion.Euler(openRotation);

        Debug.Log("Door initialized. Closed: " + closedRotation.eulerAngles + ", Open: " + openRotationQuaternion.eulerAngles);

        if (pressEText != null)
            pressEText.SetActive(false);
    }

    private void Update()
    {
        if (playerNear && playerInventory != null && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E pressed while near door. Checking inventory...");
            TryOpenDoor(playerInventory);
        }
    }

    private void TryOpenDoor(Inventory inventory)
    {
        Debug.Log("Checking door requirements...");

        if (isKeypadDoor)
        {
            Debug.Log("Door requires a keypad. Cannot open with E.");
            return;
        }

        if (requiredKey != null)
        {
            Debug.Log("Door requires key: " + requiredKey.keyID);
            if (!inventory.HasKey(requiredKey))
            {
                Debug.Log("Player does NOT have the required key: " + requiredKey.keyID);
                return;
            }
            else
            {
                Debug.Log("Player has the required key: " + requiredKey.keyID);
            }
        }
        else
        {
            Debug.Log("Door does not require a key.");
        }

        ToggleDoorState();
    }

    private void ToggleDoorState()
    {
        StopAllCoroutines();
        Quaternion targetRotation = isOpen ? closedRotation : openRotationQuaternion;

        Debug.Log("Animating door from " + transform.localRotation.eulerAngles +
                  " to " + targetRotation.eulerAngles + ". Currently open: " + isOpen);

        StartCoroutine(AnimateDoorRotation(targetRotation));
        isOpen = !isOpen;
    }

    private IEnumerator AnimateDoorRotation(Quaternion targetRotation)
    {
        while (Quaternion.Angle(transform.localRotation, targetRotation) > 0.01f)
        {
            transform.localRotation = Quaternion.RotateTowards(
                transform.localRotation,
                targetRotation,
                Time.deltaTime * animationSpeed * 100f
            );
            yield return null;
        }

        transform.localRotation = targetRotation;

        Debug.Log("Door rotation finished. Door is now " + (isOpen ? "open" : "closed"));
    }

    // --- Trigger detection ---
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object has an Inventory component
        Inventory inventory = other.GetComponent<Inventory>();
        if (inventory != null)
        {
            playerNear = true;
            playerInventory = inventory;

            Debug.Log("Player detected near door: " + other.name);

            if (pressEText != null)
                pressEText.SetActive(true);
        }
        else
        {
            Debug.Log("Trigger entered by object without Inventory: " + other.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Inventory inventory = other.GetComponent<Inventory>();
        if (inventory != null && inventory == playerInventory)
        {
            playerNear = false;
            playerInventory = null;

            Debug.Log("Player exited door trigger zone: " + other.name);

            if (pressEText != null)
                pressEText.SetActive(false);
        }
    }
}
