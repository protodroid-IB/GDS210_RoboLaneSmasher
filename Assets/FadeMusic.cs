using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FadeMusic : MonoBehaviour
{
    private AudioSource thisAudio;

    private bool paused = false;

    private bool startFade = false;


    // Use this for initialization
    void Awake ()
    {
        thisAudio = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (startFade == true)
        {
            if (!paused)
            {

            }
        }
	}


    private void OnEnable()
    {
        // if the game is not paused
        startFade = true;
    }

    public void SetPaused(bool inPaused)
    {
        paused = inPaused;
    }
}
