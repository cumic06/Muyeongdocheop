using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Internal;

public class Player : Unit
{
    #region º¯¼ö
    private readonly float hitShakeTime = 0.5f;
    private readonly float hitShakePower = 0.15f;

    public static Player Instance;

    public AudioClip DashSound => dashSound;
    [SerializeField] private AudioClip dashSound;
    #endregion

    protected override void Awake()
    {
        base.Awake();
        Instance = GetComponent<Player>();
    }

    protected override void ResetHp()
    {
        unitStat.MaxHp = 2;
        base.ResetHp();
    }

    protected override void ResetSpeed()
    {
        unitStat.MoveSpeed = 7;
    }

    protected override void ResetAttackPower()
    {
        unitStat.AttackPower = 10;
    }

    public override void TakeDamage(int damageValue)
    {
        base.TakeDamage(damageValue);
        UIManager.Instance.HitAction?.Invoke();
        CameraShakeSystem.Instance.CameraShake(hitShakeTime, hitShakePower);
    }

    protected override void ChangeHp(int value)
    {
        base.ChangeHp(value);
        UIManager.Instance.PlayerHpAction?.Invoke(GetMaxHp(), GetHp());
    }

    public void SetGravityScale(float value)
    {
        rigid.gravityScale = value;
    }

    #region Animation
    public void ChangeAnimationLayer(int animationValue, float weight)
    {
        anim.SetLayerWeight(animationValue, weight);
    }

    public void ChangeAnimation(int animationValue, bool value)
    {
        anim.SetBool(animationValue, value);
    }
    #endregion

    protected override void Death()
    {
        base.Death();
        GameManager.Instance.GameOver();
    }

    public void VelocityReset()
    {
        rigid.velocity = Vector2.zero;
    }
}