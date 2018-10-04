using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuScript : MonoBehaviour
{
    [SerializeField]
    GameObject menuPanel;

    [SerializeField]
    GameObject optionsPanel;

    public void Play()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void OptionsPanel()
    {
        menuPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }
    public void MenuPanel()
    {
        menuPanel.SetActive (true);
        optionsPanel.SetActive (false);
    }
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit game");
    }
}
