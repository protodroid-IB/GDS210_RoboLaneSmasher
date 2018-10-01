using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class GameOverScreen : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOverGO;

    [SerializeField]
    private TextMeshProUGUI scoreValue, battleConditionValue;
    


    public void GameOverPlayerWins()
    {
        GameOver();
        battleConditionValue.text = "YOU WIN!!!";
        scoreValue.text = "100";
    }

    public void GameOverEnemyWins()
    {
        GameOver();
        battleConditionValue.text = "YOU LOSE!!!";
        scoreValue.text = "100";
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
