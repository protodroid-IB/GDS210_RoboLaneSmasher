using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreventBuild : MonoBehaviour
{
    [SerializeField]
    private Commander commander = Commander.Player;

    private bool canBuild = true;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Unit")
        {
            if(collision.gameObject.GetComponent<BaseUnit>().GetCommander() == commander)
            {
                canBuild = false;
            }
        }
        else canBuild = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Unit")
        {
            if (collision.gameObject.GetComponent<BaseUnit>().GetCommander() == commander)
            {
                canBuild = true;
            }
        }
    }

    public bool CanBuild()
    {
        return canBuild;
    }
}
