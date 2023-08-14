using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillSystem : MonoBehaviour
{
    [SerializeField] protected Vector2 skillRange;
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
    protected float GetSkillHalfHorizontalRange()
    {
        return skillRange.x * 0.5f;
    }

    protected float GetSkillHalfVerticalRange()
    {
        return skillRange.y * 0.5f;
    }

    public Vector2 GetSkillRange()
    {
        return skillRange;
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
