using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSystem : MonoBehaviour
{
    [SerializeField] private float destroyTime;

    private void Start()
    {
        TimeAgent agent = new(destroyTime, endTimeAction: (agent) => DestroyEffect());
        TimerSystem.Instance.AddTimer(agent);
    }

    private void DestroyEffect()
    {
        Destroy(gameObject);
    }
}
