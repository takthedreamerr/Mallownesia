using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameNamePopUp : MonoBehaviour
{
    [Header("Animation Settings")]
    public Animator titleAnimator;
    public string animationName = "BubbleScale";
    public float delayBeforeLoad = 1.0f;

    [Header("Next Scene")]
    public string nextSceneName = "MainMenu";

    [Header("Fallback Settings")]
    public float fallbackWaitTime = 3f; // If animation length can't be determined

    [Header("Input Settings")]
    public bool allowSkipWithAnyKey = true;
    public KeyCode skipKey = KeyCode.Space; // Alternative: specific key

    private void Start()
    {
        StartCoroutine(PlayAnimationAndLoadScene());
    }

    private IEnumerator PlayAnimationAndLoadScene()
    {
        // Validate components
        if (titleAnimator == null || string.IsNullOrEmpty(animationName))
        {
            Debug.LogWarning("Animator or animation name missing on GameNamePopUp!");
            yield return new WaitForSeconds(fallbackWaitTime);
            LoadNextScene();
            yield break;
        }

        // Play animation
        titleAnimator.Play(animationName);

        // Calculate wait time
        float waitTime = GetAnimationLength() + delayBeforeLoad;

        yield return new WaitForSeconds(waitTime);

        LoadNextScene();
    }

    private float GetAnimationLength()
    {
        if (titleAnimator.runtimeAnimatorController == null)
        {
            Debug.LogWarning("No Runtime Animator Controller found!");
            return fallbackWaitTime - delayBeforeLoad;
        }

        var clips = titleAnimator.runtimeAnimatorController.animationClips;
        foreach (var clip in clips)
        {
            if (clip.name == animationName)
            {
                return clip.length;
            }
        }

        Debug.LogWarning($"Animation clip '{animationName}' not found!");
        return fallbackWaitTime - delayBeforeLoad;
    }

    private void LoadNextScene()
    {
        if (string.IsNullOrEmpty(nextSceneName))
        {
            Debug.LogError("Next scene name is not specified!");
            return;
        }

        SceneManager.LoadScene(nextSceneName);
    }

    // Optional: Skip animation with user input
    private void Update()
    {
        if (allowSkipWithAnyKey && Input.anyKeyDown)
        {
            StopAllCoroutines();
            LoadNextScene();
        }

        // Alternative: Use specific key
        // if (Input.GetKeyDown(skipKey))
        // {
        //     StopAllCoroutines();
        //     LoadNextScene();
        // }
    }

    // Alternative: Animation Event method (call this from your animation)
    public void OnAnimationComplete()
    {
        StartCoroutine(LoadAfterDelay());
    }

    private IEnumerator LoadAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeLoad);
        LoadNextScene();
    }
}

