using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : Singleton<JoyStick>, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private RectTransform joystickRect;
    [SerializeField] private RectTransform center;

    [SerializeField] private float joyStickXMinPos;
    [SerializeField] private float joyStickXMaxPos;

    private float joyStickHorizontalValue;
    public float JoyStickHorizontalValue => joyStickHorizontalValue;

    public void OnDrag(PointerEventData eventData)
    {
        SetHandle(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ReSetJoystickPos();
        SetJoyStickHorizonValue();
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        SetHandle(eventData);
    }

    private void SetHandle(PointerEventData eventData)
    {
        float joystickDistance = eventData.position.x - center.position.x;

        float limitXPos = Mathf.Clamp(joystickDistance, joyStickXMinPos, joyStickXMaxPos);

        Vector2 joyStickPos = new(limitXPos, 0);

        joystickRect.anchoredPosition = joyStickPos;

        SetJoyStickHorizonValue();
    }

    #region JoystickValue Get Set
    private void SetJoyStickHorizonValue()
    {
        joyStickHorizontalValue = joystickRect.anchoredPosition.x / center.rect.width;
    }

    public float GetJoyStickMoveHorizonValue()
    {
        return JoyStickHorizontalValue;
    }
    #endregion

    private void ReSetJoystickPos()
    {
        joystickRect.anchoredPosition = center.anchoredPosition;
    }
}
