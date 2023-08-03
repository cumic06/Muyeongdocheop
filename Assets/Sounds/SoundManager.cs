using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioSource Bgmsource;

    public AudioSource SE_source;

    public Slider BGMslider;

    public Slider SE_slider;

    private void Start()
    {
        if(PlayerPrefs.HasKey("bgm") == true)
        {
            Bgmsource.volume = PlayerPrefs.GetFloat("bgm");
        }
        if (PlayerPrefs.HasKey("SE") == true)
        {
            SE_source.volume = PlayerPrefs.GetFloat("SE");
        }
        BGMslider.value = Bgmsource.volume;
        SE_slider.value = SE_source.volume;
    }

    public void SetSoundBGM(float bgm)
    {
        Bgmsource.volume = bgm;
        PlayerPrefs.SetFloat("bgm", bgm);
        
    }

    public void SetSoundSE(float SE)
    {
        SE_source.volume = SE;
        PlayerPrefs.SetFloat("SE", SE);
    }
}
