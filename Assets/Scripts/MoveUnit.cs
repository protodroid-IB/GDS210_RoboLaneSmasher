using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUnit : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 2f;

    private UnitStates unitState = UnitStates.Move;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        switch(unitState)
        {
            case UnitStates.Move:
                Move();
                break;

            default:
                Idle();
                break;
        }
        
	}


    private void Move()
    {
        transform.position += Vector3.right * moveSpeed * Time.deltaTime;
    }

    private void Idle()
    {
        throw new UnassignedReferenceException("IDLE METHOD NOT IMPLEMENTED YET!");
    }
}
