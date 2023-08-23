using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand_Fsm2 : IFsm
{
    private Hand Main;
    int[] Des = new int[1] {1};

    public Hand_Fsm2(Hand Main_)
    {
        //Main.InputSetActive(Des);
        this.Main = Main_;
    }
    public void Fsm_Action()
    {
        Main.transform.position = Main.StrObject[1].transform.position;
        Main.Check = false;
        Main._coroutine = Main.StartCoroutine(Main.PosMove(Main.Position2));
        Main.StartCoroutine(Main.Timer(2));
    }
}
