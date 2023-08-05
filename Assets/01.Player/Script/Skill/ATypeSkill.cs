using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATypeSkill : SkillSystem
{
    public static ATypeSkill Instance;
    protected PlayerMoveMent playerMoveMent;

    protected override void Awake()
    {
        base.Awake();
        playerMoveMent = GetComponent<PlayerMoveMent>();
        Instance = GetComponent<ATypeSkill>();
    }

    protected override void UseSkill()
    {
        playerMoveMent.StartCoroutine(playerMoveMent.Dash(AtypeSkillJoyStick.Instance.GetJoyStickHorizonValue() * 2, AtypeSkillJoyStick.Instance.GetJoyStickVerticalValue() * 2));
    }
}
