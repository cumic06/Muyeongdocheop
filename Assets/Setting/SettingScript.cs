using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingScript : MonoBehaviour
{
    public GameObject SettingUI;
    public GameObject GameUI;
    public Toggle Vibrate_Toggle;
    public int VibrateInt;

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
        GameUI.SetActive(false);
    }

    public void Setting_Exit()
    {
        SettingUI.SetActive(false);
        GameUI.SetActive(true);
    }

    public void Vibrate()
    {
        if (Vibrate_Toggle.isOn)
        {
            Vibration.Vibrate(150); // Vibrate 150ms
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
