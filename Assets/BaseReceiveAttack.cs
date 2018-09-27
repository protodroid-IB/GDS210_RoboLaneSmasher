using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseReceiveAttack : MonoBehaviour
{


    private BaseHealth theBase;

    private void Start()
    {
        theBase = transform.parent.GetComponent<BaseHealth>();
    }

    public void SubtractHealth(int inHealth)
    {
        theBase.SubtractHealth(inHealth);
    }
}
