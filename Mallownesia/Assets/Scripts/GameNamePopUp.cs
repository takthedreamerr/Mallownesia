using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameNamePopUp : MonoBehaviour
{

    [Header("Input Settings")]
    //public bool allowSkipWithAnyKey = true;  // Whether any key can skip the animation
    //public KeyCode skipKey = KeyCode.Space;  // Alternative: specific key to skip animation
    public KeyCode ENTERkEY = KeyCode.Return;  // Alternative: specific key to skip animation

    // Called when the script instance is being loaded
    private void Start()
    {
        // Start the coroutine that handles the animation and scene transition
       // StartCoroutine(PlayAnimationAndLoadScene());
    }


    public void Update()
    {
        if (Input.GetKeyDown(ENTERkEY))
        {
            // Stop all running coroutines (including the animation waiting coroutine)
            // StopAllCoroutines();
            SceneManager.LoadScene(1);
        }
    }


}