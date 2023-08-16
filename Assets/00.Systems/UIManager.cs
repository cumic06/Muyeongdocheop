using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public Action HitAction;
    public Action<Vector2> RayUIAction;

    [SerializeField] private Image hitImage;
    [SerializeField] private Image playerHpSlider;
    [SerializeField] private Image baldoSkillDirectionImage;
    [SerializeField] private GameObject player;

    protected override void Awake()
    {
        base.Awake();
        HitAction += () => UIActiveSystem(0.5f, hitImage.gameObject);
        RayUIAction += BaldoSkillUIResetPos;
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
    public void BaldoSkillUIActive(bool active)
    {
        baldoSkillDirectionImage.gameObject.SetActive(active);
    }

    public void BaldoSkillUIResetPos(Vector2 targetPos)
    {
        Vector2 screenTargetPos = Camera.main.WorldToScreenPoint(targetPos);
        baldoSkillDirectionImage.rectTransform.position = screenTargetPos;
    }
    #endregion
}
