using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    private GameController gameController;
    private ResourceController resourceController;

    private UnitStates unitState = UnitStates.Move; // the current state of the unit



    // THE COMMANDER OF THIS UNIT (PLAYER OR ENEMY)
    [Header("CHOOSE THE COMMANDER: ")]
    [SerializeField]
    private Commander commander = Commander.Player;



    // UNIT ICON FOR UI PURPOSES
    [SerializeField]
    private Sprite iconUI;




    // RAYCASTING REFERENCES AND VARIABLES
    [Space(5)]
    [Header("RAYCAST DETAILS: ")]
    [SerializeField]
    private Transform raycastOrigin; // the origin of the raycast

    [SerializeField]
    private float raycastRange = 8f; // the maximum distance to cast the ray

    [SerializeField]
    private float stoppingDistance = 3f; // the distance in which a unit should stop moving

    [SerializeField] 
    private float attackingRange = 1.5f; // the distance in which the unit should start attacking respectively

    private int stoppingLayerMask;
    private int attackLayerMask; // a physics layer specially set up for the raycast that checks if an opposing unit is within attacking range






    // ANIMATION REFERENCES AND VARIABLES
    [Space(5)]
    [Header("ANIMATION DETAILS: ")]
    [SerializeField]
    private AnimationClip attackAnimation; // the attack animation clip used

    private float attackAnimLength; // the length of the attack animation

    private Animator unitAnimator; // the units animator





    // THE STATISTICS OF THIS UNIT
    [Space(5)]
    [Header("UNIT STATISTICS")]
    [Space(1)]

    [SerializeField]
    private int maxHealth;

    private int currentHealth;

    [SerializeField]
    private float moveSpeed = 2f;

    [SerializeField]
    private int damagePerHit;

    [SerializeField]
    private float hitPerSecond;

    [SerializeField]
    private float buildTime;

    [SerializeField]
    private int buildCost;

    [SerializeField]
    private int scrapDrop;

    [SerializeField]
    private int expDrop;



    // ATTACKING VARIABLES
    private bool attacking = false; // is the unit attacking?
    private float attackTimer = 0f; // the timer that controls attacks
    private float attackRate; // the time between attacks

    private GameObject targetUnitGO = null; // the unit to attacks gameobject reference
    private BaseUnit targetUnit = null; // the unit to attacks base unit script reference

    private GameObject targetBaseGO = null;
    private BaseReceiveAttack targetBase = null;





    private void Awake()
    {
        unitAnimator = GetComponent<Animator>(); // grab the animator 
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        resourceController = GameObject.FindWithTag("GameController").GetComponent<ResourceController>();
    }







    private void Start()
    {
        stoppingLayerMask = 1 << 8; // the physics layer that stopping raycasts are used

        // there is a physics layer for each commander type when attacking
        // this checks which the commander type is and assigns the correct physics layer mask
        if (commander == Commander.Player)
        {
            attackLayerMask = 1 << 9;
        }
        else
        {
            attackLayerMask = 1 << 10;
        }

        attackRate = 1f / hitPerSecond; // calculate the time between attacks

        currentHealth = maxHealth; // set the current health to full

        attackAnimLength = attackAnimation.length; // grab the length of the attack animation
    }







    void Update ()
    {
        // runs the appropriate method when the units state is changed
        switch(unitState)
        {
            case UnitStates.Move:
                Move();
                break;

            case UnitStates.Idle:
                Idle();
                break;

            case UnitStates.Death:
                Death();
                break;

            default:
                Idle();
                break;
        }




        // if this unit is still alive
        if (!CheckDead())
        {
            // if the unit is within its attacking range of an opposing unit
            if (CheckAttackingRange() == true)
            {
                // try to attack
                Attack();
            }
            // if the unit is not within attacking range of an opposing unit, set the target unit variables to null
            else
            {
                targetUnitGO = null;
                targetUnit = null;
                targetBaseGO = null;
                targetBase = null;
            }
        }
	}





    private void Move()
    {
        // start playing the move animation
        unitAnimator.SetTrigger("Move");

        // move the unit in world space
        transform.position += transform.right * moveSpeed * Time.deltaTime;


        // if this unit is alive
        if (!CheckDead())
        {
            // if there is another unit blocking this units path, change the state of this unit to idle (stops this unit moving)
            if (CheckStoppingRange() == true)
            {
                unitState = UnitStates.Idle;
            }
        }
        else unitState = UnitStates.Death;
    }







    private void Idle()
    {
        // start playing the idle animation
        unitAnimator.SetTrigger("Idle");

        // if this unit is alive
        if (!CheckDead())
        {
            // if there is no other unit blocking this units path, change the state of this unit to move (starts this unit moving)
            if (CheckStoppingRange() == false)
            {
                unitState = UnitStates.Move;
            }
        }
        else unitState = UnitStates.Death;
        
    }







    private void Attack()
    {
        // start attacking the target unit
        if (attackTimer == 0)
        {
            unitAnimator.SetTrigger("Attack"); // trigger the attack animation
            attacking = true; // switch the attacking boolean
        }

        // while currently attacking, when the attack animation has finished playing
        if(attackTimer >= attackAnimLength && attacking == true)
        {
            attacking = false; // switch the attack boolean

            if(targetUnit != null)
            {
                targetUnit.SubtractHealth(damagePerHit); // subtract health from the target unit
            }

            if (targetBase != null)
            {
                targetBase.SubtractHealth(damagePerHit);
            }

            


        }

        // when the time between attacks has passed, set attack timer to 0 - triggering the next attack
        if (attackTimer >= attackRate)
        {
            attackTimer = 0f;
        }
        // when the time between attacks has not yet passed
        else
        {
            // if the attack hasn't just been triggered
            if(attackTimer != 0f)
            {
                // check if in stopping range of a unit and set unit state to idle or move respectively
                if (CheckStoppingRange())
                {
                    unitState = UnitStates.Idle;
                }
                else
                {
                    unitState = UnitStates.Move;
                }
            }

            // increment the attack timer
            attackTimer += Time.deltaTime;
        }
    }







    private void Death()
    {
        // THIS IS FOR TESTING PURPOSES
        unitAnimator.speed = 0;
        Invoke("Kill", 0.25f);
        // END TEST



        // DEATH STUFF HERE!!!
    }


    // if this unit is dead
    private bool CheckDead()
    {
        if(currentHealth <= 0)
        {
            return true;
        }

        return false;
    }







    // This method checks to see if the unit is within range to attack a target unit - if it is it returns true otherwise returns false
    private bool CheckAttackingRange()
    {
        // determines if can attack
        bool attack = false;

        // cast a 2D ray on the attack physics layer and get the hit information
        RaycastHit2D hit = Physics2D.Raycast(raycastOrigin.position, transform.right, attackingRange, attackLayerMask);

        // if the raycast hit a collider
        if (hit.collider)
        {
            // if the collider belongs to a unit
            if(hit.collider.transform.tag == "Unit")
            {
                // if a target unit has not yet been set
                if (targetUnitGO == null)
                {
                    // set the target units GO and BaseUnit script
                    targetUnitGO = hit.transform.gameObject;
                    targetUnit = targetUnitGO.GetComponent<BaseUnit>();
                }

                // if a target unit has been assigned
                if (targetUnitGO != null)
                {
                    // if the distance between this unit and the target unit is smaller than the attackign range
                    if (hit.distance <= attackingRange)
                    {
                        attack = true; // this unit can attack!
                    }
                }
            }

            // if the collider belongs to a base
            else if(hit.collider.transform.tag == "Base")
            {
                // if a target base has not yet been set
                if (targetBaseGO == null)
                {
                    // set the target base GO and BaseHealth script
                    targetBaseGO = hit.transform.gameObject;
                    targetBase = targetBaseGO.GetComponent<BaseReceiveAttack>();
                }

                // if a target base has been assigned
                if (targetBaseGO != null)
                {
                    // if the distance between this unit and the target base is smaller than the attackign range
                    if (hit.distance <= attackingRange)
                    {
                        attack = true; // this unit can attack!
                    }
                }
            }
        }
        // if the raycast did not hit a collider
        else
        {
            attack = false;
        }

        return attack;
    }







    // this method checks to see if this unit is within it's stopping (idle) range - if it is it returns true otherwise returns false
    private bool CheckStoppingRange()
    {
        bool stop = false; // determines if can attack

        // cast a 2D ray on the stopping physics layer and get the hit information
        RaycastHit2D hit = Physics2D.Raycast(raycastOrigin.position, transform.right, raycastRange, stoppingLayerMask);

        // if the raycast hit a collider
        if (hit.collider)
        {
            // if that collider was a unit
            if (hit.transform.tag == "Unit")
            {
                // if that collider is within the stopping distance of this unit - this unit should stop moving
                if (hit.distance <= stoppingDistance)
                {
                    stop = true;
                }
            }

            else if (hit.transform.tag == "Base")
            {
                if (hit.distance <= stoppingDistance)
                {
                    stop = true;
                }
            }
        }

        return stop;
    }


    private void Kill()
    {
        
        Destroy(this.gameObject);
    } 




    // sets the commander of this unit
    public void SetCommander(Commander inCommander)
    {
        commander = inCommander;
    }

    // grabs the commander of this unit
    public Commander GetCommander()
    {
        return commander;
    }


    // calculates and returns the ratio of currenthealth to health - should always return a number between 0 & 1 (inclusive)
    public float GetHealthRatio()
    {   
        return (float)currentHealth / (float)maxHealth;
    }

    // subtracts health from this unit and caps the minimum to 0
    public void SubtractHealth(int inNum)
    {
        currentHealth -= inNum;

        if (currentHealth <= 0) currentHealth = 0;
    }

    public int GetBuildCost()
    {
        return buildCost;
    }

    public float GetBuildTime()
    {
        return buildTime;
    }

    public Sprite GetIcon()
    {
        return iconUI;
    }


    private void OnDestroy()
    {
        // when this unit is destroyed

        // if the commander of this unit is the player
        if(commander == Commander.Player)
        {
            gameController.RemovePlayerUnit(this.gameObject);
            resourceController.AddEnemyScrap(scrapDrop);
            resourceController.AddEnemyExp(expDrop);
        }
        
        // if the commander of this unit is the enemy
        else
        {
            gameController.RemoveEnemyUnit(this.gameObject);
            resourceController.AddPlayerScrap(scrapDrop);
            resourceController.AddPlayerExp(expDrop);
        }
    }


    public BaseReceiveAttack GetTargetBase()
    {
        return targetBase;
    }

    public BaseUnit GetTargetUnit()
    {
        return targetUnit;
    }


    //private void FireProjectile()
    //{
    //    if(rangedProjectile != null)
    //    {
    //        if(targetUnit != null)
    //        {
    //            rangedProjectile.Fire(targetUnit.transform);
    //        }

    //        if(targetBase != null)
    //        {
    //            rangedProjectile.Fire(targetBase.transform);
    //        } 
    //    }
    //}

}
