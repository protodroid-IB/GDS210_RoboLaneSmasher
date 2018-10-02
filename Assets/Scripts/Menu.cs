using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class Menu : MonoBehaviour
{
    [SerializeField]
    GameObject menuPanel;

    [SerializeField]
    GameObject optionsPanel;

    public void Play()
    {
        SceneManager.LoadScene("Game scene Bill");
        FindObjectOfType<AudioManager>().Play("ButtonSelect");
    }
    public void Options()
    {
        menuPanel.SetActive(false);
        optionsPanel.SetActive(true);
        FindObjectOfType<AudioManager>().Play("ButtonSelect");
    }
    public void MainMenu()
    {
        menuPanel.SetActive(true);
        optionsPanel.SetActive(false);
        FindObjectOfType<AudioManager>().Play("ButtonSelect");
    }
    public void Quit()
    {
        Debug.Log("App Quit");
        Application.Quit();
        FindObjectOfType<AudioManager>().Play("ButtonSelect");
    }

}
