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
        //Main.StartCoroutine(Main.Timer(1));
    }
    IEnumerator Chopping()
    {
        while (true)
        {
            for (int i=0; i < Main._position.Length; i++)
            {
                yield return new WaitForSeconds(Main._Time);
                Vector3 startPos =Main.transform.position;
                Vector3 targetPos = Main._position[i].transform.position;
                for (float j = 0; j < 1; j += Time.fixedDeltaTime * 1.3f)
                {
                    Main.transform.position = Vector3.Lerp(startPos, targetPos, j);
                    yield return new WaitForFixedUpdate();
                }
                if(Main.transform.position == Main._position[0].transform.position)
                {

                }
            }
            yield return new WaitForSeconds(4-(Main._Time*2));
        }
    }
}
