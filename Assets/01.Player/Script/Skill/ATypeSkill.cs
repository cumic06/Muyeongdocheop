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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + new Vector3(ReturnSkillHalfRange(), 0, 0), skillRange[0]);
    }

    protected override void UseSkill()
    {
        Debug.Log("UseSkill");
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
        playerMoveMent.Dash(25f, AtypeSkillJoyStick.Instance.GetJoyStickHorizonValue() * 2, AtypeSkillJoyStick.Instance.GetJoyStickVerticalValue() * 2);

=======
        playerMoveMent.Dash(50f, AtypeSkillJoyStick.Instance.GetJoyStickHorizonValue() * 2, AtypeSkillJoyStick.Instance.GetJoyStickVerticalValue() * 2);
>>>>>>> parent of 8f68c1d (Dash MoveWay & Background Settings & Change And Assets)
=======
        playerMoveMent.Dash(50f, AtypeSkillJoyStick.Instance.GetJoyStickHorizonValue() * 2, AtypeSkillJoyStick.Instance.GetJoyStickVerticalValue() * 2);
>>>>>>> parent of 8f68c1d (Dash MoveWay & Background Settings & Change And Assets)
=======
        playerMoveMent.Dash(50f, AtypeSkillJoyStick.Instance.GetJoyStickHorizonValue() * 2, AtypeSkillJoyStick.Instance.GetJoyStickVerticalValue() * 2);
>>>>>>> parent of 8f68c1d (Dash MoveWay & Background Settings & Change And Assets)
    }
}
