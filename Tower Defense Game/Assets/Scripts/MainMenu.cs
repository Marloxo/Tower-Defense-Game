using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public string levelToLoad = "MainLevel";
    public SceneFader sceneFader;
    public void play()
    {
        sceneFader.FadeTo(levelToLoad);
    }

    public void quit()
    {
        Application.Quit();
    }

}
