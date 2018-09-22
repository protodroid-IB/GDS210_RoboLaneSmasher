using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnController : MonoBehaviour
{

    private GameController gameController;
    private ResourceController resourceController;

    private UnitDatabase unitDatabase;

    [SerializeField]
    private Transform spawnPos, playerUnitsHierarchy;



    // Use this for initialization
    void Start ()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        resourceController = GameObject.FindWithTag("GameController").GetComponent<ResourceController>();
        unitDatabase = GetComponent<UnitDatabase>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}



    public void SpawnMeleeUnit()
    {
        CreateUnit(gameController.GetPlayerWeightClass(), UnitType.Melee);
    }



    private void CreateUnit(WeightClass inClass, UnitType inType)
    {
        Unit newUnit = unitDatabase.FindUnit(inClass, inType);

        if(CanAfford(newUnit.prefab.GetComponent<BaseUnit>().GetBuildCost()))
        {
            GameObject newUnitGO = Instantiate(newUnit.prefab, spawnPos.position, Quaternion.identity, playerUnitsHierarchy);
            gameController.AddPlayerUnit(newUnitGO);
        }   
    }

    private bool CanAfford(int inCost)
    {
        if(inCost <= resourceController.GetPlayerScrap())
        {
            resourceController.SubtractPlayerScrap(inCost);
            return true;
        }

        return false;
    }
}
