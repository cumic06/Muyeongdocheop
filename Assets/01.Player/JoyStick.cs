using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private RectTransform moveJoystick;
    [SerializeField] private RectTransform center;

    [SerializeField] private float joyStickXMinPos;
    [SerializeField] private float joyStickXMaxPos;

    public float Horizontal => (center.anchoredPosition.x - moveJoystick.anchoredPosition.x) * 2 - 1;

    public void OnDrag(PointerEventData eventData)
    {
        SetHandle(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        moveJoystick.anchoredPosition = center.anchoredPosition;
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

        moveJoystick.anchoredPosition = joyStickPos;
    }
}
