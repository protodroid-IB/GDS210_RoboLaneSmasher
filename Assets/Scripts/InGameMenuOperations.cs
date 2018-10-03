using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameMenuOperations : MonoBehaviour
{
    [SerializeField]
    private Image muteIcon;

    [SerializeField]
    private Sprite[] muteIconSpriteArray;

    private AudioManager audioManager;

    private void Start()
    {
        audioManager = AudioManager.instance;

        UpdateMuteUI(audioManager.isMuted);
    }

    public void Mute()
    {
        AudioListener.pause = !AudioListener.pause;

        audioManager.isMuted = !audioManager.isMuted;

        UpdateMuteUI(audioManager.isMuted);
    }

    private void UpdateMuteUI(bool muted)
    {
        if (muted) muteIcon.sprite = muteIconSpriteArray[1];
        else muteIcon.sprite = muteIconSpriteArray[0];
    }

    public void MainMenu(string inMainMenuSceneName)
    {
        SceneManager.LoadScene(inMainMenuSceneName);
    }
}
