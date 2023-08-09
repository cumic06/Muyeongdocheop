using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveMent : MonoBehaviour
{
    private Player player;

    [SerializeField] private LayerMask wallMask = 1 << 6;

    private readonly float LimitXLowValue = -8.5f;
    private readonly float LimitYLowValue = -2.0f;
    private readonly float LimitXHighValue = 100.0f;
    private readonly float LimitYHighValue = 10.0f;

    private readonly int run = Animator.StringToHash("IsRun");

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void FixedUpdate()
    {
        LimitMove();

        if (MoveJoyStick.Instance.CheckJoyStickMove() && !BaldoSkillJoyStick.Instance.CheckJoyStickMove() && !BaldoSkillJoyStick.Instance.CheckIsClick())
        {
            player.Anim.SetBool(run, true);
            MoveMent();
            SetFilp();
        }
        else
        {
            player.Anim.SetBool(run, false);
        }
    }

    private void MoveMent()
    {
        float moveSpeed = player.GetMoveSpeed() * Time.fixedDeltaTime;
        Vector2 direction = new(MoveJoyStick.Instance.GetJoyStickHorizontalValue(), 0);
        transform.Translate(moveSpeed * direction);
    }

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

    private void LimitMove()
    {
        float LimitX = Mathf.Clamp(transform.position.x, LimitXLowValue, LimitXHighValue);
        float LimitY = Mathf.Clamp(transform.position.y, LimitYLowValue, LimitYHighValue);

        transform.position = new Vector3(LimitX, LimitY);
    }

    #region Dash
    public void Dash(Vector3 dashRange, Vector3 direction)
    {
        BaldoSkillJoyStick.Instance.SetIsJoyStickClick(true);
        SetFilp();

        if (CheckWall(dashRange, direction, out float wallAngle, out Vector2 point))
        {
            StickWall(wallAngle, point);
            return;
        }
        Vector3 DashPos = dashRange.x * dashRange.y * direction;
        transform.Translate(DashPos);

        BaldoSkillJoyStick.Instance.SetIsJoyStickClick(false);
    }

    #endregion

    #region Wall
    private bool CheckWall(Vector3 dashRange, Vector3 direction, out float wallAngle, out Vector2 point)
    {
        wallAngle = 0;
        point = Vector2.zero;
        Debug.DrawRay(transform.position, direction, Color.red, 100);

        RaycastHit2D rayWall = Physics2D.Raycast(BaldoSkill.Instance.GetSkillStartPos(), direction, dashRange.x, wallMask);

        if (rayWall)
        {
            Vector3 target = rayWall.normal;

            wallAngle = Vector3.Angle(Vector3.up, target);
            point = rayWall.point;

            Debug.Log(wallAngle);
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
