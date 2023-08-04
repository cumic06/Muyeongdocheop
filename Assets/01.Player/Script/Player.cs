using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    private readonly float hitShakeTime = 0.5f;
    private readonly float hitShakePower = 0.05f;

    protected override void ResetHp()
    {
        unitStat.MaxHp = 100;
        base.ResetHp();
    }

    protected override void ResetSpeed()
    {
        unitStat.MoveSpeed = 7;
        base.ResetSpeed();
    }

    public override void TakeDamage(int damageValue)
    {
        base.TakeDamage(damageValue);
        UIManager.Instance.HitAction?.Invoke();
        CameraShakeSystem.Instance.CameraShake(hitShakeTime, hitShakePower);
    }

    protected override void Death()
    {
        base.Death();
    }
}
