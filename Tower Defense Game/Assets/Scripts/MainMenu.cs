using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public string levelToLoad = "MainLevel";
    public void play()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void quit()
    {
        Application.Quit();
    }

}
