using UnityEngine;
using UnityEngine.EventSystems;

public class BaldoSkillJoyStick : JoyStick
{
    public static BaldoSkillJoyStick Instance;

    private void Awake()
    {
        Instance = GetComponent<BaldoSkillJoyStick>();
    }

    public override float GetJoyStickHorizontalValue()
    {
        return base.GetJoyStickHorizontalValue();
    }

    public override float GetJoyStickVerticalValue()
    {
        return base.GetJoyStickVerticalValue();
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        BaldoSkill.Instance.Skill();
        SetJoyStickHorizontalValue();
        UIManager.Instance.BaldoSkillUIActive(false);
    }

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
        UIManager.Instance.BaldoSkillUIActive(true);
    }

    public override bool CheckJoyStickMove()
    {
        return GetJoyStickHorizontalValue() != 0;
    }
}
