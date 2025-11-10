using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class GameStarterController : MonoBehaviour
{
    [Header("Animation Settings")]
    public Animator eyeAnimator; // Reference to your eye blinking animator
    public string animationName = "Blinking"; // Name of your animation clip
    public float animationDelay = 8f; // Total delay including animation

    [Header("UI Elements")]
    public GameObject blurPanel; // Reference to your blur panel

    [Header("References")]
    public FPController playerController;
    public Timer timer;

    private void Start()
    {
        // Disable player movement and timer at start
        if (playerController != null)
        {
            playerController.enabled = false;
        }

        if (timer != null)
        {
            timer.StopTimer();
            timer.HideTimer();
        }

        // Start the coroutine to handle the animation and delay
        StartCoroutine(StartGameAfterAnimation());
    }

    private IEnumerator StartGameAfterAnimation()
    {
        // Play the eye blinking animation
        if (eyeAnimator != null)
        {
            eyeAnimator.Play("Blinking");
        }

        // Wait for the specified delay (animation duration)
        yield return new WaitForSeconds(animationDelay);

        // Disable the blur panel
        if (blurPanel != null)
        {
            blurPanel.SetActive(false);
        }

        // Enable player movement
        if (playerController != null)
        {
            playerController.enabled = true;
        }

        // Start the timer
        if (timer != null)
        {
            timer.StartTimer();
            timer.ShowTimer();
            timer.ResetTimer();
        }

        Debug.Log("Game started after animation completion");
    }
}
