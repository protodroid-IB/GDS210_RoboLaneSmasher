using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ResourceController : MonoBehaviour
{
    private GameController gameController;
    private UnitButtonsUI unitButtonsUI;


    [Space(5)]
    [Header("STARTING SCRAP AMOUNTS")]
    [SerializeField]
    private int playerStartScrap;

    [SerializeField]
    private int enemyStartScrap;

    private int playerScrap, enemyScrap;

    [Space(5)]
    [Header("EXPERIENCE NEEDED TO ADVANCE TO NEXT WEIGHT CLASS")]
    [SerializeField]
    private int advanceToMediumExp = 1000;

    [SerializeField]
    private int advanceToHeavyExp = 3000;

    private int playerRequiredExp, enemyRequiredExp;
    private int playerExp = 0, enemyExp = 0;


    [Space(5)]
    [Header("GAME VALUE TEXT COMPONENT REFERENCES")]
    [SerializeField]
    private TextMeshProUGUI scrapValue;

    [SerializeField]
    private TextMeshProUGUI expValue;

    [Space(5)]
    [Header("ADVANCE WEIGHT CLASS BUTTON REFERENCES")]
    [SerializeField]
    private Image advanceWeightClassIcon;

    [SerializeField]
    private Button advanceWeightClassButton;

    [SerializeField]
    private Image weightClassIcon;

    [SerializeField]
    private Sprite[] weightClassIconSprites;

    private bool advanceWeightClassButtonPressed = false;

    private Color advanceEnabledColor = Color.white;
    private Color advanceDisabledColor = new Color(0.4f, 0.4f, 0.4f, 0.5f);

    private bool enemyCanAdvance = false;





    // Use this for initialization
    void Start ()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        unitButtonsUI = GameObject.FindWithTag("UnitButtons").GetComponent<UnitButtonsUI>(); 


        // set the player and enemy scrap to their starting scrap amounts
        playerScrap = playerStartScrap;
        enemyScrap = enemyStartScrap;

        // set the player and enemy experience to the required experience
        playerRequiredExp = advanceToMediumExp;
        enemyRequiredExp = advanceToMediumExp;

        // update scrap and exp UI
        UpdateScrapUI();
        UpdateExpUI();

        // update advance class weight UI
        UpdateAdvanceClassUI(advanceDisabledColor, false);

        UpdateWeightClassIconsUI(gameController.GetPlayerWeightClass());
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

    public void SubtractEnemyExp(int inExp)
    {
        enemyExp -= inExp;
    }


    public void SetEnemyCanAdvance(bool inBool)
    {
        enemyCanAdvance = inBool;
    }

    public bool GetEnemyCanAdvance()
    {
        return enemyCanAdvance;
    }

    public void AddPlayerExp(int inExp)
    {
        playerExp += inExp;

        if (playerExp >= playerRequiredExp) playerExp = playerRequiredExp;

        if(gameController.GetPlayerWeightClass() != WeightClass.Heavy)
        {
            if (playerExp >= playerRequiredExp)
            {
                UpdateAdvanceClassUI(advanceEnabledColor, true);

                if (advanceWeightClassButtonPressed == true)
                {
                    playerExp = 0;

                    if (gameController.GetPlayerWeightClass() == WeightClass.Light)
                    {
                        playerRequiredExp = advanceToHeavyExp;
                    }

                    gameController.AdvancePlayerWeightClass();
                    unitButtonsUI.UpdateUI();
                    advanceWeightClassButtonPressed = false;
                    UpdateAdvanceClassUI(advanceDisabledColor, false);
                    UpdateWeightClassIconsUI(gameController.GetPlayerWeightClass());

                    Debug.Log("Player Weight Class Advanced: " + gameController.GetPlayerWeightClass().ToString());
                } 
            }
        }

        UpdateExpUI();
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

                enemyCanAdvance = true;
            }
        }
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



    // this is triggered when the player presses the advance button
    public void AdvanceWeightClass()
    {
        advanceWeightClassButtonPressed = true;
        AddPlayerExp(0);
    }

    public void UpdateAdvanceClassUI(Color inColor, bool buttonEnabled)
    {
        if(advanceWeightClassIcon != null) advanceWeightClassIcon.color = inColor;
        if(advanceWeightClassButton != null) advanceWeightClassButton.interactable = buttonEnabled;
    }

    public void UpdateWeightClassIconsUI(WeightClass inClass)
    {
        switch(inClass)
        {
            case WeightClass.Light:
                weightClassIcon.sprite = weightClassIconSprites[0];
                break;

            case WeightClass.Medium:
                weightClassIcon.sprite = weightClassIconSprites[1];
                break;

            case WeightClass.Heavy:
                weightClassIcon.sprite = weightClassIconSprites[2];
                break;
        }
    }
}
