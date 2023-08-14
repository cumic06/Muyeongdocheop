using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BaldoSkill : SkillSystem
{
    #region º¯¼ö
    public static BaldoSkill Instance;
    private Player player;

    private LayerMask monsterLayerMask = 1 << 3;

    private readonly int skillAnimation = Animator.StringToHash("IsAttack");

    private bool IsCharging;
    #endregion

    protected override void Awake()
    {
        base.Awake();
        Instance = GetComponent<BaldoSkill>();
        player = GetComponent<Player>();
    }

    private void FixedUpdate()
    {
#if UNITY_EDITOR
        DrawSkillDirection();
#endif
    }

    public override Vector2 GetSkillStartPos()
    {
        Vector2 startPos = new(transform.position.x * GetSkillDirection().x, transform.position.y);
        return startPos;
    }

    protected override Vector2 GetSkillDirection()
    {
        Vector2 skillDirection = new(BaldoSkillJoyStick.Instance.GetJoyStickHorizontalValue(), BaldoSkillJoyStick.Instance.GetJoyStickVerticalValue());
        skillDirection.Normalize();

        return skillDirection;
    }

    private void DrawSkillDirection()
    {
        Debug.DrawRay(GetSkillStartPos(), GetSkillDirection(), Color.red);
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
    public void Dash(Vector2 dashRange, Vector2 direction)
    {
        if (CheckMonster(out Monster resultMonster))
        {
            PlayerMoveMent.Instance.transform.position = resultMonster.transform.position;
        }
        else if (CheckWall(dashRange, direction, out float wallAngle, out var point))
        {
            StickWall(wallAngle, point);
        }
        else
        {
            Vector2 DashPos = dashRange.x * dashRange.y * direction;
            transform.Translate(DashPos);
        }
    }
    #endregion

    #region Check
    private bool CheckWall(Vector2 dashRange, Vector2 direction, out float wallAngle, out Vector2 point)
    {
        wallAngle = 0;
        point = Vector2.zero;

        RaycastHit2D rayWall = Physics2D.Raycast(GetSkillStartPos(), direction, dashRange.x, PlayerMoveMent.Instance.wallLayerMask);

        if (rayWall)
        {
            Vector2 target = rayWall.normal;

            wallAngle = Vector2.Angle(Vector2.up, target);

            point = rayWall.point;
        }
        else
        {
            player.transform.eulerAngles = Vector2.zero;
            player.SetGravityScale(1);
        }
        return rayWall;
    }

    private bool CheckMonster(out Monster resultMonster)
    {
        resultMonster = null;

        float angle = Mathf.Atan2(BaldoSkillJoyStick.Instance.GetJoyStickVerticalValue(), BaldoSkillJoyStick.Instance.GetJoyStickHorizontalValue()) * Mathf.Rad2Deg;

        RaycastHit2D[] ray = Physics2D.BoxCastAll(GetSkillStartPos(), GetSkillRange(), angle, GetSkillDirection(), 0, monsterLayerMask);

        if (ray.Length > 0)
        {
            List<Monster> monsterList = new();

            foreach (var hit in ray)
            {
                hit.collider.TryGetComponent(out Monster monster);
                monsterList.Add(monster);
                monster.TakeDamage(player.GetAttackPower());
            }

            monsterList = monsterList.OrderByDescending((mob) => { return Mathf.Abs(mob.transform.position.magnitude - transform.position.magnitude); }).ToList();

            resultMonster = monsterList[0];
        }
        return ray.Length > 0;
    }
    #endregion

    private void StickWall(float wallAngle, Vector2 point)
    {
        player.transform.eulerAngles = new(0, 0, wallAngle);
        player.transform.position = point;
        player.SetGravityScale(0);
        PlayerMoveMent.Instance.SetCanMove(false);
        PlayerMoveMent.Instance.landingAction?.Invoke(true);
    }
}
