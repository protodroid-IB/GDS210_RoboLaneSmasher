using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 movement = Vector3.zero;

    [SerializeField]
    private float maxCamSpeed = 5f, camAcceleration = 0.05f;

    private bool moveLeft = true, moveRight = true;


    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Move();
        UpdateCameraPosition();
	}

    // this method takes the players inputs in and moves the camera horizontally
    private void Move()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f && moveRight == true)
        {
            movement = new Vector3(Mathf.Lerp(movement.x, maxCamSpeed, camAcceleration), movement.y, movement.z);
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f && moveLeft == true)
        {
            movement = new Vector3(Mathf.Lerp(movement.x, -maxCamSpeed, camAcceleration), movement.y, movement.z);
        }
    }


    void UpdateCameraPosition()
    {
        transform.position += (movement * Time.deltaTime);

        if(moveLeft == false || moveRight == false)
        {
            movement = Vector3.zero;
        }
        else
        {
            movement = new Vector3(Mathf.Lerp(movement.x, 0f, camAcceleration), movement.y, movement.z);
        }
        
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("CameraBoundaryPlayer"))
        {
            moveLeft = false;
        }

        if (collision.CompareTag("CameraBoundaryEnemy"))
        {
            moveRight = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("CameraBoundaryPlayer"))
        {
            moveLeft = true;
        }

        if (collision.CompareTag("CameraBoundaryEnemy"))
        {
            moveRight = true;
        }
    }
}
