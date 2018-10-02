using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBoard : MonoBehaviour
{
    [SerializeField]
    private AudioSource buttonSelectAudioSource;

    [SerializeField]
    private AudioClip buttonSelectSound, advanceWeightClass;


    [SerializeField]
    private AudioClip unitConstructedSound, unitMoveSound, unitShootSound, unitExplodeSound;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}



    public void ButtonSelectSound()
    {
        buttonSelectAudioSource.clip = buttonSelectSound;
        buttonSelectAudioSource.loop = false;
        buttonSelectAudioSource.Play();
    }



    public void UnitMoveSound(ref AudioSource inAudioSource)
    {
        if(inAudioSource.clip != unitMoveSound)
        {
            inAudioSource.clip = unitMoveSound;
            inAudioSource.Play();
            inAudioSource.loop = true;
        }  
    }


    public void UnitShootSound(ref AudioSource inAudioSource)
    {
            inAudioSource.clip = unitShootSound;
            inAudioSource.Play();
            inAudioSource.loop = false;
    }


    public void AdvanceWeightClass()
    {
        buttonSelectAudioSource.clip = advanceWeightClass;
        buttonSelectAudioSource.loop = false;
        buttonSelectAudioSource.Play();
    }



    public void UnitExplodeSound(ref AudioSource inAudioSource)
    {
        if(inAudioSource.clip != unitExplodeSound)
        {
            inAudioSource.clip = unitExplodeSound;
            inAudioSource.loop = false;
            inAudioSource.Play();
        }
        
    }


    public void UnitConstructedSound(ref AudioSource inAudioSource)
    {
            inAudioSource.clip = unitConstructedSound;
            inAudioSource.loop = false;
            inAudioSource.Play();
    }
}
