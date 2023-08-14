using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(AudioSource))]
public class SoundSystem : Singleton<SoundSystem>
{
    private AudioSource sfx;

    protected override void Awake()
    {
        base.Awake();
        sfx = GetComponent<AudioSource>();
    }

    //private void Start()
    //{
    //    PlayBGM();
    //}

    public void PlaySound(AudioClip audioClip, float volume = 0.5f)
    {
        sfx.PlayOneShot(audioClip, volume);
    }

    //public void PlayBGM()
    //{

    //}
}
