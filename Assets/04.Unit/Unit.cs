using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct UnitStatInfo
{
    public int MaxHp;
    public int MinHp;
    public float MoveSpeed;
    public int AttackPower;
}

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public abstract class Unit : MonoBehaviour, IDamageable
{
    #region º¯¼ö
    [SerializeField] protected UnitStatInfo unitStat;
    public UnitStatInfo UnitStat => unitStat;

    [Space]
    [SerializeField] protected GameObject attackEffect;
    [SerializeField] protected GameObject dashEffect;

    protected int hp;
    public int Hp => hp;

    protected bool isDead = false;

    protected SpriteRenderer spriteRenderer;
    public SpriteRenderer SpriteRenderer => spriteRenderer;

    protected Animator anim;
    public Animator Anim => anim;

    protected Rigidbody2D rigid;
    public Rigidbody2D Rigid => rigid;
    #endregion

    protected void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }

    protected void Start()
    {
        ReSetStat();
    }

    #region ReSet
    protected void ReSetStat()
    {
        ResetHp();
        ResetSpeed();
        ResetAttackPower();
    }

    protected virtual void ResetHp()
    {
        hp = UnitStat.MaxHp;
        unitStat.MinHp = 0;
    }

    protected abstract void ResetSpeed();

    protected abstract void ResetAttackPower();
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
            ClampHp(ref value);
            hp = value;
            if (Hp <= UnitStat.MinHp)
            {
                Death();
            }
        }
    }

    protected void ClampHp(ref int value)
    {
        if (Hp + value >= UnitStat.MaxHp)
        {
            hp = UnitStat.MaxHp;
        }

        if (Hp + value <= UnitStat.MinHp)
        {
            hp = UnitStat.MinHp;
        }
    }
    #endregion

    #region GetValue
    public float GetMoveSpeed()
    {
        return unitStat.MoveSpeed;
    }

    public int GetHp()
    {
        return hp;
    }

    public int GetMaxHp()
    {
        return unitStat.MaxHp;
    }

    public int GetAttackPower()
    {
        return unitStat.AttackPower;
    }
    #endregion

    protected virtual void Death()
    {
        Debug.Log("Dead");
    }
}