using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fsm_Patteren2 : Fsm
{
    private Monster Monster_Type;
    public Fsm_Patteren2(Monster monster_Type)
    {
        Monster_Type = monster_Type;
    }

    public void Fsm_Action()
    {
        PatterenAttack2();
    }
    public void PatterenAttack2()
    {
        Debug.Log("°¨Áö¿ä");
    }
}
