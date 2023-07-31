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
        if (CheckJoyStickMove())
        {
            MoveMent();
            SetFilp();
        }
    }

    private void MoveMent()
    {
        transform.Translate(player.GetMoveSpeed() * Time.fixedDeltaTime * new Vector2(JoyStick.Instance.GetJoyStickMoveHorizonValue(), 0));
    }

    private void SetFilp()
    {
        player.SpriteRenderer.flipX = JoyStick.Instance.GetJoyStickMoveHorizonValue() > 0.01f;
    }

    private bool CheckJoyStickMove()
    {
        return JoyStick.Instance.GetJoyStickMoveHorizonValue() != 0;
    }
}
