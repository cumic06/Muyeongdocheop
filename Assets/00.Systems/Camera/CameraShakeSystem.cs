using UnityEngine;

public class CameraShakeSystem : Singleton<CameraShakeSystem>
{
    public void CameraShake(float shakeTime, float shakePower)
    {
        TimeAgent agent = new(shakeTime, updateTimeAction: (agent) => CameraShaking());
        TimerSystem.Instance.AddTimer(agent);

        void CameraShaking()
        {
            Vector3 camOriginPos = Camera.main.transform.position;
            Camera.main.transform.localPosition = Random.insideUnitSphere * shakePower + camOriginPos;
        }
    }
}