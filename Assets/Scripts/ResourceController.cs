using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceController : MonoBehaviour
{
    private GameController gameController;

    [Space(5)]
    [Header("STARTING SCRAP AMOUNTS")]
    [SerializeField]
    private int playerStartScrap, enemyStartScrap;

    private int playerScrap, enemyScrap;

    [SerializeField]
    private int advanceToMediumExp = 1000, advanceToHeavyExp = 3000;

    private int playerRequiredExp, enemyRequiredExp;
    private int playerExp = 0, enemyExp = 0;

    [SerializeField]
    private TextMeshProUGUI scrapValue, expValue;





    // Use this for initialization
    void Start ()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();

        // set the player and enemy scrap to their starting scrap amounts
        playerScrap = playerStartScrap;
        enemyScrap = enemyStartScrap;

        // set the player and enemy experience to the required experience
        playerRequiredExp = advanceToMediumExp;
        enemyRequiredExp = advanceToMediumExp;

        // update scrap and exp UI
        UpdateScrapUI();
        UpdateExpUI();
    }







    public int GetPlayerScrap()
    {
        return playerScrap;
    }

    public void AddPlayerScrap(int inScrap)
    {
        playerScrap += inScrap;
        UpdateScrapUI();
    }

    public void SubtractPlayerScrap(int inScrap)
    {
        playerScrap -= inScrap;
        UpdateScrapUI();
    }



    public int GetEnemyScrap()
    {
        return enemyScrap;
    }

    public void AddEnemyScrap(int inScrap)
    {
        enemyScrap += inScrap;
    }

    public void SubtractEnemyScrap(int inScrap)
    {
        enemyScrap -= inScrap;
    }






    public int GetPlayerExp()
    {
        return playerExp;
    }

    public void AddPlayerExp(int inExp)
    {
        playerExp += inExp;

        if(gameController.GetPlayerWeightClass() != WeightClass.Heavy)
        {
            if (playerExp >= playerRequiredExp)
            {
                playerExp = 0;

                if (gameController.GetPlayerWeightClass() == WeightClass.Light)
                {
                    playerRequiredExp = advanceToHeavyExp;
                }

                gameController.AdvancePlayerWeightClass();
                Debug.Log("Player Weight Class Advanced: " + gameController.GetPlayerWeightClass().ToString());
            }
        }

        UpdateExpUI();
    }

    public void SubtractPlayerExp(int inExp)
    {
        playerExp -= inExp;

        if (playerExp <= 0) playerExp = 0;

        UpdateExpUI();
    }



    public int GetEnemyExp()
    {
        return enemyExp;
    }

    public void AddEnemyExp(int inExp)
    {
        enemyExp += inExp;

        if (gameController.GetEnemyWeightClass() != WeightClass.Heavy)
        {
            if (enemyExp >= enemyRequiredExp)
            {
                enemyExp = 0;

                if (gameController.GetEnemyWeightClass() == WeightClass.Light)
                {
                    enemyRequiredExp = advanceToHeavyExp;
                }

                gameController.AdvanceEnemyWeightClass();
                Debug.Log("Enemy Weight Class Advanced: " + gameController.GetEnemyWeightClass().ToString());

            }
        }
    }

    public void SubtractEnemyExp(int inExp)
    {
        enemyExp -= inExp;
    }


    private void UpdateScrapUI()
    {
        scrapValue.text = playerScrap.ToString();
    }

    private void UpdateExpUI()
    {
        if(gameController.GetPlayerWeightClass() != WeightClass.Heavy)
        {
            expValue.text = playerExp.ToString() + " / " + playerRequiredExp.ToString();
        }
        else
        {
            expValue.text = playerExp.ToString();
        }
    }
}
