using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fsm_Patteren3 : Fsm
{
    private Monster Monster_Type;
    public Fsm_Patteren3(Monster monster_Type)
    {
        Monster_Type = monster_Type;
    }

    public void Fsm_Action()
    {
        PatterenAttack3();
    }
    public void PatterenAttack3()
    {

    }
}
