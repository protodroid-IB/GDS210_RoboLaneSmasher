using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    private Commander commander = Commander.Player;

    [SerializeField]
    private Transform raycastOrigin;

    [SerializeField]
    private float raycastRange = 100f;

    [SerializeField]
    private float stoppingDistance = 3f;

    [SerializeField]
    private float moveSpeed = 2f;

    private UnitStates unitState = UnitStates.Move;




	
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
        throw new UnassignedReferenceException("CheckAttackingRange() METHOD NOT IMPLEMENTED YET!");
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
