using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FastForward : MonoBehaviour
{
    [SerializeField]
    private float increasedGameSpeed = 2f;

    [SerializeField]
    private Sprite[] toggleSprites;

    [SerializeField]
    private Image fastForwardImage;

    private bool spedUp = false;

    private void Start()
    {
        if(spedUp == false)
        {
            fastForwardImage.sprite = toggleSprites[0];
        }
    }



    public void Toggle()
    {
        if (spedUp == false)
        {
            fastForwardImage.sprite = toggleSprites[1];
            Time.timeScale = increasedGameSpeed;
            spedUp = true;
        }
        else
        {
            fastForwardImage.sprite = toggleSprites[0];
            Time.timeScale = 1.0f;
            spedUp = false;
        }
    }
	
}
