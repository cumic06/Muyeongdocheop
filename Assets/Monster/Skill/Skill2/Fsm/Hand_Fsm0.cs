using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand_Fsm0 : IFsm
{
    private readonly Monster main;
    int[] Des = new int[2] { 1, 2 };

    public Hand_Fsm0(Monster main)
    {
        this.main = main;
    }

    public void Fsm_Action()
    {
        main.transform.position = main.StrObject[0].transform.position;
        //main.InputSetActive(Des);
        main.Check = false;
        main._coroutine = main.StartCoroutine(main.PosMove(main.Position1));
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