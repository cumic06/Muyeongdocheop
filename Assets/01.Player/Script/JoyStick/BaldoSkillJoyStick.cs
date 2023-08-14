using UnityEngine;
using UnityEngine.EventSystems;

public class BaldoSkillJoyStick : JoyStick
{
    public static BaldoSkillJoyStick Instance;

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
    #endregion

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        BaldoSkill.Instance.Skill();
        SetJoyStickHorizontalValue();
        UIManager.Instance.BaldoSkillUIActive(false);
        BaldoSkill.Instance.SetCharging(false);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        BaldoSkill.Instance.SetCharging(true);
    }

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
        UIManager.Instance.BaldoSkillUIActive(true);
        PlayerMoveMent.Instance.SetFilp();
    }

    #region Check
    public override bool CheckJoyStickMove()
    {
        return GetJoyStickHorizontalValue() != 0;
    }
    #endregion
}
