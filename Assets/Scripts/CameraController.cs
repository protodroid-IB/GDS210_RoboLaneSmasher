using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    private GameController gameController;

    DeviceType deviceType = DeviceType.Desktop;

    private Vector3 previousPos;

    [Header("The left and right most positions the camera is allowed to be at:")]
    [SerializeField]
    private Transform leftBoundary, rightBoundary;


    private bool moveLeft = true, moveRight = true;






    private void Awake()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }


    // Use this for initialization
    void Start ()
    {
        previousPos = transform.position;

        deviceType = SystemInfo.deviceType;

    }
	








	// Update is called once per frame
	void Update ()
    {
        if(!gameController.IsPaused())
        {
            switch (deviceType)
            {
                case DeviceType.Desktop:
                    DragCameraPC();
                    break;

                case DeviceType.Handheld:
                    DragCameraAndroid();
                    break;
            }
        }
        
	}








    private void DragCameraAndroid()
    { 
        // if one finger is touching the screen
        if(Input.touchCount == 1)
        {
            // if that finger has moved
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                // find worldspace of the finger touching the screen
                Vector3 newPos = new Vector3(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).x, transform.position.y, transform.position.z);

                DragCamToPosition(newPos);
            }

            // set previous position to the now current position
            previousPos = new Vector3(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).x, transform.position.y, transform.position.z);
        }
    } 












    private void DragCameraPC()
    {
        // if the mouse button is being held down
        if (Input.GetMouseButton(0))
        {
            // find worldspace of the mouse
            Vector3 newPos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, transform.position.y, transform.position.z);

            DragCamToPosition(newPos);
        }

        // set previous position to the now current position
        previousPos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, transform.position.y, transform.position.z);
    }













    private void DragCamToPosition(Vector3 newPos)
    {
        // translate the camera's position between the new position and the cameras previous position
        transform.Translate(-(newPos - previousPos));

        DetectCameraBoundaries();
    }






    private void DetectCameraBoundaries()
    {
        if(transform.position.x < leftBoundary.transform.position.x)
        {
            transform.position = new Vector3(leftBoundary.transform.position.x, transform.position.y, transform.position.z);
        }

        if (transform.position.x > rightBoundary.transform.position.x)
        {
            transform.position = new Vector3(rightBoundary.transform.position.x, transform.position.y, transform.position.z);
        }
    }
}
