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
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}





    public WeightClass GetPlayerWeightClass()
    {
        return playerWeightClass;
    }

    public WeightClass GetEnemyWeightClass()
    {
        return enemyWeightClass;
    }






    public void AddPlayerUnit(GameObject inUnit)
    {
        playerUnits.Add(inUnit);
    }

    public void RemovePlayerUnit(GameObject inUnit)
    {
        playerUnits.Remove(inUnit);
    }

    public void AddEnemyUnit(GameObject inUnit)
    {
        enemyUnits.Add(inUnit);
    }

    public void RemoveEnemyUnit(GameObject inUnit)
    {
        enemyUnits.Remove(inUnit);
    }
}

