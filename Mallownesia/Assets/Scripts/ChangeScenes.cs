using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScenes : MonoBehaviour
{
    public GameObject Panel001;
    public GameObject Panel002;
    public GameObject Panel003;

    public int currentPanel;

    private void Start()
    {
        currentPanel = 0;
        
    }
    public void GoToSceneTwo()
    {
        SceneManager.LoadScene("LevelObjective");
        

    }

    public void OnNextClick()
    {
        
        if (currentPanel == 0)
        {
            Panel001.SetActive(true);
            Panel002.SetActive(false);
            Panel003.SetActive(false);

        }
        if (currentPanel == 1)
        {
            Panel001.SetActive(false);
            Panel002.SetActive(true);
            Panel003.SetActive(false);
            
        }
        if (currentPanel == 2)
        {
            Panel001.SetActive(false);
            Panel002.SetActive(false);
            Panel003.SetActive(true);
            
        }
        if (currentPanel == 3)
        {
            SceneManager.LoadScene("Sub2");
        }
        currentPanel++;


    }


}
