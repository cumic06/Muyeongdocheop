using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BaldoSkill : SkillSystem
{
    #region º¯¼ö
    public static BaldoSkill Instance;
    private Player player;

    private LayerMask monsterLayerMask = 1 << 3;

    private readonly int skillAnimation = Animator.StringToHash("IsAttack");

    private bool IsCharging;

    [SerializeField] private ParticleSystem afterImage;
    #endregion

    protected override void Awake()
    {
        base.Awake();
        Instance = GetComponent<BaldoSkill>();
        player = GetComponent<Player>();
    }

    #region Get
    public override Vector2 GetSkillStartPos()
    {
        Vector2 startPos = new(transform.position.x, transform.position.y);
        return startPos;
    }

    public Vector2 GetSkillStartPosMonster()
    {
        Vector2 startPos = GetSkillStartPos() + new Vector2(GetSkillHalfDistance(), 0) * GetSkillDirection();
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
        Vector2 skillRadius = GetSkillDistance() * GetSkillDirection();
        return skillRadius;
    }
    #endregion

    #region DrawRange
    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        if (Application.isPlaying)
        {
            RaycastHit2D wallHit = Physics2D.Raycast(GetSkillStartPos(), GetSkillDirection(), GetSkillDistance(), PlayerMoveMent.Instance.wallLayerMask);

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

            //float angle = Mathf.Atan2(BaldoSkillJoyStick.Instance.GetJoyStickVerticalValue(), BaldoSkillJoyStick.Instance.GetJoyStickHorizontalValue()) * Mathf.Rad2Deg;

            //Vector2 skillSize = new(GetSkillDistance(), 1);

            //RaycastHit2D monsterHit = Physics2D.BoxCast(GetSkillStartPosMonster(), skillSize, angle, GetSkillDirection(), GetSkillDistance(), monsterLayerMask);
            //if (monsterHit)
            //{
            //    Debug.Log(monsterHit.collider.name);
            //    Gizmos.color = Color.red;
            //    Gizmos.DrawRay(GetSkillStartPosMonster(), GetSkillRadius());
            //}
            //else
            //{
            //    Gizmos.color = Color.green;
            //    Gizmos.DrawRay(GetSkillStartPosMonster(), GetSkillRadius());
            //}
        }
#endif
    }
    #endregion

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
        Dash();
    }

    #region Dash
    public void Dash()
    {
        if (CheckMonster(out Monster resultMonster))
        {
            PlayerMoveMent.Instance.transform.position = resultMonster.transform.position;
        }
        else if (CheckWall(out float wallAngle, out var point))
        {
            StartCoroutine(DashCor(point));
            StickWall(wallAngle, point);
        }
        else
        {
            Vector2 lastPos = (Vector2)transform.position + GetSkillRadius();
            StartCoroutine(DashCor(lastPos));
        }

    }

    private IEnumerator DashCor(Vector2 lastPos)
    {
        float t = 0;
        Vector2 startPos = player.transform.position;

        while (t <= 1f)
        { 
            t += Time.deltaTime * player.GetMoveSpeed();
            player.transform.position = Vector2.Lerp(startPos, lastPos, t);
            Debug.Log(Vector2.Distance(transform.position, lastPos));
            yield return null;
            if (Vector2.Distance(transform.position, lastPos) <= 0.5f)
            {
                break;
            }
        }
        player.transform.position = lastPos;
        yield return null;
    }
    #endregion

    #region Check
    private bool CheckWall(out float wallAngle, out Vector2 point)
    {
        wallAngle = 0;
        point = Vector2.zero;

        RaycastHit2D wallHit = Physics2D.Raycast(GetSkillStartPos(), GetSkillDirection(), GetSkillDistance(), PlayerMoveMent.Instance.wallLayerMask);

        if (wallHit)
        {
            PlayerMoveMent.Instance.landingAction?.Invoke(true);
            Vector2 target = wallHit.normal;

            if (target.x != 0)
            {
                wallAngle = Vector2.Angle(Vector2.up, target) * target.x;
            }
            else
            {
                wallAngle = Vector2.Angle(Vector2.up, target) * target.y;
            }
            point = wallHit.point;
        }
        return wallHit;
    }

    private bool CheckMonster(out Monster resultMonster)
    {
        resultMonster = null;

        float angle = Mathf.Atan2(BaldoSkillJoyStick.Instance.GetJoyStickVerticalValue(), BaldoSkillJoyStick.Instance.GetJoyStickHorizontalValue()) * Mathf.Rad2Deg;

        Vector2 skillSize = new(GetSkillDistance(), 1);

        RaycastHit2D[] monsterHit = Physics2D.BoxCastAll(GetSkillStartPosMonster(), skillSize, angle, GetSkillDirection(), GetSkillDistance(), monsterLayerMask);

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
        player.transform.eulerAngles = new(0, 0, -wallAngle);
        transform.position = point;
        PlayerMoveMent.Instance.SetCanMove(false);
    }
}
