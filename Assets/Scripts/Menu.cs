using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;


public class Menu : MonoBehaviour
{
    [SerializeField]
    GameObject menuPanel;

    [SerializeField]
    GameObject optionsPanel;

    AudioManager audioManager;

    public AudioMixer audioMixer;

    void Start()
    {
        audioManager = AudioManager.instance;
    }
    public void Play()
    {
        SceneManager.LoadScene("SampleScene");
        audioManager.PlaySound("ButtonSelect");
    }
    public void Options()
    {
        menuPanel.SetActive(false);
        optionsPanel.SetActive(true);
        audioManager.PlaySound("ButtonSelect");
    }
    public void MainMenu()
    {
        menuPanel.SetActive(true);
        optionsPanel.SetActive(false);
        audioManager.PlaySound("ButtonSelect");
    }
    public void Quit()
    {
        Debug.Log("App Quit");
        Application.Quit();
        audioManager.PlaySound("ButtonSelect");
    }
    public void Hover()
    {
        audioManager.PlaySound("ButtonHover");
    }

    public void SetVolumeMaster (float volume)
    {
        audioMixer.SetFloat("Master", volume);
    }
    public void SetVolumeMusic(float volume)
    {
        audioMixer.SetFloat("Music", volume);
    }
    public void SetVolumeSFX(float volume)
    {
        audioMixer.SetFloat("SFX", volume);
    }

    public void Mute()
    {
        AudioListener.pause = !AudioListener.pause;
    }

}
