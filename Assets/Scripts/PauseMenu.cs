using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private GameController gameController;

    [SerializeField]
    private GameObject pauseGO;

    private bool gamePaused = false;

    private void Awake()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

    public void PauseGame()
    {
        pauseGO.SetActive(true);
        Time.timeScale = 0.0f;
        gamePaused = true;
        gameController.SetPaused(true);
    }

    public void ResumeGame()
    {
        pauseGO.SetActive(false);
        Time.timeScale = 1.0f;
        gamePaused = false;
        gameController.SetPaused(false);
    }

    public void ToMainMenu()
    {
        // IMPLEMENT MAIN MENU BUTTON
    }

    public void ToggleSound()
    {
        // IMPLEMENT SOUND TOGGLE
    }
}
