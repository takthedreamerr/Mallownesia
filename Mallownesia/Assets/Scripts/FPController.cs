using System.Security.Cryptography.X509Certificates;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public class FPController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.5f;

    [Header("Animation Settings")]
    public Animator animator;

    [Header("Look Settings")]
    public Transform cameraTransform;
    public float lookSensitivity = 0.2f;
    public float verticalLookLimit = 90f;

    [Header("PickUpSettings")]
    public float pickupRange = 3f;
    public LayerMask pickupLayerMask = ~0; // set in inspector to only include pickupable layers
    public QueryTriggerInteraction triggerInteraction = QueryTriggerInteraction.Collide;
    public Transform holdPoint;
    private PickUpObject heldObject;

    private CharacterController controller;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private Vector3 velocity;
    private float verticalRotation = 0f;

    public GameObject pauseMenu;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        // Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        pauseMenu.SetActive(false);

        velocity = Vector3.zero;
    }

    private void Update()
    {
        if (enabled)
        {
            HandleMovement();
            HandleLook();
            Pause();
            Resume();
        }
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    public void HandleMovement()
    {
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        controller.Move(move * moveSpeed * Time.deltaTime);

        // Apply gravity
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -1.5f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // ✅ Animation update
        float speed = move.magnitude;
        animator.SetFloat("Speed", speed, 0.1f, Time.deltaTime);

        // ✅ Set grounded state for animations
        animator.SetBool("IsGrounded", controller.isGrounded);
    }

    public void HandleLook()
    {
        float mouseX = lookInput.x * lookSensitivity;
        float mouseY = lookInput.y * lookSensitivity;
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalLookLimit,
        verticalLookLimit);
        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    public void Onjump(InputAction.CallbackContext context)
    {
        if (context.performed && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    public void Pause()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
            SoundManager.Instance.PlaySound7();
        }
    }

    public void Resume()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
            SoundManager.Instance.PlaySound7();

        }
    }

}