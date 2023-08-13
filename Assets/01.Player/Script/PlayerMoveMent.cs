using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveMent : MonoBehaviour
{
    #region º¯¼ö 
    private Player player;

    private readonly float LimitXLowValue = -8.5f;
    private readonly float LimitYLowValue = -2.0f;
    private readonly float LimitXHighValue = 100.0f;
    private readonly float LimitYHighValue = 10.0f;

    private readonly int runAnimation = Animator.StringToHash("IsRun");

    public LayerMask wallLayerMask = 1 << 6;

    [SerializeField] private AudioClip runSound;

    [SerializeField] private GameObject ChargingEffect;

    private bool IsCanMove = true;
    #endregion

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void FixedUpdate()
    {
        LimitMove();
        CheckDown();

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

    public void SetCanMove(bool value)
    {
        IsCanMove = value;
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

    #region CheckRay
    private void CheckDown()
    {
        RaycastHit2D rayDown = Physics2D.Raycast(transform.position, -transform.up, 1.5f, wallLayerMask);
        if (!rayDown)
        {
            SetCanMove(true);
            player.SetGravityScale(1);
            transform.eulerAngles = Vector3.zero;
        }
        else
        {
            player.SetGravityScale(0);
        }
    }
    #endregion
}
