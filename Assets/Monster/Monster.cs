using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.collider.TryGetComponent(out Player player);
        player.TakeDamage(10);
    }
}
