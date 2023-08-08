using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fsm_Attack : Monster, Fsm
{
    private GameObject Monster_Type;
    public Fsm_Attack(GameObject monster_Type)
    {
        Monster_Type = monster_Type;
    }

    public void Fsm_Action()
    {
        Attack();
    }
    public void Attack()
    {

    }
}
