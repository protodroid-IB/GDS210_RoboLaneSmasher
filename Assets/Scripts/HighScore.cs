using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighScore : MonoBehaviour
{
    public TextMeshProUGUI score;

    public TextMeshProUGUI highScore;

    void Start()
    {
        highScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();

    }

    public void PlayerScore(int inScore)
    {
        if (inScore > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", inScore);
            highScore.text = inScore.ToString();
        }       
    }

    public void Reset ()
    {
        PlayerPrefs.DeleteAll();
        highScore.text = "0";
    }
}
