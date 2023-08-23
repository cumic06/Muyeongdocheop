using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sawblade : MonoBehaviour
{
    [SerializeField]
    private enum Index { One, Twom, Three, Four, Five };
    [SerializeField]
    private GameObject[] Position_1 = new GameObject[4];
    [SerializeField]
    private GameObject[] Position_2 = new GameObject[2];
    [SerializeField]
    private GameObject[] Position_3 = new GameObject[2];

    //Array[] Position = new Array[3] { Position_1 , Position_2, Position_3};
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Bagier());
    }

    IEnumerator Bagier()
    {
        while (true)
        {
            foreach (GameObject item in Position_1)
            {
                Vector3 startPos = transform.position;
                Vector3 targetPos = item.transform.position;
                for (float j = 0; j < 1; j += Time.fixedDeltaTime * 0.07f)
                {
                    gameObject.transform.position = Vector3.Lerp(startPos, targetPos, j);
                    yield return new WaitForFixedUpdate();
                }
            }
        }
    }

}
