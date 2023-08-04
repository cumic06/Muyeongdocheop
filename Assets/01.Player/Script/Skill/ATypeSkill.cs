using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATypeSkill : SkillSystem
{
    public static ATypeSkill Instance;

    protected override void Awake()
    {
        base.Awake();
        Instance = GetComponent<ATypeSkill>();
    }

#if UNITY_EDITOR
    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Vector3 skillDirection = transform.position + new Vector3(, 0, 0);
        //Gizmos.DrawWireCube(skillDirection, skillRange[1]);
    }
#endif

    protected override void UseSkill()
    {
        Debug.Log("ATypeSkill");
    }
}
