using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    private AudioSource _audioSource;
    public bool sound;
    
    private void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
            DontDestroyOnLoad(this.gameObject);
        }

        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        sound = PlayerPrefsManager.Instance.GetSettingSound();
    }

    public void SoundOff()
    {
        sound = !sound;
        PlayerPrefsManager.Instance.SetSettingSound(sound);
    }

    public void PlaySoundFX(AudioClip audioClip, float volume)
    {
        if (sound)
        {
            _audioSource.PlayOneShot(audioClip, volume);
        }
    }
}
