using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Hand_Fsm1 : IFsm
{
    private Hand Main;
    private Vector3 Player_Target;
    Collision2D col;
    float time_;
    public Hand_Fsm1(Hand Main_)
    {
        this.Main = Main_;
    }
    public void Fsm_Action()
    {
        Main.Check = false;
        Main._coroutine = Main.StartCoroutine(Main.PosMove(Main.Position1));
        Main.StartCoroutine(Main.Timer(1));
    }
}
