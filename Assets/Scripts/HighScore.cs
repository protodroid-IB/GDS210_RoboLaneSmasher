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
    public void PlayerScore()
    {
        int number = Random.Range(100, 7000);
        score.text = number.ToString();

        if (number > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", number);
            highScore.text = number.ToString();
        }       
    }

    public void Reset ()
    {
        PlayerPrefs.DeleteAll();
        highScore.text = "0";
    }
}
