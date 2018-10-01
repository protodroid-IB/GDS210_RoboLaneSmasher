using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOverGO;

    public void GameOverPlayerWins()
    {
        GameOver();
        // set UI text winner
        // find and set UI score
    }

    public void GameOverEnemyWins()
    {
        GameOver();
        // set UI text winner
        // find and set UI score
    }

    private void GameOver()
    {
        gameOverGO.SetActive(true);
        Time.timeScale = 0.0f;
    }


    public void ToMainMenu()
    {
        // IMPLEMENT MAIN MENU BUTTON
    }

    public void ToggleSound()
    {
        // IMPLEMENT SOUND TOGGLE
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
