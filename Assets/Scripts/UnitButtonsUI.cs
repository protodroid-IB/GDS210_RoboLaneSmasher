using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitButtonsUI : MonoBehaviour
{
    private GameController gameController;
    private UnitDatabase unitDatabase;

    [SerializeField]
    private Image[] unitIcons;

    // Use this for initialization
    void Start ()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        unitDatabase = GameObject.FindWithTag("SpawnController").GetComponent<UnitDatabase>();

        UpdateUI();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void UpdateUI()
    {
        WeightClass currentClass = gameController.GetPlayerWeightClass();

        Sprite meleeIcon = unitDatabase.FindUnit(currentClass, UnitType.Melee).prefab.GetComponent<BaseUnit>().GetIcon();
        Sprite rangedIcon = unitDatabase.FindUnit(currentClass, UnitType.Melee).prefab.GetComponent<BaseUnit>().GetIcon();
        Sprite flyingIcon = unitDatabase.FindUnit(currentClass, UnitType.Melee).prefab.GetComponent<BaseUnit>().GetIcon();

        unitIcons[0].sprite = meleeIcon;
        unitIcons[1].sprite = rangedIcon;
        unitIcons[2].sprite = flyingIcon;
    }
}
