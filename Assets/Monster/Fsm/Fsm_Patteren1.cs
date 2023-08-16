using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fsm_Patteren1 : Monster, Fsm
{
    private Monster Monster_Type;
    public Fsm_Patteren1(Monster monster_Type)
    {
        Monster_Type = monster_Type;
    }

    public void Fsm_Action()
    {
        if(Monster_Type._check)
        {
            _fsm[6] = _fsm[Random.Range(1,6)-1];
        }
        PatterenAttack();
    }
    public void PatterenAttack()
    {
        Monster_Type.Skill1.gameObject.SetActive(true);
    }
}
