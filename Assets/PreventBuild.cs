using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreventBuild : MonoBehaviour
{

    private bool canBuild = true;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Unit")
        {
            canBuild = false;
            Debug.Log("UNIT COLLIDED!");
        }
        else canBuild = true;
    }

    public bool CanBuild()
    {
        return canBuild;
    }
}
