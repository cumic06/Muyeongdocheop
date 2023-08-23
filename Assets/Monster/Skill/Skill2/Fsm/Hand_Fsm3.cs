using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand_Fsm3 : IFsm
{
    private Monster Main;

    public Hand_Fsm3(Monster Main_)
    {
        this.Main = Main_;
    }

    public void Fsm_Action()
    {
        
    }

    //IEnumerator Gun() 
    //{
    //    while (true) 
    //    {
    //        Main.transform.position
    //    }
    //}
}
