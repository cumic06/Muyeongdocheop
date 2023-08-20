using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand_Fsm0 : MonoBehaviour, Fsm
{
    private Hand Main;
    public Hand_Fsm0(Hand Main_)
    {
        this.Main = Main_;
    }
    void Start()
    {

    }
    public void Fsm_Action()
    {
        Main._check = false;
        Main._coroutine = Main.StartCoroutine(UP());
        Debug.Log("∆–≈œ0");
        Main.StartCoroutine(Main.Timer(0));
    }
    IEnumerator UP()
    {
        while (true)
        {
            Main.transform.position = Main.player.transform.position - new Vector3(0, 3, 0);
            Main.SavePlayer = Main.player.transform.position;
            while (Main.transform.position.y < Main.SavePlayer.y + 8)
            {
                yield return null;
                Main.transform.Translate(Vector2.up * 0.02f);
            }
            yield return null;
        }
    }

}
