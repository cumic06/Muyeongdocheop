using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fsm_Patteren6 : Fsm
{
    private Monster Monster_Type;
    public Fsm_Patteren6(Monster monster_Type)
    {
        Monster_Type = monster_Type;
    }

    public void Fsm_Action()
    {
        PatterenAttack6();
    }
    public void PatterenAttack6()
    {

    }
}
