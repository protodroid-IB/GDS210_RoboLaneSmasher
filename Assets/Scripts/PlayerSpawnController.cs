using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnController : SpawnController
{

    public void SpawnMeleeUnit()
    {
        if(gameController.GetCurrentGameState() == GameState.InGame)
            CreateUnit(gameController.GetPlayerWeightClass(), UnitType.Melee);
    }

    public void SpawnRangedUnit()
    {
        if (gameController.GetCurrentGameState() == GameState.InGame)
            CreateUnit(gameController.GetPlayerWeightClass(), UnitType.Ranged);
    }

    public void SpawnFlyingUnit()
    {
        if (gameController.GetCurrentGameState() == GameState.InGame)
            CreateUnit(gameController.GetPlayerWeightClass(), UnitType.Flying);
    } 
}
