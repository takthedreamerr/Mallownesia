using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    // [Header("UI Panels")]
    // public GameObject settingsPanel; // Reference to the settings/player interactions panel

    void Start()
    {
        // Ensure settings panel is hidden when the game starts
        // if (settingsPanel != null)
        // {
        //     settingsPanel.SetActive(false);
        // }
    }

    // Called when Settings button is clicked
    public void OnSettingsClicked()
    {
        // if (settingsPanel != null)
        // {
        //     settingsPanel.SetActive(true);
        // }
        // else
        // {
        //     Debug.LogWarning("Settings panel reference not set in MainMenuButtons");
        // }

        Debug.Log("Settings button clicked - Settings functionality commented out");
    }

    // Called when Play button is clicked
    public void OnPlayClicked()
    {
        // Load the next scene in build order
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        // Check if next scene exists
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("No next scene available! Make sure your game scene is added to build settings.");
            // Alternatively, you can load a specific scene by name:
            // SceneManager.LoadScene("GameScene");
        }
    }

    // Called when Quit button is clicked
    public void OnQuitClicked()
    {
        Debug.Log("Quit button clicked - Application would quit now");

        // Quit the application
#if UNITY_EDITOR
        // If running in the editor, stop play mode
        UnityEditor.EditorApplication.isPlaying = false;
#else
            // If running in a build, quit the application
            Application.Quit();
#endif
    }

    // Optional: Method to close the settings panel
    // public void CloseSettingsPanel()
    // {
    //     if (settingsPanel != null)
    //     {
    //         settingsPanel.SetActive(false);
    //     }
    // }
}
