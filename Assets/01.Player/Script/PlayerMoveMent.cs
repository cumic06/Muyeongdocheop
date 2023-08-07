using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveMent : MonoBehaviour
{
    private Player player;

    private readonly int run = Animator.StringToHash("IsRun");

    private readonly float LimitXLowValue = -8.5f;
    private readonly float LimitYLowValue = -2.0f;

    private readonly float LimitXHighValue = 100.0f;
    private readonly float LimitYHighValue = 10.0f;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void FixedUpdate()
    {
        LimitMove();

        player.Anim.SetBool(run, MoveJoyStick.Instance.CheckJoyStickMove());

        if (MoveJoyStick.Instance.CheckJoyStickMove())
        {
            SetFilp();
            MoveMent();
            Debug.Log("Move");
        }
    }

    private void MoveMent()
    {
        transform.Translate(player.GetMoveSpeed() * Time.fixedDeltaTime * new Vector2(MoveJoyStick.Instance.GetJoyStickHorizonValue(), 0));
    }

    private void SetFilp()
    {
        Debug.Log(AtypeSkillJoyStick.Instance.GetJoyStickHorizonValue());
        if (AtypeSkillJoyStick.Instance.CheckJoyStickMove())
        {
            player.SpriteRenderer.flipX = AtypeSkillJoyStick.Instance.GetJoyStickHorizonValue() < 0.01f;
        }
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
        else
        {
            if (MoveJoyStick.Instance.CheckJoyStickMove())
            {
                player.SpriteRenderer.flipX = MoveJoyStick.Instance.GetJoyStickHorizonValue() < 0.01f;
            }
=======
=======
>>>>>>> parent of 8f68c1d (Dash MoveWay & Background Settings & Change And Assets)
=======
>>>>>>> parent of 8f68c1d (Dash MoveWay & Background Settings & Change And Assets)

        if (MoveJoyStick.Instance.CheckJoyStickMove())
        {
            player.SpriteRenderer.flipX = MoveJoyStick.Instance.GetJoyStickHorizonValue() < 0.01f;
<<<<<<< HEAD
<<<<<<< HEAD
>>>>>>> parent of 8f68c1d (Dash MoveWay & Background Settings & Change And Assets)
=======
>>>>>>> parent of 8f68c1d (Dash MoveWay & Background Settings & Change And Assets)
=======
>>>>>>> parent of 8f68c1d (Dash MoveWay & Background Settings & Change And Assets)
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
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
        Vector3 direction = new Vector3(horizonValue, verticalValue).normalized;

        transform.Translate(DashPower * direction);
=======
        player.Rigid.AddForce(DashPower * new Vector2(horizonValue, verticalValue), ForceMode2D.Impulse);
>>>>>>> parent of 8f68c1d (Dash MoveWay & Background Settings & Change And Assets)
=======
        player.Rigid.AddForce(DashPower * new Vector2(horizonValue, verticalValue), ForceMode2D.Impulse);
>>>>>>> parent of 8f68c1d (Dash MoveWay & Background Settings & Change And Assets)
=======
        player.Rigid.AddForce(DashPower * new Vector2(horizonValue, verticalValue), ForceMode2D.Impulse);
>>>>>>> parent of 8f68c1d (Dash MoveWay & Background Settings & Change And Assets)
    }
}
