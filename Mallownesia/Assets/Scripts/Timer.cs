using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // <-- import TextMeshPro namespace

public class Timer : MonoBehaviour
{
    float currentTime;
    public float startingTime = 10f;

    [SerializeField] TMP_Text countdownText; // <-- TMP_Text instead of Text
    private bool hasTriggeredGameOver = false;

    private SceneLoader sceneLoader;
    public float CurrentTime => currentTime;

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
                sceneLoader.LoadGameOverScene("GameOver");
            }
            else
            {
                Debug.LogWarning("SceneLoader not found in scene.");
            }
        }

        countdownText.text = currentTime.ToString("0");
    }
}
