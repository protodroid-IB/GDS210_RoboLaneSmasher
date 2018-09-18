using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Commander { Player, Enemy };

public enum WeightClass { Light, Medium, Heavy };

public enum UnitType { Melee, Ranged, Flying };

[System.Serializable]
public struct Unit
{
    public WeightClass weightClass;
    public UnitType unitType;
    public GameObject prefab;
}


public enum UnitStates { Idle, Move, Attack, Death };
