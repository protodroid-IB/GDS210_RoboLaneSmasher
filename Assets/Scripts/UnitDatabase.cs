using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDatabase : MonoBehaviour
{
    [SerializeField]
    private Unit[] units;


    public Unit FindUnit(WeightClass inClass, UnitType inType)
    {
        Unit outUnit = units[0];

        for(int i=0; i < units.Length; i++)
        {
            if(units[i].weightClass.Equals(inClass))
            {
                if(units[i].unitType.Equals(inType))
                {
                    outUnit = units[i];
                }
            }
        }

        return outUnit;
    }
}

