using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    protected override void ResetHp()
    {
        unitStat.MaxHp = 100;
        base.ResetHp();
    }

    protected override void ResetSpeed()
    {
        unitStat.MoveSpeed = 5;
        base.ResetSpeed();
    }

    public override void TakeDamage(int damageValue)
    {
        base.TakeDamage(damageValue);
        Debug.Log("hp" + hp);
        UIManager.Instance.hitAction?.Invoke();
    }
}
