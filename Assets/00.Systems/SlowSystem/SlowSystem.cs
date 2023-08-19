using UnityEngine;

public class SlowSystem : MonoSingleton<SlowSystem>
{
    private readonly int normalTime = 1;

    public void Slow(float slowTime, float slowValue)
    {
        Time.timeScale = slowValue;

        TimeAgent agent = new(slowTime, endTimeAction: (agent) => StopSlow());
        TimerSystem.Instance.AddTimer(agent);
    }

    public void StopSlow()
    {
        Time.timeScale = normalTime;
    }
}
