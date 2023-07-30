using UnityEngine;

public class SlowSystem : Singleton<SlowSystem>
{
    private readonly int normalTime = 1;

    public void Slow(float slowValue, float slowTime)
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
