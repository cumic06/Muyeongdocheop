using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public Action hitAction;

    [SerializeField] private Image hitImage;

    protected override void Awake()
    {
        base.Awake();
        hitAction += HitEffect;
    }

    [ContextMenu("HitEffect")]
    private void HitEffect()
    {
        hitImage.gameObject.SetActive(true);
        EffectTime();
    }

    private void EffectTime()
    {
        TimeAgent agent = new(0.5f, endTimeAction: (effect) => hitImage.gameObject.SetActive(false));
        TimeManager.Instance.AddTimer(agent);
        Debug.Log(agent.CurrentTime);
    }
}
