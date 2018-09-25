using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildQueueUI : MonoBehaviour
{

    [SerializeField]
    private Image[] queueImages;

    private Color transparent = new Color(1f,1f,1f,0f);
    private Color firstColor = Color.white;
    private Color queuedColor = new Color(0f, 0f, 0f, 0.5f);

	// Use this for initialization
	void Start ()
    { 

	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void UpdateUI(bool[] emptyArray)
    {
        // if there is a unit in the first position of the queue
        if(!emptyArray[0])
        {
            // change sprite to unit icon
            // IMPLEMENT!

            queueImages[0].color = firstColor;  // change color to first unit color
        }

        // if there is no unit in the first position of the queue
        else
        {
            queueImages[0].color = transparent; // change color to transparent
        }

        // cycle through queue images and set each
        for (int i = 1; i < queueImages.Length; i++)
        {
            // if there is no unit in the position
            if(emptyArray[i])
            {
                queueImages[i].color = transparent; // change color to transparent
            }

            // if there is a unit in the position
            else
            {
                // change sprite to unit icon
                // IMPLEMENT!

                queueImages[i].color = queuedColor;   // change color to queued unit color
            }
        }
        
    }
}
