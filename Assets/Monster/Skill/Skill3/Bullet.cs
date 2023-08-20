using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private number num;
    [SerializeField]
    private ScriptTableObject_ Table;
    private void Start()
    {
        StartCoroutine(Des());
    }
    private void FixedUpdate()
    {
        Table.Move(gameObject,((int)num*0.3f));
    }

    IEnumerator Des()
    {
        yield return new WaitForSeconds(4);
        Destroy(gameObject);
    }
}
