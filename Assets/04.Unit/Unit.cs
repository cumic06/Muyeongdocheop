using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct UnitStatInfo
{
    public int MaxHp;
    public int MinHp;
    public float MoveSpeed;
    public float AttackPower;
}

public class Unit : MonoBehaviour/*, IDamageable*/
{
    [SerializeField] protected UnitStatInfo unitStat;
    public UnitStatInfo UnitStat => unitStat;

    protected int hp;
    public int Hp => hp;

    protected bool isDead = false;

    protected virtual void Start()
    {
        ReSetStat();
    }

    protected void ReSetStat()
    {
        ResetHp();
    }

    protected virtual void ResetHp()
    {
        hp = UnitStat.MaxHp;
        unitStat.MinHp = 0;
    }

    public virtual void TakeDamage(int damageValue)
    {
        ChangeHp(-damageValue);
    }

    public virtual void HealHp(int healValue)
    {
        ChangeHp(healValue);
    }

    protected void ChangeHp(int value)
    {
        hp += ClampHp(value);
        if (Hp <= UnitStat.MinHp)
        {
            isDead = true;
        }
    }

    protected int ClampHp(int value)
    {
        if (Hp + value >= UnitStat.MaxHp)
        {
            hp = UnitStat.MaxHp;
        }

        if (Hp + value <= UnitStat.MinHp)
        {
            hp = UnitStat.MinHp;
        }
        return value;
    }

    protected virtual void Death()
    {
        if (isDead)
        {
            
        }
    }
}