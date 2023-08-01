using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveJoyStick : JoyStick
{
    public static MoveJoyStick Instance;

    [SerializeField] private RectTransform moveJoyStickPoint;

    private void Awake()
    {
        Instance = GetComponent<MoveJoyStick>();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);

    }

    protected override void SetHandle(PointerEventData eventData)
    {
        float joystickDistance = eventData.position.x - center.position.x;

        float limitXPos = Mathf.Clamp(joystickDistance, joyStickXMinPos, joyStickXMaxPos);

        Vector2 joyStickPos = new(limitXPos, 0);

        joystickRect.anchoredPosition = joyStickPos;

        SetJoyStickHorizonValue();
        base.SetHandle(eventData);
        Debug.Log("MoveJoyStick");
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
