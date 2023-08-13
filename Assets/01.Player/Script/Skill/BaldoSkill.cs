using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BaldoSkill : SkillSystem
{
    #region º¯¼ö
    public static BaldoSkill Instance;
    private PlayerMoveMent playerMoveMent;
    private Player player;

    private LayerMask monsterLayerMask = 1 << 3;

    private readonly int skillAnimation = Animator.StringToHash("IsAttack");

    private bool IsCharging;
    #endregion

    protected override void Awake()
    {
        base.Awake();
        playerMoveMent = GetComponent<PlayerMoveMent>();
        Instance = GetComponent<BaldoSkill>();
        player = GetComponent<Player>();
    }

    private void FixedUpdate()
    {
#if UNITY_EDITOR
        DrawSkillDirection();
#endif
    }

    public override Vector3 GetSkillStartPos()
    {
        Vector3 skillStartPos = new(transform.position.x + (GetSkillHalfHorizontalRange() * BaldoSkillJoyStick.Instance.GetJoyStickHorizontalValue()), transform.position.y);
        return skillStartPos;
    }

    protected override Vector3 GetSkillDirection()
    {
        Vector3 skillDirection = new(BaldoSkillJoyStick.Instance.GetJoyStickHorizontalValue(), GetSkillRange().y * BaldoSkillJoyStick.Instance.GetJoyStickVerticalValue());
        skillDirection.Normalize();

        return skillDirection;
    }

    private void DrawSkillDirection()
    {
        Debug.DrawRay(transform.position, GetSkillDirection(), Color.red);
    }

    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        if (Application.isPlaying)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(GetSkillStartPos(), GetSkillRange());
        }
#endif
    }

    #region Charging
    public void SetCharging(bool charging)
    {
        IsCharging = charging;
    }

    public bool CheckCharging()
    {
        return IsCharging;
    }
    #endregion

    protected override void UseSkill()
    {
        Dash(GetSkillRange(), GetSkillDirection());
    }

    #region Dash
    public void Dash(Vector3 dashRange, Vector3 direction)
    {
        if (CheckMonster(out Monster resultMonster))
        {
            playerMoveMent.transform.position = resultMonster.transform.position;
            return;
        }
        else if (CheckWall(dashRange, direction, out float wallAngle, out var point))
        {
            StickWall(wallAngle, point);
        }
        else
        {
            Vector3 DashPos = dashRange.x * dashRange.y * direction;
            transform.Translate(DashPos);
        }
    }
    #endregion

    #region Check
    private bool CheckWall(Vector3 dashRange, Vector3 direction, out float wallAngle, out Vector2 point)
    {
        wallAngle = 0;
        point = Vector2.zero;

        RaycastHit2D rayWall = Physics2D.Raycast(transform.position + new Vector3(BaldoSkillJoyStick.Instance.GetJoyStickHorizontalValue(), BaldoSkillJoyStick.Instance.GetJoyStickVerticalValue()), direction, dashRange.x, playerMoveMent.wallLayerMask);

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

    private bool CheckMonster(out Monster resultMonster)
    {
        float angle = Mathf.Atan2(BaldoSkillJoyStick.Instance.GetJoyStickVerticalValue(), BaldoSkillJoyStick.Instance.GetJoyStickHorizontalValue()) * Mathf.Rad2Deg;

        RaycastHit2D[] ray = Physics2D.BoxCastAll(GetSkillStartPos(), GetSkillRange(), angle, GetSkillDirection(), 0, monsterLayerMask);

        bool checkMonster = false;
        resultMonster = null;

        List<Monster> monsterList = new();

        foreach (var hit in ray)
        {
            checkMonster = hit.collider.TryGetComponent(out Monster monster);
            monsterList.Add(monster);
            monster.TakeDamage(player.GetAttackPower());
        }

        if (checkMonster)
        {
            resultMonster = monsterList[^1];
            Debug.Log(monsterList[^1].name);
        }
        return checkMonster;
    }
    #endregion

    private void StickWall(float wallAngle, Vector2 point)
    {
        player.transform.eulerAngles = new(0, 0, wallAngle);
        player.transform.position = point;
        player.SetGravityScale(0);
        playerMoveMent.SetCanMove(false);
    }
}
