using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHealth : MonoBehaviour
{
    [SerializeField]
    private int maxHealth;

    private int currentHealth;

    [SerializeField]
    private RectTransform healthTransform;

	// Use this for initialization
	void Start ()
    {
        currentHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void SubtractHealth(int inHealth)
    {
        currentHealth -= inHealth;

        if (currentHealth < 0) currentHealth = 0;

        float healthRatio = (float)currentHealth / (float)maxHealth;

        healthTransform.localScale = new Vector3(healthTransform.localScale.x, healthRatio, healthTransform.localScale.z);
    }
}
