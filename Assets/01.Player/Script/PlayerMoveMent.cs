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

    public LayerMask wallLayerMask = 1 << 6;

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
        RayDown();

        MoveSystem();
    }

    private void MoveSystem()
    {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        #region PC
        if (Input.GetAxisRaw("Horizontal") != 0 && IsCanMove && !BaldoSkillJoyStick.Instance.CheckJoyStickMove())
        {
            MoveMent();
            player.ChangeAnimation(runAnimation, true);
            SetFilp();
        }
        else
        {
            player.ChangeAnimation(runAnimation, false);
        }
        #endregion
#else
        #region Mobile
        if (MoveJoyStick.Instance.CheckJoyStickMove() && !BaldoSkillJoyStick.Instance.CheckJoyStickMove() && IsCanMove)
        {
            MoveMent();
            player.ChangeAnimation(runAnimation, true);
            SetFilp();
        }
        else
        {
            player.ChangeAnimation(runAnimation, false);
        }
        #endregion
#endif
    }

    #region Move
    private void MoveMent()
    {
        float moveSpeed = player.GetMoveSpeed() * Time.fixedDeltaTime;

#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        Vector2 direction = new(Input.GetAxisRaw("Horizontal"), 0);
#else
        Vector2 direction = new(MoveJoyStick.Instance.GetJoyStickHorizontalValue(), 0);
#endif
        transform.Translate(moveSpeed * direction);
    }

    public void MoveSound()
    {
        SoundSystem.Instance.PlaySound(runSound);
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

    private RaycastHit2D GetRayDown()
    {
        RaycastHit2D rayDown = Physics2D.Raycast(transform.position, -transform.up, 1.5f, wallLayerMask);
        return rayDown;
    }

    public bool CheckRayDown()
    {
        return GetRayDown();
    }

    private void RayDown()
    {
        if (GetRayDown())
        {
            player.SetGravityScale(0);
            player.SetDrag(0, 0);
            Debug.Log("DownWall");
        }
        else
        {
            Debug.Log("DownFloor");
            SetCanMove(true);
            player.SetGravityScale(1);
            player.SetDrag(1, 0.05f);
            player.ChangeAnimation(landingAnimation, false);
            transform.eulerAngles = Vector3.zero;
            landingAction?.Invoke(false);
        }
    }
    #endregion

    #region Landing
    private void LandingAnim(bool value)
    {
        player.ChangeAnimation(landingAnimation, value);
    }
    #endregion
}