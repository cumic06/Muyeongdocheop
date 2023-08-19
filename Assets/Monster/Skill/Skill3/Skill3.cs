using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill3 : MonoBehaviour
{
   
    [SerializeField]
    private GameObject [] Map  = new GameObject[3];
    void Start()
    {
        Bring();

    }



    void Bring()
    {
        for (int i = 0; i < 3; i++)
        {
            Map[i] = MapManager.Instance.all.Save_Map[1].transform.GetChild(i).gameObject;
        }
    }
}
