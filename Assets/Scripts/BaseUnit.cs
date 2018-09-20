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

    private bool attacking = false;

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
            Debug.Log("Attack!");

            Attack();
        }
	}





    private void Move()
    {
        transform.position += transform.right * moveSpeed * Time.deltaTime;

        if(CheckStoppingRange() == true)
        {
            unitState = UnitStates.Idle;
        }

        if (attacking == false)
        {
            unitAnimator.SetBool("Move", true);
            unitAnimator.SetBool("Attack", false);
        }


    }




    private void Idle()
    {
        if(CheckStoppingRange() == false)
        {
            unitState = UnitStates.Move;
        }

        if (attacking == false)
        {
            unitAnimator.SetBool("Idle", true);
            unitAnimator.SetBool("Attack", false);
        }
    }






    private void Attack()
    {
        if(attackTimer == 0)
        {
            attacking = true;
            unitAnimator.SetBool("Attack", true);
            unitAnimator.SetBool("Move", false);
            unitAnimator.SetBool("Idle", false);
        }

        if (unitAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f && attacking == true)
        {
            attacking = false;
        }

        attackTimer += Time.deltaTime;

        if (attackTimer >= attackRate) attackTimer = 0f;
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

                //Debug.Log("Hit the collidable object " + hit.collider.name);
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
