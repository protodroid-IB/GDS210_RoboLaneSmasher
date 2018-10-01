using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnController : MonoBehaviour
{

    private GameController gameController;
    private ResourceController resourceController;
    private BuildQueue buildQueue;

    private UnitDatabase unitDatabase;

    [SerializeField]
    private Transform spawnPos, playerUnitsHierarchy;



    // Use this for initialization
    void Start ()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        resourceController = GameObject.FindWithTag("GameController").GetComponent<ResourceController>();
        unitDatabase = transform.parent.GetComponent<UnitDatabase>();
        buildQueue = GetComponent<BuildQueue>();

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}



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




    private void CreateUnit(WeightClass inClass, UnitType inType)
    {
            Unit newUnit = unitDatabase.FindUnit(inClass, inType); // grab the unit from the database 
            BaseUnit newUnitDetails = newUnit.prefab.GetComponent<BaseUnit>();

            int buildCost = newUnitDetails.GetBuildCost(); // grab the cost to build the unit

            // if the unit cost is affordable
            if (CanAfford(buildCost))
            {
                // grab the build time of the unit
                float buildTime = newUnitDetails.GetBuildTime();
                Sprite unitIcon = newUnitDetails.GetIcon();

                // add to build queue
                buildQueue.AddToQueue(newUnit.prefab, spawnPos.position + newUnit.prefab.transform.localPosition, Quaternion.identity, playerUnitsHierarchy, buildTime, unitIcon);
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
