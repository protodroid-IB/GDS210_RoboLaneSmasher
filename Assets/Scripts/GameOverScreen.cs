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
    private TextMeshProUGUI battleConditionValue;
    


    public void GameOverPlayerWins()
    {
        GameOver();
        battleConditionValue.text = "YOU WIN!!!";
    }

    public void GameOverEnemyWins()
    {
        GameOver();
        battleConditionValue.text = "YOU LOSE!!!";
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
