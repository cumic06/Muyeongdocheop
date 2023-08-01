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
            MoveMent();
            SetFilp();
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
}
