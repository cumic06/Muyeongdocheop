using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATypeSkill : SkillSystem
{
    private const float dashPower = 25f;
    public static ATypeSkill Instance;
    protected PlayerMoveMent playerMoveMent;

    protected override void Awake()
    {
        base.Awake();
        playerMoveMent = GetComponent<PlayerMoveMent>();
        Instance = GetComponent<ATypeSkill>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + new Vector3(ReturnSkillHalfRange(), 0, 0), skillRange[0]);
    }

    protected override void UseSkill()
    {
        playerMoveMent.Dash(dashPower, AtypeSkillJoyStick.Instance.GetJoyStickHorizonValue() * 2, AtypeSkillJoyStick.Instance.GetJoyStickVerticalValue() * 2);
    }
}
