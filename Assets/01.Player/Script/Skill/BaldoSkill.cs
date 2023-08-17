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

    private readonly int balldoAnimation = Animator.StringToHash("IsAttack");
    private readonly int chargingAnimation = Animator.StringToHash("IsCharging");

    private bool IsCharging;
    public bool IsDash;

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
        Vector2 monsterStartPos = new(transform.position.x + GetSkillHalfDistance() * GetSkillDirection().x, transform.position.y + GetSkillHalfDistance() * GetSkillDirection().y);
        return monsterStartPos;
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

    private Vector2 GetDashLastPos()
    {
        Vector2 lastPos = (Vector2)transform.position + GetSkillRadius();
        return lastPos;
    }
    #endregion

    #region DrawRange
    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        if (Application.isPlaying)
        {
            WallRay();

            MonsterRay();
        }

        void WallRay()
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
        }

        void MonsterRay()
        {
            float angle = Mathf.Atan2(BaldoSkillJoyStick.Instance.GetJoyStickVerticalValue(), BaldoSkillJoyStick.Instance.GetJoyStickHorizontalValue()) * Mathf.Rad2Deg;

            Vector2 skillSize = new(GetSkillDistance(), 1);

            Collider2D monsterHit = Physics2D.OverlapBox(GetSkillStartPosMonster(), skillSize, angle, monsterLayerMask);

            if (monsterHit)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireCube(GetSkillStartPosMonster(), skillSize);
            }
            else
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireCube(GetSkillStartPosMonster(), skillSize);
            }
        }
#endif
    }
    #endregion

    #region Charging
    public void SetCharging(bool charging)
    {
        IsCharging = charging;
    }
    #endregion

    protected override void UseSkill()
    {
        Dash();
    }

    #region Dash
    public void Dash()
    {
        if (TryAttackMonster(out List<Monster> resultMonster))
        {
            for (int i = 0; i < resultMonster.Count; i++)
            {
                resultMonster[i].TakeDamage(player.GetAttackPower());
            }

            PlayerMoveMent.Instance.transform.position = resultMonster[0].transform.position;
            player.ChangeAnimation(balldoAnimation, true);
        }
        else if (TryWall(out float wallAngle, out Vector2Int normal, out RaycastHit2D wallHit, out Vector2 point))
        {
            PlayerMoveMent.Instance.landingAction?.Invoke(true);
            StartCoroutine(DashCor(point));
            if (normal.y == 1)
            {
                transform.position = wallHit.transform.position;
            }
            else
            {
                StickWall(wallAngle, point);
            }
            player.ChangeAnimation(balldoAnimation, false);
        }
        else
        {
            StartCoroutine(DashCor(GetDashLastPos()));
            player.ChangeAnimation(balldoAnimation, false);
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
    public bool TryAttackMonster(out List<Monster> resultMonster)
    {
        resultMonster = null;

        float angle = Mathf.Atan2(BaldoSkillJoyStick.Instance.GetJoyStickVerticalValue(), BaldoSkillJoyStick.Instance.GetJoyStickHorizontalValue()) * Mathf.Rad2Deg;

        Vector2 skillSize = new(GetSkillDistance(), 1);

        Collider2D[] monsterHit = Physics2D.OverlapBoxAll(GetSkillStartPosMonster(), skillSize, angle, monsterLayerMask);

        if (monsterHit.Length > 0)
        {
            List<Monster> monsterList = new();

            foreach (var hit in monsterHit)
            {
                hit.TryGetComponent(out Monster monster);
                monsterList.Add(monster);
                UIManager.Instance.RayUIAction?.Invoke(monster.transform.position);
            }

            monsterList = monsterList.OrderByDescending((mob) => { return Mathf.Abs(mob.transform.position.magnitude - transform.position.magnitude); }).ToList();

            resultMonster = monsterList;
        }
        return monsterHit.Length > 0;
    }

    public bool TryWall(out float wallAngle, out Vector2Int normalInt, out RaycastHit2D wallHit, out Vector2 point)
    {
        wallAngle = 0;
        point = Vector2.zero;
        normalInt = Vector2Int.zero;

        wallHit = Physics2D.Raycast(GetSkillStartPos(), GetSkillDirection(), GetSkillDistance(), PlayerMoveMent.Instance.wallLayerMask);

        if (wallHit)
        {
            normalInt = new Vector2Int((int)wallHit.normal.x, (int)wallHit.normal.y);
            Debug.Log(normalInt);
            wallAngle = WallAngle(normalInt);

            point = wallHit.point;
        }
        return wallHit;

        static float WallAngle(Vector2Int normal)
        {
            float wallAngle;

            if (normal.x != 0)
            {
                wallAngle = Vector2.Angle(Vector2.up, normal) * normal.x;
            }
            else
            {
                wallAngle = Vector2.Angle(Vector2.up, normal) * normal.y;
            }
            return wallAngle;
        }
    }
    #endregion

    private void StickWall(float wallAngle, Vector2 point)
    {
        transform.position = point;
        player.Rigid.drag = 1;
        player.transform.eulerAngles = new(0, 0, -wallAngle);
        PlayerMoveMent.Instance.SetCanMove(false);
    }
}
