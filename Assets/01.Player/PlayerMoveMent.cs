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
        MoveMent();
    }

    private void MoveMent()
    {
        transform.Translate(player.GetMoveSpeed() * Time.fixedDeltaTime * new Vector2(JoyStick.Instance.GetJoyStickHorizonValue(), 0));
    }
}
