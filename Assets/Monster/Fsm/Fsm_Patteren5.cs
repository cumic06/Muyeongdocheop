using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fsm_Patteren5 : Monster, Fsm
{
    private Monster Monster_Type;
    public Fsm_Patteren5(Monster monster_Type)
    {
        Monster_Type = monster_Type;
    }

    public void Fsm_Action()
    {
        PatterenAttack5();
    }
    public void PatterenAttack5()
    {

    }
}
