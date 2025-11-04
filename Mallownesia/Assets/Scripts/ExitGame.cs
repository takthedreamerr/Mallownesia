using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitGame : MonoBehaviour
{
    public void RetryGame()
    {
        SceneManager.LoadScene(1);
    }
}
