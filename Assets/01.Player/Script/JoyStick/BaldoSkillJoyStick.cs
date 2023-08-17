using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Collections;
using System;

public class BaldoSkillJoyStick : JoyStick
{
    public static BaldoSkillJoyStick Instance;

    private void Awake()
    {
        Instance = GetComponent<BaldoSkillJoyStick>();
    }

    #region GetSet
    public override float GetJoyStickHorizontalValue()
    {
        return base.GetJoyStickHorizontalValue();
    }

    public override float GetJoyStickVerticalValue()
    {
        return base.GetJoyStickVerticalValue();
    }
    #endregion

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        BaldoSkill.Instance.Skill();
        SetJoyStickHorizontalValue();
        BaldoSkill.Instance.SetCharging(false);
        UIManager.Instance.BaldoSkillUIActive(false);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
    }

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
        BaldoSkill.Instance.SetCharging(true);
        PlayerMoveMent.Instance.SetFilp();

        if (BaldoSkill.Instance.TryAttackMonster(out List<Monster> monster))
        {
            UIManager.Instance.BaldoSkillUIActive(true);
            for (int i = 0; i < monster.Count; i++)
            {
                UIManager.Instance.RayUIAction?.Invoke(monster[i].transform.position);
            }
            return;
        }
        UIManager.Instance.BaldoSkillUIActive(false);

        if (BaldoSkill.Instance.TryWall(out float wallangle, out Vector2Int normal, out RaycastHit2D wallHit, out Vector2 point))
        {
            UIManager.Instance.BaldoSkillUIActive(true);
            UIManager.Instance.RayUIAction?.Invoke(point);
            return;
        }
        UIManager.Instance.BaldoSkillUIActive(false);
    }

    #region Check
    public override bool CheckJoyStickMove()
    {
        return GetJoyStickHorizontalValue() != 0;
    }
    #endregion
}
