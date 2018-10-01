using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : SpawnController
{
    private bool firstUnit = true;
    private UnitType lastUnitType = UnitType.Melee;


	// Use this for initialization
	void Start ()
    {
      

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void SpawnMeleeUnit()
    {
        CreateUnit(gameController.GetEnemyWeightClass(), UnitType.Melee);
        lastUnitType = UnitType.Melee;
        Debug.Log("Enemy Scrap: " + resourceController.GetEnemyScrap());
    }

    private void SpawnRangedUnit()
    {
        CreateUnit(gameController.GetEnemyWeightClass(), UnitType.Ranged);
        lastUnitType = UnitType.Ranged;
        Debug.Log("Enemy Scrap: " + resourceController.GetEnemyScrap());
    }

    private void SpawnFlyingUnit()
    {
        CreateUnit(gameController.GetEnemyWeightClass(), UnitType.Flying);
        lastUnitType = UnitType.Ranged;
        Debug.Log("Enemy Scrap: " + resourceController.GetEnemyScrap());
    }

    private void AdvanceToNextWeightClass()
    {
        if (resourceController.GetEnemyCanAdvance())
        {
            gameController.AdvanceEnemyWeightClass();
            resourceController.SetEnemyCanAdvance(false);
            Debug.Log("Enemy Weight Class Advanced: " + gameController.GetEnemyWeightClass().ToString());
        }
    }

    private UnitType LastUnitTypeBuilt()
    {
        return lastUnitType;
    }

    private bool IsBuildQueueFull()
    {
        return buildQueue.IsFull();
    }

    private bool CanAfford(int inCost, int inAvailableFunds)
    {
        if(inCost <= inAvailableFunds)
        {
            return true;
        }

        return false;
    }


}
