using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    #region 변수
    public Action HitAction;
    public Action<float, float> PlayerHpAction;
    public Action<Vector2> RayUIAction;

    [SerializeField] private Image hitImage;
    [SerializeField] private Image playerHpSlider;
    [SerializeField] private Image baldoSkillDirectionImage;
    [SerializeField] private GameObject player;
    [Space]

    [SerializeField] private Text timeTxt;

    [Header("GameOver & GameClear")]
    [SerializeField] private Text clearTxt;
    [SerializeField] private Image gameOverUIPanel;
    [SerializeField] private Image gameOverUI;

    [SerializeField] private Image gameClearUIPanel;
    [SerializeField] private Image gameClearUI;

    [SerializeField] private Button reStartBtn;
    [SerializeField] private Button backToMainBtn;
    [SerializeField] private Button nextBossBtn;

    [Header("Setting")]
    [SerializeField] private Button optionBtn;
    [SerializeField] private Slider fxHandler;
    [SerializeField] private Slider bgmHandler;
    [SerializeField] private Button xBtn;
    [SerializeField] private Button frame30Btn;
    [SerializeField] private Button frame60Btn;
    [SerializeField] private GameObject settingImage;
    [SerializeField] private Toggle vibrate_Toggle;
    [SerializeField] private int vibrateInt;
    #endregion

    protected override void Awake()
    {
        base.Awake();
        HitAction += () => UIActiveSystem(0.5f, hitImage.gameObject);

        RayUIAction += BaldoSkillUIResetPos;
        PlayerHpAction += PlayerHpHandler;

        xBtn.onClick.AddListener(() => { UIDisable(settingImage); GameTimeSystem.Instance.NormalTime(); });
        optionBtn.onClick.AddListener(() => { UIActive(settingImage); GameTimeSystem.Instance.TimeStop(); });

        frame30Btn.onClick.AddListener(() => FrameRate.Instance.SetMaxFrame(30));
        frame60Btn.onClick.AddListener(() => FrameRate.Instance.SetMaxFrame(60));

        if (GameManager.Instance.GetGameScene().Equals(GameScene.Stage))
        {
            reStartBtn.onClick.AddListener(ReStartBtn);
            backToMainBtn.onClick.AddListener(BackToMainBtn);
            nextBossBtn.onClick.AddListener(NextBossBtn);
        }
    }

    private void Start()
    {
        UpdateSystem.Instance.AddUpdateAction(() => { BGMHandlerManager(); });

        bgmHandler.value = SoundSystem.Instance.GetBGMVolume();
        fxHandler.value = SoundSystem.Instance.GetFXVolume();
    }

    private void TimeTextUI()
    {
        float time = GameManager.Instance.GetTime();
        int min = 0;
        int hour = 0;

        while (time >= 60)
        {
            time -= 60;
            min++;
        }

        while (min >= 60)
        {
            min -= 60;
            hour++;
        }

        timeTxt.text = $"PlayTime : {hour}시간 {min}분 {time:F1}초";
    }

    public void GameOverUI()
    {
        UIActive(gameOverUIPanel.gameObject);
        UIActive(gameOverUI.gameObject);
    }

    public void GameClearUI(string bossName)
    {
        clearTxt.text = $"{bossName} Clear !";
        UIActive(gameClearUIPanel.gameObject);
        UIActive(gameClearUI.gameObject);
    }

    #region GameOverBtn
    public void BackToMainBtn()
    {
        StageSelect.Instance.Main_Scene();
    }

    public void ReStartBtn()
    {
        StageSelect.Instance.LoadThisScene();
        gameOverUIPanel.gameObject.SetActive(false);
        GameTimeSystem.Instance.NormalTime();
    }

    public void NextBossBtn()
    {
        StageSelect.Instance.StageSelect_Scene();
    }
    #endregion

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

    #region Vibrate
    public void Vibrate()
    {
        if (vibrate_Toggle.isOn)
        {
            Vibration.Vibrate(150);
        }
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

    public Vector2 GetBaldoSkillUIPos()
    {
        Vector2 balldoSkillUIPos = Camera.main.ScreenToWorldPoint(baldoSkillDirectionImage.rectTransform.position);
        return balldoSkillUIPos;
    }

    public bool GetBaldoSkillUIActive()
    {
        return baldoSkillDirectionImage.gameObject.activeSelf;
    }
    #endregion
}
