using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn_FadeOut : Singleton<FadeIn_FadeOut>
{
    public CanvasGroup Canvas_Group;

    void Start()
    {
        Canvas_Group = GetComponent<CanvasGroup>();
    }

    public void Fade_in()
    {
        StartCoroutine(Fade_In());
    }

    public void Fade_out()
    {
        StartCoroutine(Fade_Out());
    }

    IEnumerator Fade_In()
    {
        while (Canvas_Group.alpha < 1)
        {
            Canvas_Group.alpha += Time.deltaTime * 0.5f;
            yield return null;
        }
    }

    IEnumerator Fade_Out()
    {
        while (Canvas_Group.alpha > 0)
        {
            Canvas_Group.alpha -= Time.deltaTime * 0.5f;
            yield return null;
        }
    }
}
