using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaldoSkill : SkillSystem
{
    public static BaldoSkill Instance;
    private PlayerMoveMent playerMoveMent;
    private Player player;

    private LayerMask monsterLayerMask = 1 << 3;

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

    protected override Vector3 GetSkillStartPos()
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

    protected override void UseSkill()
    {
        playerMoveMent.Dash(GetSkillRange(), GetSkillDirection());

        RaycastHit2D[] ray = Physics2D.BoxCastAll(GetSkillStartPos(), GetSkillRange(), 0, GetSkillDirection(), GetSkillRange().x, monsterLayerMask);


        foreach (var hit in ray)
        {
            Debug.Log(hit.collider.name);
            hit.collider.TryGetComponent(out Monster monster);
            monster.TakeDamage(player.GetAttackPower());
        }
    }

}
