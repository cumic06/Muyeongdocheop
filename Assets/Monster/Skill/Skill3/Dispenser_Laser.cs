using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispenser_Laser : MonoBehaviour
{
    [SerializeField]
    private float Time_;

    [SerializeField]
    private Vector2 XY;

    [SerializeField]
    private ScriptTableObject_ Table;

    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private GameObject Laser;
    void Start()
    {
        LaserOn();
    }

    void LaserOn()
    {
        Laser = gameObject.transform.GetChild(0).gameObject;
        Table.Scale(Laser, XY, layerMask);
        StartCoroutine(LaserLunch());
    }

    IEnumerator LaserLunch()
    {
        WaitForSeconds Wait = new WaitForSeconds(Time_);
        while (true) 
        {
            
            yield return Wait;
            Laser.SetActive(false);
            yield return Wait;
            Laser.SetActive(true);
        }
    }
}
