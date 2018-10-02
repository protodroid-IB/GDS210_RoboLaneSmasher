using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // self reminder for finding audio FindObjectOfType<AudioManager>().Play("Soundname");

    // array of all sounds within the game
    public Sound[] sounds;

    // keep track of any existing audio managers within the scene
    public static AudioManager instance;

    public AudioMixer audioMixer;
    
    // execute on awake
    void Awake ()
    {
        // if instance isn't added an audio manager into the scene else if there is double delete it 
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // makes object persistent in all scenes
        DontDestroyOnLoad(gameObject);

        // makes audio component changable within the audio manager
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        // creates an error message whenever a clip in not found 
        if (s == null)
        {
            Debug.LogWarning("Sound:" + name + " not found");
            return;
        }
        // play audio    
        s.source.Play();
    }

    public void SetVolumeSFX(float volume)
    {
        audioMixer.SetFloat("SFX", volume);
    }
    public void SetVolumeMain(float volume)
    {
        audioMixer.SetFloat("Master", volume);
    }
    public void SetVolumeMusic(float volume)
    {
        audioMixer.SetFloat("Music", volume);
    }
}
