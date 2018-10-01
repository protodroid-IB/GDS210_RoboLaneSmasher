using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHealth : MonoBehaviour
{
    private GameController gameController;

    [SerializeField]
    private int maxHealth;

    private int currentHealth;

    [SerializeField]
    private RectTransform healthTransform;

	// Use this for initialization
	void Start ()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        currentHealth = maxHealth;
	}


    public void SubtractHealth(int inHealth)
    {
        currentHealth -= inHealth;

        if (currentHealth < 0) currentHealth = 0;

        float healthRatio = (float)currentHealth / (float)maxHealth;

        healthTransform.localScale = new Vector3(healthTransform.localScale.x, healthRatio, healthTransform.localScale.z);

        CheckDead();
    }


    private void CheckDead()
    {
        if(currentHealth <= 0)
        {
            if(gameObject.tag == "PlayerBase")
            {
                gameController.SetBattleWinner(Commander.Enemy);
            }

            if(gameObject.tag == "EnemyBase")
            {
                gameController.SetBattleWinner(Commander.Player);
            }
        }
    }
}
