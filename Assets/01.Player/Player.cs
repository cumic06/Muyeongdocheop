using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    private readonly float hitShakeTime = 0.15f;
    private readonly float hitShakePower = 0.1f;

    protected override void ResetHp()
    {
        unitStat.MaxHp = 100;
        base.ResetHp();
    }

    protected override void ResetSpeed()
    {
        unitStat.MoveSpeed = 8;
        base.ResetSpeed();
    }

    public override void TakeDamage(int damageValue)
    {
        base.TakeDamage(damageValue);
        UIManager.Instance.HitAction?.Invoke();
        CameraShakeSystem.Instance.CameraShake(hitShakeTime, hitShakePower);
        SlowSystem.Instance.Slow(0.5f, 0.1f);
    }

    protected override void Death()
    {
        base.Death();

    }
}
