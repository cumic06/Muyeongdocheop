using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveMent : MonoBehaviour
{
    private Player player;

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

        if (MoveJoyStick.Instance.CheckJoyStickMove())
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
        Vector2 direction = new(MoveJoyStick.Instance.GetJoyStickHorizonValue(), 0);
        transform.Translate(moveSpeed * direction);
    }

    private void SetFilp()
    {
        if (AtypeSkillJoyStick.Instance.CheckJoyStickMove())
        {
            player.SpriteRenderer.flipX = AtypeSkillJoyStick.Instance.GetJoyStickHorizonValue() < 0.01f;
        }
        else
        {
            if (MoveJoyStick.Instance.CheckJoyStickMove())
            {
                player.SpriteRenderer.flipX = MoveJoyStick.Instance.GetJoyStickHorizonValue() < 0.01f;
            }
        }
    }

    private void LimitMove()
    {
        float LimitX = Mathf.Clamp(transform.position.x, LimitXLowValue, LimitXHighValue);
        float LimitY = Mathf.Clamp(transform.position.y, LimitYLowValue, LimitYHighValue);

        transform.position = new Vector3(LimitX, LimitY);
    }

    public void Dash(float dashPower, float horizonValue, float verticalValue)
    {
        SetFilp();
        float DashPower = dashPower * player.GetMoveSpeed() * Time.fixedDeltaTime;
        Vector2 direction = new(horizonValue, verticalValue);
        transform.Translate(DashPower * direction);
    }
}
