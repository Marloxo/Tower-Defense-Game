using UnityEngine;
public class GameManager : MonoBehaviour
{
    // Update is called once per frame
    private bool gameEnded = false;
    void Update()
    {
        if (gameEnded)
            return;
        if (PlayerStats.Lives <= 0)
            EndGame();
    }

    private void EndGame()
    {
        gameEnded = true;
        Debug.Log("Game Over");
    }
}
