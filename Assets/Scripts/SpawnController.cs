using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField]
    private Commander commander = Commander.Player;

    public GameController gameController;
    public ResourceController resourceController;
    public BuildQueue buildQueue;

    public UnitDatabase unitDatabase;

    [SerializeField]
    private Transform spawnPos, unitsInHierarchy;

    private Quaternion enemyRotation = Quaternion.Euler(0f, 180f, 0f);



    // Use this for initialization
    void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        resourceController = gameController.GetComponent<ResourceController>();
        unitDatabase = transform.parent.GetComponent<UnitDatabase>();
        buildQueue = GetComponent<BuildQueue>();
    }




    public void CreateUnit(WeightClass inClass, UnitType inType)
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
            if(commander == Commander.Player)
            {
                buildQueue.AddToQueue(newUnit.prefab, spawnPos.position + newUnit.prefab.transform.localPosition, Quaternion.identity, unitsInHierarchy, buildTime, unitIcon);
                resourceController.SubtractPlayerScrap(buildCost);
            }
            else if(commander == Commander.Enemy)
            {
                buildQueue.AddToQueue(newUnit.prefab, new Vector3(spawnPos.position.x - newUnit.prefab.transform.localPosition.x, spawnPos.position.y + newUnit.prefab.transform.localPosition.y, spawnPos.position.z) , enemyRotation, unitsInHierarchy, buildTime, unitIcon);
                resourceController.SubtractEnemyScrap(buildCost);
            }
            
        }
    }




    private bool CanAfford(int inCost)
    {
        int availableFunds = 0;

        if (commander == Commander.Player) availableFunds = resourceController.GetPlayerScrap();
        else if (commander == Commander.Enemy) availableFunds = resourceController.GetEnemyScrap();

        if (inCost <= availableFunds)
        {
            return true;
        }

        return false;
    }
}