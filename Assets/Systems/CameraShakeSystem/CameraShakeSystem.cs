using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeSystem : Singleton<CameraShakeSystem>
{
    private Vector3 camOriginPos;

    private void Start()
    {
        camOriginPos = Camera.main.transform.position;
    }

    public void CameraShake(float shakeTime, float shakePower)
    {
        TimeAgent agent = new(shakeTime, updateTimeAction: (agent) => CameraShaking());
        TimerSystem.Instance.AddTimer(agent);

        void CameraShaking()
        {
            Camera.main.transform.localPosition = Random.insideUnitSphere * shakePower + camOriginPos;
        }
    }
}
