using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CompleteLevel : MonoBehaviour
{
    public SceneFader sceneFader;
    private string menuSceneName = "MainMenu";
    public string nextLevel = "Level02";
    public int levelToUnlock = 2;

    public void Continue()
    {
        PlayerPrefs.SetInt("levelReached", levelToUnlock);
        sceneFader.FadeTo(nextLevel);
    }
    public void Menu()
    {
        sceneFader.FadeTo(menuSceneName);
    }
}
