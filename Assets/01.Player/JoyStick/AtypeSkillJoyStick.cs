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
        ATypeSkill.Instance.Skill();
    }

    public override bool CheckJoyStickMove()
    {
        return GetJoyStickHorizonValue() != 0;
    }

}
