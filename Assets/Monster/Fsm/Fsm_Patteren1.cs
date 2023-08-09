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
        PatterenAttack();
    }
    public void PatterenAttack()
    {
        Debug.Log("∆–≈œ1");
    }
}
