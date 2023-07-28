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
    [SerializeField] protected UnitStatInfo unitStat;
    public UnitStatInfo UnitStat => unitStat;

    protected int hp;
    public int Hp => hp;

    protected bool isDead = false;

    protected void Start()
    {
        ReSetStat();
    }

    #region ReSet
    protected void ReSetStat()
    {
        ResetHp();
        ResetSpeed();
    }

    protected virtual void ResetHp()
    {
        hp = UnitStat.MaxHp;
        unitStat.MinHp = 0;
    }

    protected virtual void ResetSpeed()
    {

    }
    #endregion

    #region Hp
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
        if (!isDead)
        {
            hp += ClampHp(value);
            if (Hp <= UnitStat.MinHp)
            {
                isDead = true;
            }
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
    #endregion

    #region GetValue
    public float GetMoveSpeed()
    {
        return unitStat.MoveSpeed;
    }
    #endregion

    protected virtual void Death()
    {
        if (isDead)
        {

        }
    }
}