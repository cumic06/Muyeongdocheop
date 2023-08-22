using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BaldoSkill : SkillSystem
{
    #region º¯¼ö
    public static BaldoSkill Instance;

    [SerializeField] private LayerMask monsterLayerMask = 1 << 3;

    private readonly int balldoAnimation = Animator.StringToHash("IsAttackIdle");
    public int BalldoAnimation => balldoAnimation;

    private bool IsCharging;
    public bool IsDash;

    [SerializeField] private GameObject chargingEffect;
    #endregion

    protected override void Awake()
    {
        base.Awake();
        Instance = GetComponent<BaldoSkill>();
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
        Charging();
    }

    public void Charging()
    {
        chargingEffect.SetActive(CheckCharging());
    }

    public bool CheckCharging()
    {
        return IsCharging;
    }

    #endregion

    protected override void UseSkill()
    {
        if (!GameManager.Instance.CheckGameOver())
        {
            SoundSystem.Instance.PlayFXSound(Player.Instance.DashSound, 0.5f);
            Dash();
        }
    }

    #region Dash
    public void Dash()
    {
        Player.Instance.ChangeAnimationLayer(1, 1);
        Player.Instance.ChangeAnimation(BalldoAnimation, true);

        CameraShakeSystem.Instance.CameraShake(0.25f, 0.15f);


        if (TryAttackMonster(out List<Monster> resultMonster))
        {
            for (int i = 0; i < resultMonster.Count; i++)
            {
                resultMonster[i].TakeDamage(Player.Instance.GetAttackPower());
            }
            PlayerMoveMent.Instance.transform.position = resultMonster[0].transform.position;
        }
        else if (TryWall(out float wallAngle, out Vector2Int normal, out RaycastHit2D wallHit, out Vector2 point))
        {
            if (normal.y == 1)
            {
                transform.position = point;
                PlayerMoveMent.Instance.landingAction?.Invoke(false);
                PlayerMoveMent.Instance.ReSetMoveMent();
            }
            else
            {
                PlayerMoveMent.Instance.landingAction?.Invoke(true);
                Player.Instance.ChangeAnimationLayer(1, 0);
                StickWall(wallAngle, point, false);
            }
        }
        else
        {
            PlayerMoveMent.Instance.ReSetMoveMent();
            if (!TryFloor())
            {
                StartCoroutine(DashCor(GetDashLastPos()));
            }
        }
        Player.Instance.ChangeAnimationLayer(1, 0);
    }

    private IEnumerator DashCor(Vector2 lastPos)
    {
        float t = 0;
        Vector2 startPos = Player.Instance.transform.position;

        while (t <= 1f)
        {
            t += Time.deltaTime * Player.Instance.GetMoveSpeed();
            Player.Instance.transform.position = Vector2.Lerp(startPos, lastPos, t);
            yield return null;
            if (Vector2.Distance(transform.position, lastPos) <= 0.5f)
            {
                break;
            }
        }
        Player.Instance.transform.position = lastPos;
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
                Debug.Log(hit.name);
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

    public bool TryFloor()
    {
        RaycastHit2D floorHit;
        floorHit = Physics2D.Raycast(GetSkillStartPos(), GetSkillDirection(), GetSkillDistance(), PlayerMoveMent.Instance.floorLayerMask);

        return floorHit;
    }
    #endregion

    private void StickWall(float wallAngle, Vector2 point, bool setCanMove)
    {
        Player.Instance.SetGravityScale(0);
        Player.Instance.VelocityReset();
        transform.position = point;
        Player.Instance.transform.eulerAngles = new(0, 0, -wallAngle);
        PlayerMoveMent.Instance.SetCanMove(setCanMove);
    }
}
