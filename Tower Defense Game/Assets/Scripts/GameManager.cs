﻿using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static bool GameIsOver;
    public GameObject gameOverUI;
    void Start()
    {
        GameIsOver = false;
    }
    void Update()
    {
        if (GameIsOver)
            return;
        if (PlayerStats.Lives <= 0)
            EndGame();

        #region For test purpose only
        if (Input.GetKeyDown(KeyCode.E))
            EndGame();
        #endregion

    }

    private void EndGame()
    {
        GameIsOver = true;
        gameOverUI.SetActive(true);
    }
}
