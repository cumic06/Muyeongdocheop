using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingScript : MonoBehaviour
{
    public GameObject SettingUI;
    public GameObject GameUI;

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
}
