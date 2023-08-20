using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCollider : MonoBehaviour
{
    public GameObject OnCollisionEnter2D(Collision2D collision)
    {
        return collision.gameObject;
    }
}
