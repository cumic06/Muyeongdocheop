using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fsm_recognize : Monster, Fsm
{
    private GameObject Monster_Type;
    public Fsm_recognize(GameObject monster_Type)
    {
        Monster_Type = monster_Type;
    }

    public void Fsm_Action()
    {
        Recognize();
    }
    public void Recognize()
    {

    }
}
