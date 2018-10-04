using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;


public class Menu : MonoBehaviour
{
    [SerializeField]
    GameObject menuPanel;

    [SerializeField]
    GameObject optionsPanel;

    AudioManager audioManager;

    public AudioMixer audioMixer;

    [SerializeField]
    private Toggle muteToggle;

    [SerializeField]
    GameObject creditsPanel;

    void Start()
    {
        audioManager = AudioManager.instance;

        if(audioManager.isMuted)
        {
            muteToggle.isOn = true;
        }

    }

    // Button commands and audio needed to be played when used
    public void Play()
    {
        SceneManager.LoadScene("Game");
        audioManager.PlaySound("ButtonSelect1");
    }
    public void Options()
    {
        menuPanel.SetActive(false);
        optionsPanel.SetActive(true);
        audioManager.PlaySound("ButtonSelect1");
    }
    public void MainMenu()
    {
        menuPanel.SetActive(true);
        optionsPanel.SetActive(false);
        creditsPanel.SetActive(false);
        audioManager.PlaySound("ButtonSelect1");
    }
    public void Quit()
    {
        Debug.Log("App Quit");
        Application.Quit();
        audioManager.PlaySound("ButtonSelect1");
    }
    public void Hover()
    {
        audioManager.PlaySound("ButtonHover");
    }
    public void Credits()
    {
        creditsPanel.SetActive(true);
        audioManager.PlaySound("ButtonSelect1");
    }

    public void CreditsClose()
    {
        creditsPanel.SetActive(false);
        audioManager.PlaySound("ButtonSelect1");
    }

    // links options slider to audiomixer groups
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

    // pauses audio listeners to mute all audio
    public void Mute()
    {
        AudioListener.pause = !AudioListener.pause;
        audioManager.isMuted = !audioManager.isMuted;
    }
}
