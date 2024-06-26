using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class JoyStick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] protected RectTransform joystickRect;
    [SerializeField] protected RectTransform center;

    [SerializeField] protected float joyStickXMinPos;
    [SerializeField] protected float joyStickXMaxPos;
    [SerializeField] protected float joyStickYMinPos;
    [SerializeField] protected float joyStickYMaxPos;

    protected float joyStickHorizontalValue;
    public float JoyStickHorizontalValue => joyStickHorizontalValue;

    protected float joyStickVerticalValue;
    public float JoyStickVerticalValue => joyStickVerticalValue;

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

    protected virtual void SetHandle(PointerEventData eventData)
    {
        Vector2 joyStickPos = new(LimitJoyStickXPos(eventData), LimitJoyStickYPos(eventData));

        joystickRect.anchoredPosition = joyStickPos;

        SetJoyStickHorizonValue();
    }

    #region LimitJoystickPos
    protected float LimitJoyStickXPos(PointerEventData eventData)
    {
        float joystickDistance = eventData.position.x - center.position.x;

        float limitXPos = Mathf.Clamp(joystickDistance, joyStickXMinPos, joyStickXMaxPos);
        return limitXPos;
    }

    protected float LimitJoyStickYPos(PointerEventData eventData)
    {
        float joystickDistance = eventData.position.y - center.position.y;

        float limitYPos = Mathf.Clamp(joystickDistance, joyStickYMinPos, joyStickYMaxPos);
        return limitYPos;
    }
    #endregion

    #region JoystickValue Get Set
    protected void SetJoyStickHorizonValue()
    {
        joyStickHorizontalValue = joystickRect.anchoredPosition.x / center.rect.width;
    }

    public virtual float GetJoyStickMoveHorizonValue()
    {
        return JoyStickHorizontalValue;
    }

    protected void SetJoyStickVerticalValue()
    {
        joyStickVerticalValue = joystickRect.anchoredPosition.y / center.rect.height;
    }

    public virtual float GetJoyStickVerticalValue()
    {
        return JoyStickVerticalValue;
    }
    #endregion

    private void ReSetJoystickPos()
    {
        joystickRect.anchoredPosition = center.anchoredPosition;
    }

    public abstract bool CheckJoyStickMove();
}
