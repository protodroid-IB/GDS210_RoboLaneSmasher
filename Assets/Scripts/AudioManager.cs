using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    // AudioManager audiomanager;
    // in start audiomanager = AudioManager.instance;
    // to play sound audioManager.PlaySound("Sound Name")
    public static AudioManager instance;

    [SerializeField]

    Sound[] sound; 

	// Use this for initialization
	void Awake ()
    {
        DontDestroyOnLoad(gameObject);
		if (instance == null)
        {
            instance = this;
        }
        else if ( instance != this )
        {
            Destroy(gameObject);
        }
	}

    void Start()
    {
        for (int i = 0; i < sound.Length; i++)
        {
            GameObject _go = new GameObject("Sound_" + i + "_" + sound[i].clipName);
            _go.transform.SetParent(this.transform);
            sound[i].SetSource(_go.AddComponent<AudioSource>());
        }

        PlaySound("Theme");
    }
    
    public void PlaySound(string _name)
    {
		for (int i = 0; i < sound.Length; i++)
        {
            if (sound[i].clipName == _name)
            {
                sound[i].Play();
                return;
            }
            
            
        }
	}
}
