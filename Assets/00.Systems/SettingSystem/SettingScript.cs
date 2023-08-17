using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingScript : MonoBehaviour
{
    [SerializeField] private GameObject SettingUI;
    [SerializeField] private Toggle Vibrate_Toggle;
    [SerializeField] private int VibrateInt;

    private void Start()
    {
        VibrateInt = PlayerPrefs.GetInt("Vibrate");

        if(VibrateInt == 0)
        {
            Vibrate_Toggle.isOn = true;
        }
    }

    public void Setting_Window()
    {
        SettingUI.SetActive(true);
    }

    public void Setting_Exit()
    {
        SettingUI.SetActive(false);
    }

    public void Vibrate()
    {
        if (Vibrate_Toggle.isOn)
        {
            Vibration.Vibrate(150); 
        }
    }

    public void Vibrate_save()
    {
        if(Vibrate_Toggle.isOn)
        {
            PlayerPrefs.SetInt("Vibrate", 0);
        }
        else
        {
            PlayerPrefs.SetInt("Vibrate", 1);
        }
    }
}
