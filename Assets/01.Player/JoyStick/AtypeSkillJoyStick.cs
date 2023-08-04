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

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
    }

    public override float GetJoyStickHorizonValue()
    {
        return base.GetJoyStickHorizonValue();
    }

    public override float GetJoyStickVerticalValue()
    {
        return base.GetJoyStickVerticalValue();
    }

    public override bool CheckJoyStickMove()
    {
        return GetJoyStickHorizonValue() != 0;
    }

}
