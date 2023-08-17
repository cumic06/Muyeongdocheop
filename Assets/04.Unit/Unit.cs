using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
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
    [SerializeField] protected GameObject hitEffect;
    [SerializeField] protected GameObject dashEffect;

    [Space]
    [SerializeField] private AudioClip hitSound;

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

    protected virtual void Start()
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
        hp = unitStat.MaxHp;
        unitStat.MinHp = 0;
    }

    protected abstract void ResetSpeed();

    protected abstract void ResetAttackPower();
    #endregion

    #region Hp
    public virtual void TakeDamage(int damageValue)
    {
        ChangeHp(-damageValue);
        HitEffect();
        HitSound();

        void HitEffect()
        {
            GameObject hitEffectSpawn = Instantiate(hitEffect);
            Vector3 randomPos = transform.position + (Vector3)Random.insideUnitCircle * 0.5f;
            hitEffectSpawn.transform.position = randomPos;
        }

        void HitSound()
        {
            SoundSystem.Instance.PlaySound(hitSound);
        }
    }

    public virtual void HealHp(int healValue)
    {
        ChangeHp(healValue);
    }

    protected virtual void ChangeHp(int value)
    {
        if (!isDead)
        {
            ClampHp(ref value);
            hp += value;
            if (Hp <= UnitStat.MinHp)
            {
                Death();
                isDead = true;
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
        CameraShakeSystem.Instance.CameraShake(0.5f, 0.2f);
    }
}