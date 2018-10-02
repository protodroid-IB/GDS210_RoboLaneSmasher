using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastForward : MonoBehaviour
{
    [SerializeField]
    private float increasedGameSpeed = 2f;

    public void SpeedUp()
    {
        Time.timeScale = increasedGameSpeed;
    }

    public void SpeedNormal()
    {
        Time.timeScale = 1.0f;
    }
	
}
