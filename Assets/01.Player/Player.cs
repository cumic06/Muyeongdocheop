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

    public override void TakeDamage(int damageValue)
    {
        Debug.Log("hp" + hp);
        base.TakeDamage(damageValue);
        UIManager.Instance.hitAction?.Invoke();
    }
}
