using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public Action hitAction;
    public Action hpAction;

    [SerializeField] private Image hitImage;

    protected override void Awake()
    {
        base.Awake();
        hitAction += () => UIActiveSystem(0.5f, hitImage.gameObject);
    }

    #region UIActiveSystem
    private void UIActiveSystem(float disableTime, GameObject ui)
    {
        UIActive(ui);
        UIDisableTime(disableTime, () => UIDisable(disableTime, ui));
    }

    private void UIActive(GameObject ui)
    {
        ui.SetActive(true);
    }

    private void UIDisable(float disableTime, GameObject ui)
    {
        ui.SetActive(false);
    }

    private void UIDisableTime(float disableTime, Action Method)
    {
        TimeAgent agent = new(disableTime, endTimeAction: (agent) => Method());
        TimeManager.Instance.AddTimer(agent);
    }
    #endregion
}
