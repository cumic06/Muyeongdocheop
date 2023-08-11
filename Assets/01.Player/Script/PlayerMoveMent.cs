using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveMent : MonoBehaviour
{
    #region º¯¼ö 
    private Player player;

    private LayerMask wallMask = 1 << 6;

    private readonly float LimitXLowValue = -8.5f;
    private readonly float LimitYLowValue = -2.0f;
    private readonly float LimitXHighValue = 100.0f;
    private readonly float LimitYHighValue = 10.0f;

    private readonly int runAnimation = Animator.StringToHash("IsRun");

    [SerializeField] private AudioClip runSound;
    #endregion

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void FixedUpdate()
    {
        LimitMove();
        CheckDown();

        if (MoveJoyStick.Instance.CheckJoyStickMove() && !BaldoSkillJoyStick.Instance.CheckJoyStickMove() && !BaldoSkillJoyStick.Instance.CheckIsClick())
        {
            MoveMent();
            SetFilp();
            player.ChangeAnimation(runAnimation, true);
        }
        else
        {
            player.ChangeAnimation(runAnimation, false);
        }
    }

    #region Move
    private void MoveMent()
    {
        float moveSpeed = player.GetMoveSpeed() * Time.fixedDeltaTime;
        Vector2 direction = new(MoveJoyStick.Instance.GetJoyStickHorizontalValue(), 0);
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
    #endregion

    #region Filp
    private void SetFilp()
    {
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
    }
    #endregion

    #region Dash
    public void Dash(Vector3 dashRange, Vector3 direction)
    {
        BaldoSkillJoyStick.Instance.SetIsJoyStickClick(true);
        SetFilp();

        if (CheckWall(dashRange, direction, out float wallAngle, out var point))
        {
            StickWall(wallAngle, point);
        }
        else
        {
            Vector3 DashPos = dashRange.x * dashRange.y * direction;
            transform.Translate(DashPos);
        }
        BaldoSkillJoyStick.Instance.SetIsJoyStickClick(false);
    }
    #endregion

    #region CheckRay
    private void CheckDown()
    {
        RaycastHit2D rayDown = Physics2D.Raycast(transform.position, -transform.up, 1.5f, wallMask);
        if (!rayDown)
        {
            player.SetGravityScale(1);
            transform.eulerAngles = Vector3.zero;
        }
        else
        {
            player.SetGravityScale(0);
        }
    }

    private bool CheckWall(Vector3 dashRange, Vector3 direction, out float wallAngle, out Vector2 point)
    {
        wallAngle = 0;
        point = Vector2.zero;

        RaycastHit2D rayWall = Physics2D.Raycast(transform.position, direction, dashRange.x, wallMask);

        if (rayWall)
        {
            Vector2 target = rayWall.normal;

            wallAngle = Vector2.Angle(Vector2.up, target);

            point = rayWall.point;
        }
        else
        {
            player.transform.eulerAngles = Vector3.zero;
            player.SetGravityScale(1);
        }
        return rayWall;
    }

    private void StickWall(float wallAngle, Vector2 point)
    {
        player.transform.eulerAngles = new(0, 0, wallAngle);
        player.transform.position = point;
        player.SetGravityScale(0);
    }
    #endregion
}
