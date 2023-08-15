using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillSystem : MonoBehaviour
{
    [SerializeField] protected float skillDistance;

    [SerializeField] protected float skillCoolTime;

    protected bool canUseSkill;

    protected virtual void Awake()
    {

    }

    protected void Start()
    {
        canUseSkill = true;
    }

    #region GetValue
    public float GetSkillDistance()
    {
        return skillDistance;
    }

    public float GetSkillHalfDistance()
    {
        float skillHalfDistance = skillDistance * 0.5f;
        return skillHalfDistance;
    }

    public abstract Vector2 GetSkillStartPos();

    protected abstract Vector2 GetSkillDirection();
    #endregion

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
