using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private GameState currentGameState = GameState.Begin;

    private WeightClass playerWeightClass = WeightClass.Light;
    private WeightClass enemyWeightClass = WeightClass.Light;

    private List<GameObject> playerUnits = new List<GameObject>();
    private List<GameObject> enemyUnits = new List<GameObject>();

    [SerializeField]
    private BaseHealth playerBase, enemyBase;

    private GameOverScreen gameOverScreen;

    private bool isPaused = false, gameOver = false;

    [SerializeField]
    private GameObject gameStartScreen;

    private AudioManager audioManager;


    // Use this for initialization
    void Awake ()
    {
        gameOverScreen = GetComponent<GameOverScreen>();
        gameStartScreen.SetActive(true);

        audioManager = AudioManager.instance;
    }




    private void Update()
    {

    }


    public void SetPaused(bool inPaused)
    {
        isPaused = inPaused;
    }


    public bool IsPaused()
    {
        return isPaused;
    }

    public void SetGameOver(bool inOver)
    {
        gameOver = inOver;
    }

    public bool GameOver()
    {
        return gameOver;
    }



    public void SetBattleWinner(Commander inCommander)
    {
        if(inCommander == Commander.Player)
        {
            gameOverScreen.GameOverPlayerWins();
        }
        else
        {
            gameOverScreen.GameOverEnemyWins();
        }

        currentGameState = GameState.End;
    }

    public int GetPlayerScore()
    {
        return 0;
    }
    





    public WeightClass GetPlayerWeightClass()
    {
        return playerWeightClass;
    }

    public WeightClass GetEnemyWeightClass()
    {
        return enemyWeightClass;
    }


    public void AdvancePlayerWeightClass()
    {
        if(playerWeightClass == WeightClass.Light)
        {
            playerWeightClass = WeightClass.Medium;
        }
        else if(playerWeightClass == WeightClass.Medium)
        {
            playerWeightClass = WeightClass.Heavy;
        }
    }

    public void AdvanceEnemyWeightClass()
    {
        if (enemyWeightClass == WeightClass.Light)
        {
            enemyWeightClass = WeightClass.Medium;
        }
        else if (enemyWeightClass == WeightClass.Medium)
        {
            enemyWeightClass = WeightClass.Heavy;
        }
    }















    public void AddPlayerUnit(GameObject inUnit)
    {
        playerUnits.Add(inUnit);
        //PrintGOList(Commander.Player, playerUnits);
    }

    public void RemovePlayerUnit(GameObject inUnit)
    {
        playerUnits.Remove(inUnit);
        //PrintGOList(Commander.Player, playerUnits);
    }

    public void AddEnemyUnit(GameObject inUnit)
    {
        enemyUnits.Add(inUnit);
        //PrintGOList(Commander.Enemy, enemyUnits);
    }

    public void RemoveEnemyUnit(GameObject inUnit)
    {
        enemyUnits.Remove(inUnit);
        //PrintGOList(Commander.Enemy, enemyUnits);
    }


    private void PrintGOList(Commander inCommander, List<GameObject> inList)
    {
        Debug.Log(inCommander.ToString().ToUpper() + " LIST!\n");

        for(int i=0; i < inList.Count; i++)
        {
            Debug.Log("\t" + inList[i].name + "\n");
        }
    }


    public GameState GetCurrentGameState()
    {
        return currentGameState;
    }

    public void StartGame()
    {
        currentGameState = GameState.InGame;
        gameStartScreen.SetActive(false);
    }


    public void ButtonSelectSound(int inNum)
    {
        audioManager.PlaySound("ButtonSelect" + inNum.ToString());
    }

    public void AdvanceWeightClassSound()
    {
        audioManager.PlaySound("Upgrade2");
    }
}

