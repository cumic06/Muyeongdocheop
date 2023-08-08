using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillSystem : MonoBehaviour
{
    [SerializeField] protected Vector3 skillRange;
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
    protected int GetSkillHalfHorizontalRange()
    {
        return Mathf.RoundToInt(skillRange.x * 0.5f);
    }

    protected int GetSkillHalfVerticalRange()
    {
        return Mathf.RoundToInt(skillRange.y * 0.5f);
    }

    public Vector3 GetSkillRange()
    {
        return skillRange;
    }

    protected abstract Vector3 GetSkillStartPos();

    protected abstract Vector3 GetSkillDirection();
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
