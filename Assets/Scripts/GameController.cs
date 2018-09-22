using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private WeightClass playerWeightClass = WeightClass.Light;
    private WeightClass enemyWeightClass = WeightClass.Light;

    private List<GameObject> playerUnits = new List<GameObject>();
    private List<GameObject> enemyUnits = new List<GameObject>();




    // Use this for initialization
    void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}



    public WeightClass GetPlayerWeightClass()
    {
        return playerWeightClass;
    }

    public WeightClass GetEnemyWeightClass()
    {
        return enemyWeightClass;
    }


    public void AdvancePlayerWeightClass()
    {
        if(playerWeightClass == WeightClass.Light)
        {
            playerWeightClass = WeightClass.Medium;
        }
        else if(playerWeightClass == WeightClass.Medium)
        {
            playerWeightClass = WeightClass.Heavy;
        }
    }

    public void AdvanceEnemyWeightClass()
    {
        if (enemyWeightClass == WeightClass.Light)
        {
            enemyWeightClass = WeightClass.Medium;
        }
        else if (enemyWeightClass == WeightClass.Medium)
        {
            enemyWeightClass = WeightClass.Heavy;
        }
    }






    public void AddPlayerUnit(GameObject inUnit)
    {
        playerUnits.Add(inUnit);
        //PrintGOList(Commander.Player, playerUnits);
    }

    public void RemovePlayerUnit(GameObject inUnit)
    {
        playerUnits.Remove(inUnit);
        //PrintGOList(Commander.Player, playerUnits);
    }

    public void AddEnemyUnit(GameObject inUnit)
    {
        enemyUnits.Add(inUnit);
        //PrintGOList(Commander.Enemy, enemyUnits);
    }

    public void RemoveEnemyUnit(GameObject inUnit)
    {
        enemyUnits.Remove(inUnit);
        //PrintGOList(Commander.Enemy, enemyUnits);
    }


    private void PrintGOList(Commander inCommander, List<GameObject> inList)
    {
        Debug.Log(inCommander.ToString().ToUpper() + " LIST!\n");

        for(int i=0; i < inList.Count; i++)
        {
            Debug.Log("\t" + inList[i].name + "\n");
        }
    }
}

