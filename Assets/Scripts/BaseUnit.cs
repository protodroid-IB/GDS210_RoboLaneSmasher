using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    [SerializeField]
    private Commander commander = Commander.Player;

    [SerializeField]
    private Transform raycastOrigin;

    [SerializeField]
    private float raycastRange = 8f;

    [SerializeField]
    private float stoppingDistance = 3f, attackingRange = 1.5f;

    [SerializeField]
    private float moveSpeed = 2f;

    private UnitStates unitState = UnitStates.Move;

    private int attackLayerMask;

    private bool attacking = false;

    private GameObject targetUnit = null;


    private void Start()
    {
        if (commander == Commander.Player)
        {
            attackLayerMask = 1 << 9;
        }
        else
        {
            attackLayerMask = 1 << 10;
        }
    }


    // Update is called once per frame
    void Update ()
    {
        switch(unitState)
        {
            case UnitStates.Move:
                Move();
                break;

            case UnitStates.Idle:
                Idle();
                break;

            default:
                Idle();
                break;
        }
        
        if(CheckAttackingRange() == true)
        {
            Debug.Log("Attack!");

            // DO ATTACK STUFF HERE!!!
        }
	}





    private void Move()
    {
        transform.position += transform.right * moveSpeed * Time.deltaTime;

        if(CheckStoppingRange() == true)
        {
            unitState = UnitStates.Idle;
        }

    }

    private void Idle()
    {
        if(CheckStoppingRange() == false)
        {
            unitState = UnitStates.Move;
        }
    }


    private bool CheckAttackingRange()
    {
        bool attack = false;

        RaycastHit2D hit = Physics2D.Raycast(raycastOrigin.position, transform.right, attackingRange, attackLayerMask);

        if (hit.collider)
        {
            Debug.DrawLine(raycastOrigin.position, hit.point, Color.green);

            if(targetUnit == null)
            {
                targetUnit = hit.transform.gameObject;

                if(targetUnit.transform.tag == "Unit")
                {
                    if (hit.distance <= attackingRange)
                    {
                        attack = true;
                    }

                }

                Debug.Log("Hit the collidable object " + hit.collider.name);
            }
        }
        else
        {
            if(targetUnit == null)
            {
                attack = false;
            }
        }

        return attack;
    }





    private bool CheckStoppingRange()
    {
        bool stop = false;

        int layerMask = 1 << 8;

        RaycastHit2D hit = Physics2D.Raycast(raycastOrigin.position, transform.right, raycastRange, layerMask);

        if (hit.collider)
        {
            Debug.DrawLine(raycastOrigin.position, hit.point, Color.red);

            if (hit.transform.tag == "Unit")
            {
                //Debug.Log("Hit the collidable object " + hit.collider.name);

                if (hit.distance <= stoppingDistance)
                {
                    stop = true;
                }
            }
        }
        return stop;
    }

    public void SetCommander(Commander inCommander)
    {
        commander = inCommander;
    }

    public Commander GetCommander()
    {
        return commander;
    }
}
