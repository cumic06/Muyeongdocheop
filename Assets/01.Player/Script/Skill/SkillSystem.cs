using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSystem : MonoBehaviour
{
    [SerializeField] protected List<Vector3> skillRange;
    [SerializeField] protected float skillCoolTime;
    protected PlayerMoveMent playerMoveMent;

    private bool canUseSkill;

    protected virtual void Awake()
    {
        playerMoveMent = GetComponent<PlayerMoveMent>();
    }

    protected virtual int ReturnSkillHalfRange()
    {
        return Mathf.RoundToInt(skillRange[1].x * 0.5f);
    }

    protected void Skill()
    {

    }

    protected virtual void UseSkill()
    {

    }
}
