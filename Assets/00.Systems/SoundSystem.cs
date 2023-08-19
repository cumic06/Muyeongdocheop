using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundSystem : Singleton<SoundSystem>
{
    #region º¯¼ö
    [SerializeField] private AudioSource bgmSource;

    [SerializeField] private AudioSource fxSource;
    #endregion

    private void Start()
    {
        if (PlayerPrefs.HasKey("bgm") == true)
        {
            bgmSource.volume = PlayerPrefs.GetFloat("bgm");
        }
        if (PlayerPrefs.HasKey("SE") == true)
        {
            fxSource.volume = PlayerPrefs.GetFloat("SE");
        }
    }

    #region Play
    public void PlayFXSound(AudioClip fxClip, float soundValue)
    {
        fxSource.PlayOneShot(fxClip, soundValue);
    }

    public void PlayBGMSound(AudioClip bgmClip, float bgmValue)
    {
        bgmSource.PlayOneShot(bgmClip, bgmValue);
        bgmSource.volume = bgmValue;
    }
    #endregion

    #region GetSet
    public float GetBGMVolume()
    {
        return bgmSource.volume;
    }

    public float GetFXVolume()
    {
        return fxSource.volume;
    }

    public void SetBGMVolume(float volumeValue)
    {
        bgmSource.volume = volumeValue;
    }

    public void SetFXVolume(float volumeValue)
    {
        fxSource.volume = volumeValue;
    }
    #endregion
}