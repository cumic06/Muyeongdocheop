using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AtypeSkillJoyStick : JoyStick
{
    public static AtypeSkillJoyStick Instance;

    private void Awake()
    {
        Instance = GetComponent<AtypeSkillJoyStick>();
    }

    public override float GetJoyStickHorizonValue()
    {
        return base.GetJoyStickHorizonValue();
    }

    public override bool CheckJoyStickMove()
    {
        return GetJoyStickHorizonValue() != 0;
    }
}
