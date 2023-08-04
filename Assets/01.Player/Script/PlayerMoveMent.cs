using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveMent : MonoBehaviour
{
    private Player player;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void FixedUpdate()
    {
        if (MoveJoyStick.Instance.CheckJoyStickMove())
        {
            player.Anim.SetBool("IsRun", true);
            MoveMent();
            SetFilp();
        }
        else
        {
            player.Anim.SetBool("IsRun", false);
        }
    }

    private void MoveMent()
    {
        transform.Translate(player.GetMoveSpeed() * Time.fixedDeltaTime * new Vector2(MoveJoyStick.Instance.GetJoyStickHorizonValue(), 0));
    }

    private void SetFilp()
    {
        player.SpriteRenderer.flipX = MoveJoyStick.Instance.GetJoyStickHorizonValue() < 0.01f;
    }

    public void Dash()
    {
        float horizon = AtypeSkillJoyStick.Instance.GetJoyStickHorizonValue();
        float vertical = AtypeSkillJoyStick.Instance.GetJoyStickVerticalValue();
        transform.Translate(new Vector2(horizon, vertical) * player.GetMoveSpeed() * Time.deltaTime);
    }

}
