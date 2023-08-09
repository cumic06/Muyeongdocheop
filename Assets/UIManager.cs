using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public Action HitAction;

    [SerializeField] private Image hitImage;
    [SerializeField] private Image playerHpSlider;
    [SerializeField] private Image baldoSkillDirectionImage;
    [SerializeField] private GameObject player;

    protected override void Awake()
    {
        base.Awake();
        HitAction += () => UIActiveSystem(0.5f, hitImage.gameObject);
    }

    private void FixedUpdate()
    {
        if (baldoSkillDirectionImage.gameObject.activeSelf)
        {
            BaldoSkillPos();
        }
        else
        {
            BaldoSkillUIResetPos();
        }
    }

    #region UIActiveSystem
    private void UIActiveSystem(float disableTime, GameObject ui)
    {
        UIActive(ui);
        UIDisableTime(disableTime, () => UIDisable(ui));
    }

    private void UIActive(GameObject ui)
    {
        ui.SetActive(true);
    }

    private void UIDisable(GameObject ui)
    {
        ui.SetActive(false);
    }

    private void UIDisableTime(float disableTime, Action Method)
    {
        TimeAgent agent = new(disableTime, endTimeAction: (agent) => Method());
        TimerSystem.Instance.AddTimer(agent);
    }
    #endregion

    #region HpHandler
    private void PlayerHpHandler()
    {
        //playerHpSlider.fillAmount =  / ;
    }
    #endregion

    #region BaldoSkillDirection
    private void BaldoSkillPos()
    {
        Vector2 screenPos = Camera.main.WorldToScreenPoint(player.transform.position);
        Vector2 dir = new(BaldoSkillJoyStick.Instance.GetJoyStickHorizontalValue(), BaldoSkillJoyStick.Instance.GetJoyStickVerticalValue());
        dir.Normalize();

        baldoSkillDirectionImage.rectTransform.position = screenPos + (dir * 100);
    }

    public void BaldoSkillUIActive(bool active)
    {
        baldoSkillDirectionImage.gameObject.SetActive(active);
    }

    public void BaldoSkillUIResetPos()
    {
        Vector2 screenPos = Camera.main.WorldToScreenPoint(player.transform.position);
        baldoSkillDirectionImage.rectTransform.position = screenPos;
    }
    #endregion
}
