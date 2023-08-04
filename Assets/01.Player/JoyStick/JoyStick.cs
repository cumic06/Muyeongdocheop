using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class JoyStick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    #region variable
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
    #endregion

    #region OnDrag & OnPointer
    public void OnDrag(PointerEventData eventData)
    {
        SetHandle(eventData);
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        ReSetJoystickPos();
        SetJoyStickHorizonValue();
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        SetHandle(eventData);
    }

    protected virtual void SetHandle(PointerEventData eventData)
    {
        Vector2 joyStickPos = GetLimitJoyStick(eventData);

        joystickRect.anchoredPosition = joyStickPos;

        SetJoyStickHorizonValue();
        SetJoyStickVerticalValue();
    }
    #endregion

    #region LimitJoyStick
    protected Vector2 GetLimitJoyStick(PointerEventData eventData)
    {
        float limitXPos = Mathf.Clamp(GetJoyStickDistance(eventData).x, joyStickXMinPos, joyStickXMaxPos);
        float limitYPos = Mathf.Clamp(GetJoyStickDistance(eventData).y, joyStickYMinPos, joyStickYMaxPos);

        return new Vector2(limitXPos, limitYPos);
    }
    #endregion

    protected Vector3 GetJoyStickDistance(PointerEventData eventData)
    {
        Vector3 joystickDistance = (Vector3)eventData.position - center.position;
        return joystickDistance;
    }

    #region JoystickValue Get Set
    protected void SetJoyStickHorizonValue()
    {
        joyStickHorizontalValue = joystickRect.anchoredPosition.x / center.rect.width;
    }

    public virtual float GetJoyStickHorizonValue()
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

    #region ResetJoyStick
    private void ReSetJoystickPos()
    {
        joystickRect.anchoredPosition = center.anchoredPosition;
    }
    #endregion

    #region CheckJoyStick
    public abstract bool CheckJoyStickMove();
    #endregion
}
