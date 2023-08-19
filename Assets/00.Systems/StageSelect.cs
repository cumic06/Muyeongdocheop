using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelect : MonoBehaviour
{
    public void stage_number1()
    {
        SceneManager.LoadScene(0);
    }
    public void Main_Stage()
    {
        SceneManager.LoadScene(4);
    }
    public void stage_number2()
    {
        SceneManager.LoadScene(3);
    }
}
