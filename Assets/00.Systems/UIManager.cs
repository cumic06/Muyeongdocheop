using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public Action HitAction;
    public Action<float, float> PlayerHpAction;
    public Action<Vector2> RayUIAction;

    [SerializeField] private Image hitImage;
    [SerializeField] private Image playerHpSlider;
    [SerializeField] private Image baldoSkillDirectionImage;
    [SerializeField] private GameObject player;

    [SerializeField] private Slider bgmHandler;
    [SerializeField] private Slider fxHandler;

    protected override void Awake()
    {
        base.Awake();
        HitAction += () => UIActiveSystem(0.5f, hitImage.gameObject);
        RayUIAction += BaldoSkillUIResetPos;
        PlayerHpAction += PlayerHpHandler;
        bgmHandler.value = SoundSystem.Instance.GetBGMVolume();
        fxHandler.value = SoundSystem.Instance.GetFXVolume();
    }

    private void Start()
    {
        UpdateSystem.Instance.AddUpdateAction(BGMHandlerManager);
    }

    #region VolumeHandler
    private void BGMHandlerManager()
    {
        BGMVolumeHandler();
        FXVolumeHandler();
    }

    private void BGMVolumeHandler()
    {
        SoundSystem.Instance.SetBGMVolume(bgmHandler.value);
    }

    private void FXVolumeHandler()
    {
        SoundSystem.Instance.SetFXVolume(fxHandler.value);
    }
    #endregion

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
    private void PlayerHpHandler(float maxHp, float hp)
    {
        playerHpSlider.fillAmount = hp / maxHp;
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
