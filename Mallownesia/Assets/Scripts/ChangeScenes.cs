using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ChangeScenes : MonoBehaviour
{
    public GameObject[] panels; // Array to hold all 6 panels
    public GameObject playButton; // Play button for first panel
    public GameObject nextButton; // Next button for other panels

    private int currentPanel = 0;

    private void Start()
    {
        // Initialize all panels
        if (panels.Length == 0)
        {
            Debug.LogError("No panels assigned in the inspector!");
            return;
        }

        ShowPanel(0);
        UpdateButtonVisibility();
    }

    public void GoToSceneTwo()
    {
        if (SceneExists("LevelObjective"))
            SceneManager.LoadScene("LevelObjective");
        else
            Debug.LogError("Scene 'LevelObjective' not in Build Settings!");
    }

    public void OnNextClick()
    {
        currentPanel++;

        if (currentPanel < panels.Length)
        {
            ShowPanel(currentPanel);
            UpdateButtonVisibility();
        }
        else if (currentPanel == panels.Length)
        {
            // After the last panel, load the next scene
            if (SceneExists("Sub2"))
                SceneManager.LoadScene("Sub2");
            else
                Debug.LogError("Scene 'Sub2' not in Build Settings!");
        }
    }

    public void OnPlayClick()
    {
        // Play button moves to the next panel
        OnNextClick();
    }

    private void ShowPanel(int panelIndex)
    {
        // Deactivate all panels first
        for (int i = 0; i < panels.Length; i++)
        {
            if (panels[i] != null)
                panels[i].SetActive(i == panelIndex);
        }
    }

    private void UpdateButtonVisibility()
    {
        // Show play button only on first panel, next button on others
        if (playButton != null)
            playButton.SetActive(currentPanel == 0);

        if (nextButton != null)
            nextButton.SetActive(currentPanel > 0 && currentPanel < panels.Length);
    }

    private bool SceneExists(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(i);
            string name = System.IO.Path.GetFileNameWithoutExtension(path);
            if (name == sceneName) return true;
        }
        return false;
    }
}