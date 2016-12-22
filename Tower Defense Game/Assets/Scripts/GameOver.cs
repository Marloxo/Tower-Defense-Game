using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    public Text roundText;
    public SceneFader sceneFader;
    private string menuSceneName = "MainMenu";
    void OnEnable()
    {
        roundText.text = PlayerStats.Rounds.ToString();
    }
    public void Retry()
    {
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }
    public void Menu()
    {
        sceneFader.FadeTo(menuSceneName);
    }
}
