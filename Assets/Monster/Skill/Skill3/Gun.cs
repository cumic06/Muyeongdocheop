using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private GameObject Bullet;
    private void Start()
    {
        StartCoroutine(launch());
    }
    IEnumerator  launch()
    {
        WaitForSeconds Wait = new WaitForSeconds(2.5f);
        while (true) 
        {
            Instantiate(Bullet,transform.position,Quaternion.identity);
            yield return Wait;
        }
    }
}
