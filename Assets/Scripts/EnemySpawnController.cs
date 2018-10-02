using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : SpawnController
{
    private bool firstUnit = true;
    private UnitType lastUnitType = UnitType.Melee;

    [SerializeField]
    private float minTimeBetweenUnits = 1f, maxTimeBetweenUnits = 3f;
    private float timeBetweenUnits = 1f;
    private float timer = 0f;

    private int meleeProbability = 40;
    private int rangedProbability = 30;
    private int flyingProbability = 30;

	
	// Update is called once per frame
	void Update ()
    {
        if(gameController.GetCurrentGameState() == GameState.InGame)
        {
            if (firstUnit == true)
            {
                SpawnMeleeUnit();
                firstUnit = false;
                ResetTimerVariables();
            }
            else
            {
                if (timer >= timeBetweenUnits)
                {
                    AdvanceToNextWeightClass();

                    if (!IsBuildQueueFull())
                    {
                        switch (LastUnitTypeBuilt())
                        {
                            case UnitType.Melee:
                                if (CanAfford(UnitType.Melee))
                                {
                                    meleeProbability -= 14;
                                    rangedProbability += 10;
                                    flyingProbability += 4;
                                    SpawnUnitOnChance(meleeProbability, rangedProbability, flyingProbability);
                                }
                                break;

                            case UnitType.Ranged:
                                if (CanAfford(UnitType.Ranged))
                                {
                                    meleeProbability += 6;
                                    rangedProbability -= 14;
                                    flyingProbability += 8;
                                    SpawnUnitOnChance(meleeProbability, rangedProbability, flyingProbability);
                                }
                                break;

                            case UnitType.Flying:
                                if (CanAfford(UnitType.Flying))
                                {
                                    meleeProbability += 10;
                                    rangedProbability += 5;
                                    flyingProbability -= 15;
                                    SpawnUnitOnChance(meleeProbability, rangedProbability, flyingProbability);
                                }
                                break;

                            default:
                                if (CanAfford(UnitType.Melee)) SpawnRandomUnit();
                                break;
                        }
                    }
                }

                timer += Time.deltaTime;
            }
        }
		
	}





    private void SpawnMeleeUnit()
    {
        CreateUnit(gameController.GetEnemyWeightClass(), UnitType.Melee);
        lastUnitType = UnitType.Melee;
        ResetTimerVariables();
        //Debug.Log("Enemy Scrap: " + resourceController.GetEnemyScrap());
    }

    private void SpawnRangedUnit()
    {
        CreateUnit(gameController.GetEnemyWeightClass(), UnitType.Ranged);
        lastUnitType = UnitType.Ranged;
        ResetTimerVariables();
        //Debug.Log("Enemy Scrap: " + resourceController.GetEnemyScrap());
    }

    private void SpawnFlyingUnit()
    {
        CreateUnit(gameController.GetEnemyWeightClass(), UnitType.Flying);
        lastUnitType = UnitType.Flying;
        ResetTimerVariables();
        //Debug.Log("Enemy Scrap: " + resourceController.GetEnemyScrap());
    }

    private void AdvanceToNextWeightClass()
    {
        if (resourceController.GetEnemyCanAdvance())
        {
            gameController.AdvanceEnemyWeightClass();
            resourceController.SetEnemyCanAdvance(false);
            //Debug.Log("Enemy Weight Class Advanced: " + gameController.GetEnemyWeightClass().ToString());
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




    private bool CanAfford(UnitType inType)
    {
        Unit newUnit = unitDatabase.FindUnit(gameController.GetEnemyWeightClass(), inType); // grab the unit from the database 
        BaseUnit newUnitDetails = newUnit.prefab.GetComponent<BaseUnit>();

        int buildCost = newUnitDetails.GetBuildCost(); // grab the cost to build the unit

        if (buildCost <= resourceController.GetEnemyScrap())
        {
            return true;
        }

        return false;
    }




    private void SpawnRandomUnit()
    {
        // 1 = melee, 2 = ranged, 3 = flying
        int unitNum = Random.Range(1, 4);

        switch(unitNum)
        {
            case 1:
                SpawnMeleeUnit();
                break;

            case 2:
                SpawnRangedUnit();
                break;

            case 3:
                SpawnFlyingUnit();
                break;

            default:
                SpawnMeleeUnit();
                break;
        }
    }




    private void SpawnUnitOnChance(int meleeChance, int rangedChance, int flyingChance)
    {
        float totalPoints = meleeChance + rangedChance + flyingChance;
        float d100Normalised = (float)RollDiceN((int)totalPoints) / totalPoints;

        rangedChance = meleeChance + rangedChance;
        flyingChance = rangedChance + flyingChance;

        if (meleeChance <= 0) meleeChance = 1;
        if (rangedChance <= 0) rangedChance = 1;
        if (flyingChance <= 0) flyingChance = 1;

        float melee = (float)meleeChance / totalPoints;
        float ranged = (float)rangedChance / totalPoints;
        float flying = (float)flyingChance / totalPoints;

        //Debug.Log("Melee Chance: " + melee + "\tRanged Chance: " + ranged + "\tFlying Chance: " + flying);

        if(d100Normalised <= melee)
        {
            SpawnMeleeUnit();
        }
        else if(d100Normalised > melee && d100Normalised <= ranged)
        {
            SpawnRangedUnit();
        }
        else if(d100Normalised > ranged && d100Normalised <= flying)
        {
            SpawnFlyingUnit();
        }
        else
        {
            SpawnRandomUnit();
        }
    }



    private int RollDiceN(int n)
    {
        return Random.Range(1, n+1);
    }



    private void ResetTimerVariables()
    {
        timer = 0f;
        timeBetweenUnits = Random.Range(minTimeBetweenUnits, maxTimeBetweenUnits);
    }
}
