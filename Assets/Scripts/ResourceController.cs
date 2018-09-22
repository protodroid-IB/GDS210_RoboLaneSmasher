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

        playerScrap = playerStartScrap;
        enemyScrap = enemyStartScrap;

        UpdateScrapUI();

        playerRequiredExp = advanceToMediumExp;
        enemyRequiredExp = advanceToMediumExp;

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

        if (playerExp >= playerRequiredExp)
        {
            playerExp = 0;
            gameController.AdvancePlayerWeightClass();
            // CHANGE PLAYER REQUIRED EXPERIENCE HERE!!!
        }
    }

    public void SubtractPlayerExp(int inExp)
    {
        playerExp -= inExp;
    }



    public int GetEnemyExp()
    {
        return enemyExp;
    }

    public void AddEnemyExp(int inExp)
    {
        enemyExp += inExp;

        if (enemyExp >= enemyRequiredExp)
        {
            enemyExp = 0;
            gameController.AdvanceEnemyWeightClass();
            // CHANGE ENEMY REQUIRED EXPERIENCE HERE!
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
        expValue.text = playerExp.ToString() + "/" + playerRequiredExp.ToString();

    }
}
