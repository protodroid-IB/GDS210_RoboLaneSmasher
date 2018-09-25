using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildQueue : MonoBehaviour
{
    private GameController gameController;
    private BuildQueueUI buildQueueUI;

    private struct Build
    {
        public GameObject prefab;
        public Vector3 position;
        public Quaternion rotation;
        public Transform parent;
        public float buildTime;
        public Sprite buildIcon;
    }

    private List<Build> buildQueue;
    private bool[] emptyArray;
    private Sprite[] iconArray;

    private int nextEmptyIndex = 0;

    private int queueCapacity = 5;

    private float buildTimer = 0f;



    private void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        buildQueueUI = GameObject.FindWithTag("BuildQueueUI").GetComponent<BuildQueueUI>();

        buildQueue = new List<Build>();

        queueCapacity = 5;
        buildTimer = 0f;

        emptyArray = new bool[queueCapacity];
        iconArray = new Sprite[queueCapacity];

        for (int i = 0; i < emptyArray.Length; i++)
        {
            emptyArray[i] = true;
            iconArray[i] = null;
        }
    }



    public void AddToQueue(GameObject inPrefab, Vector3 inPos, Quaternion inRot, Transform inParent, float buildTime, Sprite inIcon)
    {
        // if the build queue is not full
        if(!IsFull())
        {
            // create a new build with units details
            Build newBuild = new Build();
            newBuild.prefab = inPrefab;
            newBuild.position = inPos;
            newBuild.rotation = inRot;
            newBuild.parent = inParent;
            newBuild.buildTime = buildTime;
            newBuild.buildIcon = inIcon;

            // add that build to the queue
            buildQueue.Add(newBuild);
            iconArray[nextEmptyIndex] = inIcon;
            emptyArray[nextEmptyIndex] = false;

            nextEmptyIndex++;

            // update UI
            buildQueueUI.UpdateUI(emptyArray, iconArray);
        }

    }




    private bool IsFull()
    {
        if(buildQueue.Count >= queueCapacity)
        {
            return true;
        }

        return false;
    }





    private bool IsEmpty()
    {
        if(buildQueue.Count <= 0)
        {
            return true;
        }

        return false;
    }



    private void Update()
    {
        // if there are builds in the queue
        if(!IsEmpty())
        {
            // if timer to build unit is complete
            if(buildTimer >= buildQueue[0].buildTime)
            {
                CreateTheUnit(); // create the unit
                buildQueue.RemoveAt(0); // remove the build in the queue

                nextEmptyIndex--; // decrement the next empty index

                emptyArray[nextEmptyIndex] = true; // set as empty 
                iconArray[nextEmptyIndex] = null; // set icon as null
                buildTimer = 0f; // reset the build timer

                // update UI
                buildQueueUI.UpdateUI(emptyArray, iconArray);

            }

            // increment the build timer
            buildTimer += Time.deltaTime;
        }
        else
        {
            buildTimer = 0f;
        }
    }





    private void CreateTheUnit()
    {
        GameObject newUnitGO = Instantiate(buildQueue[0].prefab, buildQueue[0].position, buildQueue[0].rotation, buildQueue[0].parent);

        if(newUnitGO.GetComponent<BaseUnit>().GetCommander() == Commander.Player)
        {
            gameController.AddPlayerUnit(newUnitGO);
        }
        else
        {
            gameController.AddEnemyUnit(newUnitGO);
        }
        
    }


}
