using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    [Header("CHOOSE THE COMMANDER: ")]
    [SerializeField]
    private Commander commander = Commander.Player;

    [Space(5)]
    [Header("RAYCAST DETAILS: ")]
    [SerializeField]
    private Transform raycastOrigin;

    [SerializeField]
    private float raycastRange = 8f;

    [SerializeField]
    private float stoppingDistance = 3f, attackingRange = 1.5f;


    [Space(5)]
    [Header("UNIT STATISTICS")]
    [Space(1)]

    [SerializeField]
    private float health;

    [SerializeField]
    private float moveSpeed = 2f;

    [SerializeField]
    private float damagePerHit;

    [SerializeField]
    private float hitPerSecond;

    [SerializeField]
    private float buildTime;

    [SerializeField]
    private float buildCost;

    [SerializeField]
    private float scrapDrop;

    [SerializeField]
    private float expDrop;








    private UnitStates unitState = UnitStates.Move;

    private int attackLayerMask;

    private float attackTimer = 0f;

    private float attackRate;

    private GameObject targetUnit = null;

    private Animator unitAnimator;






    private void Awake()
    {
        unitAnimator = GetComponent<Animator>();
    }




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

        attackRate = 1f / hitPerSecond;
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
            Attack();
        }
	}





    private void Move()
    {
        unitAnimator.SetTrigger("Move");

        transform.position += transform.right * moveSpeed * Time.deltaTime;

        if(CheckStoppingRange() == true)
        {
            unitState = UnitStates.Idle;
        }
    }




    private void Idle()
    {
        unitAnimator.SetTrigger("Idle");

        if (CheckStoppingRange() == false)
        {
            unitState = UnitStates.Move;
        }
    }






    private void Attack()
    {
        //if (commander == Commander.Player) Debug.Log("AttackTimer: " + attackTimer + "\t AttackRate: " + attackRate);

        if (attackTimer == 0)
        {
            //if(commander == Commander.Player) Debug.Log("Attack!");
            unitAnimator.SetTrigger("Attack");
        }

       

        if (attackTimer >= attackRate)
        {
            attackTimer = 0f;
        }
        else
        {
            if(attackTimer != 0f)
            {
                if (CheckStoppingRange())
                {
                    unitState = UnitStates.Idle;
                }
                else
                {
                    unitState = UnitStates.Move;
                }
            }

            attackTimer += Time.deltaTime;
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
                //Debug.Log("Hit the collidable object " + hit.collider.name);
            }

            if (targetUnit != null)
            {
                if (targetUnit.transform.tag == "Unit")
                {
                    if (hit.distance <= attackingRange)
                    {
                        attack = true;
                    }

                }
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
            //Debug.DrawLine(raycastOrigin.position, hit.point, Color.red);

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
