using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseUI;
    public SceneFader sceneFader;
    private string menuSceneName = "MainMenu";
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
            Toggle();
    }

    public void Toggle()
    {
        pauseUI.SetActive(!pauseUI.activeSelf);
        if (pauseUI.activeSelf)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;
    }

    public void Retry()
    {
        Toggle(); //change timeScale to normal
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        Toggle(); //change timeScale to normal
        sceneFader.FadeTo(menuSceneName);
    }
}
