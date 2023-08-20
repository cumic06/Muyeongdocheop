using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum number { Plus = 1, Minus = -1 }
[CreateAssetMenu(fileName = "ScriptableObject", menuName = "ScriptableObject", order = 1)]
public class ScriptTableObject_ : ScriptableObject
{
    public void Move(GameObject Object, float X)
    {
        Object.transform.Translate(X, 0, 0);
    }

    public void Scale(GameObject Object, Vector2 XY, LayerMask Wall)
    {
        RaycastHit2D hit = Physics2D.Raycast(Object.transform.position, XY, 20, Wall);
        if (hit)
        {
            Object.transform.localScale = (XY.x == 0) ? new Vector3(1, -(XY.y * hit.distance), 1) : new Vector3(-(XY.x * hit.distance), 1, 1);
        }
    }
}
