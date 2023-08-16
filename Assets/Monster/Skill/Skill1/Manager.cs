using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField]
    private GameObject player_;

    

    protected virtual void Start()
    {
        gameObject.transform.position = player_.transform.position + new Vector3(-4, 3, 0);
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        WaitForSeconds wait = new WaitForSeconds(5);
        while (transform.position.y < 6)
        {
            transform.Translate(0, 1 * Time.deltaTime, 0);
            yield return null;
        }
    }
}
