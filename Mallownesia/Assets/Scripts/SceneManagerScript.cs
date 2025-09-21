using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Call this method from the Button's OnClick event
    public void LoadTakScene()
    {
        SceneManager.LoadScene("Tak");
    }
    public void LoadGameOverScene()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void LoadYouWinScene()
    {
        SceneManager.LoadScene("YouWin");
    }
}
