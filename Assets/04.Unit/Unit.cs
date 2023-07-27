using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct UnitStatInfo
{
    public int MaxHp;
    public int MinHp;
    public float MoveSpeed;
    public float AttackPower;
}

public class Unit : MonoBehaviour, IDamageable
{
    [SerializeField] private UnitStatInfo unitStat;
    public UnitStatInfo UnitStat => unitStat;

    protected int hp;
    public int Hp => hp;

    private void Start()
    {
        ResetHp();
    }

    private void ResetHp()
    {
        hp = UnitStat.MaxHp;
    }

    public virtual void TakeDamage(int damageValue)
    {
        ChangeHp(damageValue);
    }

    public void HealHp(int healValue)
    {
        ChangeHp(healValue);
    }

    private void ChangeHp(int value)
    {
        hp += ClampHp(value);
        if (hp <= UnitStat.MinHp)
        {
            Death();
        }
    }

    private int ClampHp(int value)
    {
        if (hp + value >= UnitStat.MaxHp)
        {
            hp = UnitStat.MaxHp;
        }

        if (hp - value <= UnitStat.MinHp)
        {
            hp = UnitStat.MinHp;
        }
        return value;
    }

    protected virtual void Death()
    {

    }
}