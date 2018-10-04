using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    // SCORING VARIABLES
    private HighScore highscore;

    [SerializeField]
    private TextMeshProUGUI scoreComponent;
    private int playerScore = 0;

    private bool scoreCalculated = false;

    private float gameTime = 0;
    private int totalScrapEarned = 0;
    private int totalExperienceGained = 0;
    private int totalUnitsDeployed = 0;

    [SerializeField]
    private int advanceWeightClassScoringBonus = 500, victoryBonus = 1000;
    private bool vicotrious = false;

    private int advanceClassBonus = 0;

    [SerializeField]
    private float toNormaliseTimeForScore = 5000;




    // Use this for initialization
    void Awake ()
    {
        gameOverScreen = GetComponent<GameOverScreen>();
        gameStartScreen.SetActive(true);
        highscore = GetComponent<HighScore>();

        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        audioManager = AudioManager.instance;

        Time.timeScale = 1f;
    }


    private void Update()
    {
        TrackBattleTime();
        //Debug.Log("GAME TIME: " + gameTime + "\tSCRAP COUNT: " + totalScrapEarned + "\tEXP GAINED: " + totalExperienceGained);
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
            vicotrious = true;
        }
        else
        {
            gameOverScreen.GameOverEnemyWins();
        }

        currentGameState = GameState.End;
        CalculateScore();
        SetPlayerScore();
    }

    public int GetPlayerScore()
    {
        return playerScore;
    }


    private void SetPlayerScore()
    {
        scoreComponent.text = GetPlayerScore().ToString();
        highscore.PlayerScore(playerScore);
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
            advanceClassBonus += advanceWeightClassScoringBonus;
        }
        else if(playerWeightClass == WeightClass.Medium)
        {
            playerWeightClass = WeightClass.Heavy;
            advanceClassBonus += advanceWeightClassScoringBonus;
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





    private void TrackBattleTime()
    {
        if(currentGameState == GameState.InGame)
        {
            gameTime += Time.deltaTime;
        }
    }


    public void TrackScrapEarned(int inScrap)
    {
        totalScrapEarned += inScrap;
    }


    public void TrackExperienceGained(int inExp)
    {
        totalExperienceGained += inExp;
    }


    public void TrackUnitsDeployed()
    {
        totalUnitsDeployed++;
    }


    public int CalculateScore()
    {
        playerScore = (int)(((float)totalScrapEarned + ((float)totalExperienceGained / 2f)) * (1f - (gameTime / toNormaliseTimeForScore)) + advanceClassBonus);

        if (vicotrious) playerScore += victoryBonus;


        Debug.Log("PLAYERSCORE: " + playerScore);
        return playerScore;
    }
}

