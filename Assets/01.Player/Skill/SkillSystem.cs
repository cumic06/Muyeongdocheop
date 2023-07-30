using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillSystem : MonoBehaviour
{
    [SerializeField] protected List<Vector3> SkillDir;
    [SerializeField] protected float skillCoolTime;

    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, SkillDir[0]);
    }

    protected virtual void Update()
    {
        Skill();
    }

    protected virtual void Skill()
    {
        //if (SkillCool())
        //{
            if (Input.GetKeyDown(KeyCode.E))
            {
                UseSkill();
            }
        //}
    }

    protected void SkillCool()
    {
        TimeAgent agent = new(skillCoolTime, endTimeAction: (agent) => UseSkill());
        TimerSystem.Instance.AddTimer(agent);
    }

    protected abstract void UseSkill();
}
