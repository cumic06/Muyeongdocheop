using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Hand_Fsm1 : IFsm
{
    private Monster Main;
    private Vector3 Player_Target;
    Collision2D col;
    float time_;
    public Hand_Fsm1(Monster Main_)
    {
        this.Main = Main_;
    }
    public void Fsm_Action()
    {
        Main.transform.position = Main.StrObject[1].transform.position;
        Main.Check = false;
        Main._coroutine = Main.StartCoroutine(Main.PosMove(Main.Position0));
        Main.StartCoroutine(Main.Timer(1));
    }
}
