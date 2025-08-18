using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    /* float currentTime;
     public float startingTime = 10f;

     [SerializeField] Text countdownText;
     void Start()
     {
         currentTime = startingTime;
     }
     void Update()
     {
         currentTime -= Time.deltaTime;


         if (currentTime <= 0)
         {
             currentTime = 0;

             // Your Code
         }
         countdownText.text = currentTime.ToString(" 0");
     }*/

    float currentTime;
    public float startingTime = 10f;

    [SerializeField] Text countdownText;
    private bool hasTriggeredGameOver = false;

    private SceneLoader sceneLoader;

    void Start()
    {
        currentTime = startingTime;
        sceneLoader = Object.FindFirstObjectByType<SceneLoader>();
    }

    void Update()
    {
        if (hasTriggeredGameOver) return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            currentTime = 0;
            hasTriggeredGameOver = true;

            if (sceneLoader != null)
            {
                sceneLoader.LoadGameOverScene();
            }
            else
            {
                Debug.LogWarning("SceneLoader not found in scene.");
            }
        }

        countdownText.text = currentTime.ToString("0");
    }


}

