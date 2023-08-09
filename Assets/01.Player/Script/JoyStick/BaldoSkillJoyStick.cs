using UnityEngine;
using UnityEngine.EventSystems;

public class BaldoSkillJoyStick : JoyStick
{
    public static BaldoSkillJoyStick Instance;

    private bool isJoystickClick;

    private void Awake()
    {
        Instance = GetComponent<BaldoSkillJoyStick>();
    }

    #region GetSet
    public override float GetJoyStickHorizontalValue()
    {
        return base.GetJoyStickHorizontalValue();
    }

    public override float GetJoyStickVerticalValue()
    {
        return base.GetJoyStickVerticalValue();
    }

    public void SetIsJoyStickClick(bool isClick)
    {
        isJoystickClick = isClick;
    }
    #endregion

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        BaldoSkill.Instance.Skill();
        SetJoyStickHorizontalValue();
        UIManager.Instance.BaldoSkillUIActive(false);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
    }

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
        UIManager.Instance.BaldoSkillUIActive(true);
    }

    #region Check
    public override bool CheckJoyStickMove()
    {
        return GetJoyStickHorizontalValue() != 0;
    }

    public bool CheckIsClick()
    {
        return isJoystickClick;
    }
    #endregion
}
