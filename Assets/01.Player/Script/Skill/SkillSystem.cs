using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillSystem : MonoBehaviour
{
    [SerializeField] protected List<Vector3> skillRange;
    [SerializeField] protected float skillCoolTime;

    protected bool canUseSkill;

    protected virtual void Awake()
    {

    }

    protected void Start()
    {
        canUseSkill = true;
    }

    protected int ReturnSkillHalfRange()
    {
        return Mathf.RoundToInt(skillRange[0].x * 0.5f);
    }

    public void Skill()
    {
        if (canUseSkill)
        {
            UseSkill();
            SkillCoolTime();
            canUseSkill = false;
        }
        else
        {
            Debug.Log("CantUse");
        }
    }

    protected void SkillCoolTime()
    {
        TimeAgent agent = new(skillCoolTime, endTimeAction: (agent) => CheckCanUseSkill());
        TimerSystem.Instance.AddTimer(agent);
    }

    protected bool CheckCanUseSkill()
    {
        return canUseSkill = true;
    }

    protected abstract void UseSkill();
}
