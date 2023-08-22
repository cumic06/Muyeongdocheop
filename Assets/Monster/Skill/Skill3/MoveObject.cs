using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UIElements;

public enum Index { One = 1, Twom, Three, Four, Five };
[System.Serializable]
public class MoveObject : MonoBehaviour
{
    [SerializeField]
    private float Time_;
    [SerializeField]
    private Index index;
    [SerializeField]
    private GameObject[] Position_1 = new GameObject[4];
    [SerializeField]
    private GameObject[] Position_2 = new GameObject[2];
    [SerializeField]
    private GameObject[] Position_3 = new GameObject[2];
    [SerializeField]
    private GameObject[] Position_4 = new GameObject[7];
    Dictionary<int, GameObject[]> Position = new Dictionary<int, GameObject[]>();
    void Start()
    {
        Position = new Dictionary<int, GameObject[]>()
        {
            {1,Position_1},
            {2,Position_2},
            {3,Position_3},
            {4,Position_4}
        };
        StartCoroutine(BeziorMove());
    }

    IEnumerator BeziorMove()
    {
        WaitForFixedUpdate fixedWait = new WaitForFixedUpdate();
        
        while (true)
        {
            foreach (GameObject item in Position[((int)index)])
            {
                Vector3 startPos = transform.position;
                Vector3 targetPos = item.transform.position;

                for (float j = 0; j < 1; j += Time.fixedDeltaTime * Time_)
                {
                    gameObject.transform.position = Vector3.Lerp(startPos, targetPos, j);
                    yield return fixedWait;
                }
            }
        }
    }
}
