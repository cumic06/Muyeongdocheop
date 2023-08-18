using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMoveMent : Singleton<PlayerMoveMent>
{
    #region º¯¼ö 
    private Player player;

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
        player = GetComponent<Player>();
    }

    private void Start()
    {
        landingAction += LandingAnim;
        UpdateSystem.Instance.AddFixedUpdateAction(MoveManager);
    }

    private void MoveManager()
    {
        LimitMove();
        RayWallDown();

        MoveSystem();
    }

    private void MoveSystem()
    {
        bool move = IsMoveInput() && IsCanMove && !BaldoSkillJoyStick.Instance.CheckJoyStickMove();

        player.ChangeAnimation(runAnimation, move);
        
        if (move)
        {
            MoveMent();
            SetFilp();
        }
    }

    private bool IsMoveInput()
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
        float moveSpeed = player.GetMoveSpeed() * Time.fixedDeltaTime;
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

    private void LimitMove()
    {
        float LimitX = Mathf.Clamp(transform.position.x, LimitXLowValue, LimitXHighValue);
        float LimitY = Mathf.Clamp(transform.position.y, LimitYLowValue, LimitYHighValue);

        transform.position = new Vector3(LimitX, LimitY);
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
            player.SpriteRenderer.flipX = BaldoSkillJoyStick.Instance.GetJoyStickHorizontalValue() < 0.01f;
        }
        else
        {
            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                player.SpriteRenderer.flipX = Input.GetAxisRaw("Horizontal") < 0.01f;
            }
        }
        #endregion
#else
        #region Mobile
        if (BaldoSkillJoyStick.Instance.CheckJoyStickMove())
        {
            player.SpriteRenderer.flipX = BaldoSkillJoyStick.Instance.GetJoyStickHorizontalValue() < 0.01f;
        }
        else
        {
            if (MoveJoyStick.Instance.CheckJoyStickMove())
            {
                player.SpriteRenderer.flipX = MoveJoyStick.Instance.GetJoyStickHorizontalValue() < 0.01f;
            }
        }
        #endregion
#endif
    }
    #endregion

    #region CheckRay
    private void OnDrawGizmos()
    {
        RaycastHit2D rayDown = Physics2D.Raycast(transform.position, -transform.up, 2f, wallLayerMask);

        if (rayDown)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, -transform.up * 2f);
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, -transform.up * 2f);
        }
    }

    #region WallCheck
    private RaycastHit2D GetRayWallDown()
    {
        RaycastHit2D rayWallDown = Physics2D.Raycast(transform.position, -transform.up, 1.5f, wallLayerMask);
        return rayWallDown;
    }

    public bool CheckRayWallDown()
    {
        return GetRayWallDown();
    }

    private void RayWallDown()
    {
        if (GetRayWallDown())
        {
            player.SetGravityScale(0);
            player.VelocityReset();
        }
        else
        {
            SetCanMove(true);
            player.SetGravityScale(3);
            player.ChangeAnimation(landingAnimation, false);
            transform.eulerAngles = Vector3.zero;
            landingAction?.Invoke(false);
        }
    }

    #endregion
    #endregion

    #region Landing
    private void LandingAnim(bool value)
    {
        player.ChangeAnimation(landingAnimation, value);
    }
    #endregion
}