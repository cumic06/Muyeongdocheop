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

    public override Vector2 GetSkillStartPos()
    {
        Vector2 startPos = new(transform.position.x, transform.position.y);
        return startPos;
    }

    protected override Vector2 GetSkillDirection()
    {
        Vector2 skillDirection = new(BaldoSkillJoyStick.Instance.GetJoyStickHorizontalValue(), BaldoSkillJoyStick.Instance.GetJoyStickVerticalValue());
        skillDirection.Normalize();

        return skillDirection;
    }

    private Vector2 GetSkillRadius()
    {
        return GetSkillDirection() * GetSkillRange().x;
    }

    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        if (Application.isPlaying)
        {
            RaycastHit2D wallHit = Physics2D.Raycast(GetSkillStartPos(), GetSkillDirection(), (int)GetSkillRange().x, PlayerMoveMent.Instance.wallLayerMask);
            if (wallHit)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawRay(GetSkillStartPos(), GetSkillRadius());
            }
            else
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawRay(GetSkillStartPos(), GetSkillRadius());
            }

            float angle = Mathf.Atan2(BaldoSkillJoyStick.Instance.GetJoyStickVerticalValue(), BaldoSkillJoyStick.Instance.GetJoyStickHorizontalValue()) * Mathf.Rad2Deg;
            RaycastHit2D monsterHit = Physics2D.BoxCast(GetSkillStartPos(), GetSkillRange(), angle, GetSkillDirection(), GetSkillHalfHorizontalRange(), monsterLayerMask);
            if (monsterHit)
            {
                Debug.Log(monsterHit.collider.name);
                Gizmos.color = Color.red;
                Gizmos.DrawRay(GetSkillStartPos(), GetSkillRadius());
            }
            else
            {
                Gizmos.color = Color.green;
                Gizmos.DrawRay(GetSkillStartPos(), GetSkillRadius());
            }
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
        else if (CheckWall(out float wallAngle, out var point))
        {
            StickWall(wallAngle, point);
        }
        else
        {
            StartCoroutine(DashCor());
        }

        IEnumerator DashCor()
        {
            player.Rigid.AddForce(GetSkillRadius(), ForceMode2D.Impulse);
            yield return null;
            while (true)
            {
                if ((Vector2)transform.position == GetSkillRadius())
                {
                    transform.position = GetSkillRadius();
                    yield return null;
                    break;
                }
                yield return null;
            }
        }
    }
    #endregion

    #region Check
    private bool CheckWall(out float wallAngle, out Vector2 point)
    {
        wallAngle = 0;
        point = Vector2.zero;

        RaycastHit2D wallHit = Physics2D.Raycast(GetSkillStartPos(), GetSkillDirection(), (int)GetSkillRange().x, PlayerMoveMent.Instance.wallLayerMask);

        if (wallHit)
        {
            Vector2 target = wallHit.normal;

            wallAngle = Vector2.Angle(Vector2.up, target);

            point = wallHit.point;
        }
        else
        {
            player.transform.eulerAngles = Vector2.zero;
            player.SetGravityScale(1);
        }
        return wallHit;
    }

    private bool CheckMonster(out Monster resultMonster)
    {
        resultMonster = null;

        float angle = Mathf.Atan2(BaldoSkillJoyStick.Instance.GetJoyStickVerticalValue(), BaldoSkillJoyStick.Instance.GetJoyStickHorizontalValue()) * Mathf.Rad2Deg;

        RaycastHit2D[] monsterHit = Physics2D.BoxCastAll(GetSkillStartPos(), GetSkillRange(), angle, GetSkillDirection(), GetSkillHalfHorizontalRange(), monsterLayerMask);

        if (monsterHit.Length > 0)
        {
            List<Monster> monsterList = new();

            foreach (var hit in monsterHit)
            {
                hit.collider.TryGetComponent(out Monster monster);
                monsterList.Add(monster);
                monster.TakeDamage(player.GetAttackPower());
            }

            monsterList = monsterList.OrderByDescending((mob) => { return Mathf.Abs(mob.transform.position.magnitude - transform.position.magnitude); }).ToList();

            resultMonster = monsterList[0];
        }
        return monsterHit.Length > 0;
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
