using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand_Fsm3 : Fsm
{
    private Hand Main;

    public Hand_Fsm3(Hand Main_)
    {
        this.Main = Main_;
    }

    public void Fsm_Action()
    {
        Debug.Log("∆–≈œ3");
    }
}
