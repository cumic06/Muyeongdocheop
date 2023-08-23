using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool
{
    public GameObject c_poolObject;

    public Stack<GameObject> c_poolStack;

    public Pool(GameObject c_poolObject)
    {
        this.c_poolObject = c_poolObject;
    }
}

public class ObjectPooling : MonoSingleton<ObjectPooling>
{
    private Dictionary<GameObject, Pool> poolDic = new();

    public void GetObject(GameObject poolObject, Vector3 pos = default, Quaternion rotation = default)
    {
        GameObject returnObject;

        if (!poolDic.ContainsKey(poolObject))
        {
            poolDic.Add(poolObject, new(poolObject));
        }

        Pool pool = poolDic[poolObject];

        if (pool.c_poolStack.Count > 0)
        {
            returnObject = pool.c_poolStack.Pop();
            returnObject.SetActive(true);
        }
        else
        {
            returnObject = Instantiate(pool.c_poolObject);
            poolDic.Add(returnObject, pool);
        }
        returnObject.transform.SetPositionAndRotation(pos, rotation);

    }

    public void DestroyObject(GameObject poolObject, float time)
    {
        poolObject.transform.position = Vector2.zero;
        poolObject.SetActive(false);
        poolDic[poolObject].c_poolStack.Push(poolObject);
    }
}
