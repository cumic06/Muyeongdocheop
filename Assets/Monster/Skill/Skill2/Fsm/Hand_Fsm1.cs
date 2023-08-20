using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Hand_Fsm1 : Fsm
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
        Main._check = false;
        Main._coroutine = Main.StartCoroutine(Chopping());
        Main.StartCoroutine(Main.Timer(1));
    }
    IEnumerator Chopping()
    {
        while (true)
        {
            Main.transform.position = Vector3.zero;
            Player_Target =  Main.player.transform.position - Main.transform.position;
            Player_Target.Normalize();
            Main.hand.transform.rotation = Main.player.transform.rotation;
            Main.SavePlayer = Main.player.transform.position;
            while (time_ < 2.5f)
            {
                time_ += Time.fixedDeltaTime;
                yield return null;
                Main.transform.Translate(Player_Target * 0.03f);
            }
            time_ = 0;  
            Main.transform.position = Vector3.zero + new Vector3(0, 10.71f,0);
            Main.hand.transform.localScale = new Vector3(14, 14, 1);
            while (time_ < 1.5f)
            {
                time_ += Time.fixedDeltaTime;
                yield return null;
                Main.transform.Translate(Vector3.down * 0.05f);
            }
            Main.hand.transform.localScale = new Vector3(4, 4, 1);
            yield return null;

        }
    }
}
