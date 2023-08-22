using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fsm_Patteren4 : IFsm
{
    private Monster Monster_Type;
    public Fsm_Patteren4(Monster monster_Type)
    {
        Monster_Type = monster_Type;
    }

    public void Fsm_Action()
    {
        PatterenAttack4();
    }
    public void PatterenAttack4()
    {

    }
}
