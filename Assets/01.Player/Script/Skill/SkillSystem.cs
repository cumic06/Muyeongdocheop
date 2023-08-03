using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillSystem : MonoBehaviour
{
    [SerializeField] protected List<Vector3> skillRange;
    [SerializeField] protected float skillCoolTime;
    private PlayerMoveMent playerMoveMent;

    protected void Awake()
    {
        playerMoveMent = GetComponent<PlayerMoveMent>();
    }

    protected virtual void Update()
    {
        Skill();
    }

    protected virtual void Skill()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E");
            if (SkillCool())
            {
                UseSkill();
            }
        }
    }

    protected virtual int ReturnSkillHalfRange()
    {
        return Mathf.RoundToInt(skillRange[1].x * 0.5f);
    }

    protected bool SkillCool()
    {
        TimeAgent agent = new(skillCoolTime);
        TimerSystem.Instance.AddTimer(agent);
        Debug.Log(agent.CurrentTime);
        Debug.Log(agent.IsTimeUp);
        return agent.IsTimeUp;
    }

    protected abstract void UseSkill();
}
