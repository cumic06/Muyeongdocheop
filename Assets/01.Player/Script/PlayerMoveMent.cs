using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMoveMent : MonoSingleton<PlayerMoveMent>
{
    #region º¯¼ö 
    public Action<bool> landingAction;

    private readonly float LimitXLowValue = -8.5f;
    private readonly float LimitYLowValue = -2.0f;
    private readonly float LimitXHighValue = 100.0f;
    private readonly float LimitYHighValue = 10.0f;

    private readonly int runAnimation = Animator.StringToHash("IsRun");
    private readonly int landingAnimation = Animator.StringToHash("IsLanding");

    public readonly LayerMask wallLayerMask = 1 << 6;
    public readonly LayerMask floorLayerMask = 1 << 7;

    [SerializeField] private AudioClip runSound;

    [SerializeField] private GameObject ChargingEffect;

    private bool IsCanMove = true;
    #endregion

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        landingAction += LandingAnim;
        UpdateSystem.Instance.AddFixedUpdateAction(MoveManager);
    }

    private void MoveManager()
    {
        if (!GameManager.Instance.CheckGameOver())
        {
            MoveSystem();
        }
    }

    private void MoveSystem()
    {
        bool move = IsMoveInput() && IsCanMove && !BaldoSkillJoyStick.Instance.CheckJoyStickMove();

        Player.Instance.ChangeAnimation(runAnimation, move);

        if (move)
        {
            Player.Instance.ChangeAnimationLayer(1, 0);
            Player.Instance.ChangeAnimation(BaldoSkill.Instance.BalldoAnimation, false);
            MoveMent();
            SetFilp();
        }
    }

    public bool IsMoveInput()
    {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        return Input.GetAxisRaw("Horizontal") != 0;
#else
        return MoveJoyStick.Instance.CheckJoyStickMove();
#endif
    }

    #region Move
    private void MoveMent()
    {
        float moveSpeed = Player.Instance.GetMoveSpeed() * Time.fixedDeltaTime;
        Vector2 direction = GetDirection();
        transform.Translate(moveSpeed * direction);
    }

    private Vector2 GetDirection()
    {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        return new(Input.GetAxisRaw("Horizontal"), 0);
#else
        return new(MoveJoyStick.Instance.GetJoyStickHorizontalValue(), 0);
#endif
    }

    public void MoveSound()
    {
        SoundSystem.Instance.PlayFXSound(runSound, 0.5f);
    }

    public void SetCanMove(bool value)
    {
        IsCanMove = value;
    }
    #endregion

    #region Filp
    public void SetFilp()
    {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        #region PC
        if (BaldoSkillJoyStick.Instance.CheckJoyStickMove())
        {
            Player.Instance.SpriteRenderer.flipX = BaldoSkillJoyStick.Instance.GetJoyStickHorizontalValue() < 0.01f;
        }
        else
        {
            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                Player.Instance.SpriteRenderer.flipX = Input.GetAxisRaw("Horizontal") < 0.01f;
            }
        }
        #endregion
#else
        #region Mobile
        if (BaldoSkillJoyStick.Instance.CheckJoyStickMove())
        {
            Player.Instance.SpriteRenderer.flipX = BaldoSkillJoyStick.Instance.GetJoyStickHorizontalValue() < 0.01f;
        }
        else
        {
            if (MoveJoyStick.Instance.CheckJoyStickMove())
            {
                Player.Instance.SpriteRenderer.flipX = MoveJoyStick.Instance.GetJoyStickHorizontalValue() < 0.01f;
            }
        }
        #endregion
#endif
    }
    #endregion

    public void ReSetMoveMent()
    {
        SetCanMove(true);
        Player.Instance.SetGravityScale(3);
        Player.Instance.ChangeAnimation(landingAnimation, false);
        transform.eulerAngles = Vector3.zero;
        landingAction?.Invoke(false);
        Player.Instance.VelocityReset();
    }

    #region Landing
    private void LandingAnim(bool value)
    {
        Player.Instance.ChangeAnimation(BaldoSkill.Instance.BalldoAnimation, !value);
        Player.Instance.ChangeAnimation(landingAnimation, value);
    }
    #endregion
}