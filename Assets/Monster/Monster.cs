using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Unit
{
    protected override void ResetHp()
    {
        unitStat.MaxHp = 10;
        base.ResetHp();
    }

    protected override void ResetAttackPower()
    {
        unitStat.AttackPower = 5;
    }

    protected override void ResetSpeed()
    {
        unitStat.MoveSpeed = 5;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Player player))
        {
            player.TakeDamage(10);
        }
    }

    protected override void Death()
    {
        base.Death();
        Destroy(gameObject);
    }
}
