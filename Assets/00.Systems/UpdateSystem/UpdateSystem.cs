using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UpdateSystem : Singleton<UpdateSystem>
{
    private readonly HashSet<Action> updateActionHash = new();

    private readonly HashSet<Action> fixedUpdateActionHash = new();

    private void Start()
    {
        StartCoroutine(UpdateCor());
        StartCoroutine(FixedUpdateCor());
    }

    private IEnumerator UpdateCor()
    {
        while (true)
        {
            if (updateActionHash != null)
            {
                foreach (var updateHash in updateActionHash)
                {
                    updateHash?.Invoke();
                }
            }
            yield return null;
        }
    }

    private IEnumerator FixedUpdateCor()
    {
        while (true)
        {
            if (fixedUpdateActionHash != null)
            {
                foreach (var fixedUpdateHash in fixedUpdateActionHash)
                {
                    fixedUpdateHash?.Invoke();
                }
            }
            yield return new WaitForFixedUpdate();
        }
    }

    public void AddUpdateAction(Action action)
    {
        updateActionHash.Add(action);
    }

    public void RemoveUpdateAction(Action action)
    {
        updateActionHash.Remove(action);
    }

    public void AddFixedUpdateAction(Action action)
    {
        fixedUpdateActionHash.Add(action);
    }

    public void RemoveFixedUpdateAction(Action action)
    {
        fixedUpdateActionHash.Remove(action);
    }
}
