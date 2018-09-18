using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnController : MonoBehaviour
{

    private GameController gameController;

    private UnitDatabase unitDatabase;

    [SerializeField]
    private Transform spawnPos, playerUnitsHierarchy;


    // Use this for initialization
    void Start ()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        unitDatabase = GetComponent<UnitDatabase>();

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}



    public void SpawnMeleeUnit()
    {
        switch(gameController.GetPlayerWeightClass())
        {
            case WeightClass.Light:
                CreateUnit(gameController.GetPlayerWeightClass(), UnitType.Melee);
                break;

            case WeightClass.Medium:

                break;

            case WeightClass.Heavy:

                break;
        }
    }



    private void CreateUnit(WeightClass inClass, UnitType inType)
    {
        Unit newUnit = unitDatabase.FindUnit(inClass, inType);
        GameObject newUnitGO = Instantiate(newUnit.prefab, spawnPos.position, Quaternion.identity, playerUnitsHierarchy);
        gameController.AddPlayerUnit(newUnitGO);
    }
}
