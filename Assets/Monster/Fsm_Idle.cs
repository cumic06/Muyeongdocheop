using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fsm_Idle : Monster, Fsm
{
    private GameObject Monster_Type;
    
    public Fsm_Idle(GameObject monster_Type)
    {
        Monster_Type = monster_Type;
    }

    public void Fsm_Action()
    {
        Idle();
    }
    public void Idle()
    {
        Monster_Type.transform.Translate(new Vector3(0.0025f * Monster_Type.transform.localScale.x, 0, 0));
        Monster_Type.transform.localScale = new Vector3(-(Monster_Type.transform.localScale.x), 2, 2);
    }
}
