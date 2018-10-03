using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab;

    [SerializeField]
    private Transform projectileSpawn;

    [SerializeField]
    private float projectileSpeed = 3f;

    private Transform projectilesInHierarchy;


    private GameObject projectile = null;

    private Transform targetTransform;

    private BaseUnit thisUnit;


    private AudioManager audioManager;
    private AudioSource[] unitAudioSources;



    // Use this for initialization
    void Start ()
    {
        thisUnit = GetComponent<BaseUnit>();
        projectilesInHierarchy = GameObject.FindWithTag("ProjectilesInHierarchy").transform;

        audioManager = AudioManager.instance;
        unitAudioSources = GetComponents<AudioSource>();
    }
	

    public void Fire()
    {
        if (thisUnit.GetTargetUnit() != null) targetTransform = thisUnit.GetTargetUnit().transform;

        if (thisUnit.GetTargetBase() != null) targetTransform = thisUnit.GetTargetBase().transform;

        projectile = Instantiate(projectilePrefab, projectileSpawn.position, Quaternion.identity, projectilesInHierarchy);

        projectile.GetComponent<ProjectileBehaviour>().SetProjectileDetails(targetTransform, projectileSpeed);

        audioManager.PlaySound("Fire1", unitAudioSources[1]);
    }

    
}
