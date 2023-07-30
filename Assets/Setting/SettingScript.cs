using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingScript : MonoBehaviour
{
    public GameObject SettingUI;
    public GameObject GameUI;

    public void setting_window()
    {
        SettingUI.SetActive(true);
        GameUI.SetActive(false);
    }
}
