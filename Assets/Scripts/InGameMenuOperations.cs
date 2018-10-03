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

    public void Mute()
    {
        AudioListener.pause = !AudioListener.pause;

        if (muteIcon.sprite == muteIconSpriteArray[0]) muteIcon.sprite = muteIconSpriteArray[1];
        else muteIcon.sprite = muteIconSpriteArray[0];
    }

    public void MainMenu(string inMainMenuSceneName)
    {
        SceneManager.LoadScene(inMainMenuSceneName);
    }
}
