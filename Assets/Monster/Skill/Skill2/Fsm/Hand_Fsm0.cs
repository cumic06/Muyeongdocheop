using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand_Fsm0 : IFsm
{
    private readonly Hand main;

    public Hand_Fsm0(Hand main)
    {
        this.main = main;
    }

    public void Fsm_Action()
    {
        main.Check = false;
        main._coroutine = main.StartCoroutine(main.PosMove(main.Position0));
        Debug.Log("∆–≈œ0");
        main.StartCoroutine(main.Timer(0));
    }
     
    IEnumerator UP()
    {
        while (true)
        {
            main.transform.position = main.Player.transform.position - new Vector3(0, 3, 0);
            main.SavePlayer = main.Player.transform.position;
            while (main.transform.position.y < main.SavePlayer.y + 8)
            {
                yield return null;
                main.transform.Translate(Vector2.up * 0.02f);
            }
            yield return null;
        }
    }
}
