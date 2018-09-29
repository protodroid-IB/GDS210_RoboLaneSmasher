using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{

    private float speed;

    private Transform target;


    private void Start()
    {
        Invoke("Kill", 3f);
    }

    // Update is called once per frame
    void Update ()
    {
        MoveProjectile();
    }

    public void SetProjectileDetails(Transform inTargetTransform, float inSpeed)
    {
        target = inTargetTransform;
        speed = inSpeed;
    }

    private void MoveProjectile()
    {
        Vector3 direction = transform.right;

        if (target != null)
        {
            direction = (target.position - transform.position).normalized;
        }

        transform.position += direction * speed * Time.deltaTime;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(target != null)
        {
            if (collision.transform.GetInstanceID() == target.GetInstanceID())
            {
                Kill();
            }
            else if(collision.transform.tag == "Base")
            {
                Kill();
            }
        }

    }

    private void Kill()
    {
        Destroy(this.gameObject);
    }
}
