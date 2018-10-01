using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnController : SpawnController
{

    public void SpawnMeleeUnit()
    {
        CreateUnit(gameController.GetPlayerWeightClass(), UnitType.Melee);
    }

    public void SpawnRangedUnit()
    {
        CreateUnit(gameController.GetPlayerWeightClass(), UnitType.Ranged);
    }

    public void SpawnFlyingUnit()
    {
        CreateUnit(gameController.GetPlayerWeightClass(), UnitType.Flying);
    } 
}
