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

    public IEnumerator Dash(float horizonValue, float verticalValue)
    {
        Debug.Log("Horizonvalue" + horizonValue);
        Debug.Log("VerticalValue" + verticalValue);
        while (true)
        {
            transform.Translate(player.GetMoveSpeed() * Time.deltaTime * new Vector2(horizonValue, verticalValue));
            yield return null;
        }
    }
}
