﻿using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    private AudioSource source;

    public string clipName; 

    public AudioClip clip;
    public AudioMixerGroup audioMixerGroup;

    [Range (0f,1f)]
    public float volume;
    [Range (0f,3f)]
    public float pitch;

    public bool loop =  false;
    public bool playOnAwake = false;
    public bool oneShot = false;

    

    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
        source.volume = volume;
        source.playOnAwake = playOnAwake;
        source.loop = loop;
        source.outputAudioMixerGroup = audioMixerGroup;
    }

    public void Play()
    {
        if (oneShot == true)
        {
            if(!source.isPlaying)
                source.Play();
        }
        else source.Play();
    }


    public void Play(AudioSource _audioSource)
    {
        SetSource(_audioSource);

        if (oneShot == true)
        {
            if (!_audioSource.isPlaying)
                _audioSource.Play();
        }
        else _audioSource.Play();
    }
}
