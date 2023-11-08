using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField]
    private AudioSource musicSource, fbxSource;

    public static AudioManager Instance;

    private void Awake()
    {
        
        if(Instance != null && Instance != this)
        {

            Destroy(this);

        }
        else
        {

            Instance = this;
            DontDestroyOnLoad(this);

        }
    }

    public void PlaySound(AudioClip clip)
    {

        fbxSource.PlayOneShot(clip);

    }

    public void SetMusicVolumen(float value)
    {

        musicSource.volume += value; 

    }
    public void SetFbxVolumen(float value)
    {

        fbxSource.volume += value;

    }
}
