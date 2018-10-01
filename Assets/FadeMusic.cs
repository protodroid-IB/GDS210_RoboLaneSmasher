using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FadeMusic : MonoBehaviour
{
    private AudioSource thisAudio;

    private GameController gameController;

    private Animator thisAnimator;

    // Use this for initialization
    void Start ()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        thisAudio = GetComponent<AudioSource>();
        thisAnimator = GetComponent<Animator>();
    }


    private void OnEnable()
    { 
        thisAnimator.SetTrigger("GameFadeIn");
    }

    public void EnterPauseMenu()
    {
        thisAnimator.SetTrigger("PauseFadeIn");
    }

    public void ExitPauseMenu()
    {
        thisAnimator.SetTrigger("PauseFadeOut");
    }

    public void EndGame()
    {
        thisAnimator.SetTrigger("GameFadeOut");
    }




}
