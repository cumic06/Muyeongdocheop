using UnityEngine;

public class GameTimeSystem : MonoSingleton<GameTimeSystem>
{
    private readonly int normalTime = 1;

    public void Slow(float slowTime, float slowTimeValue)
    {
        Time.timeScale = slowTimeValue;

        TimeAgent agent = new(slowTime, endTimeAction: (agent) => NormalTime());
        TimerSystem.Instance.AddTimer(agent);
    }

    public void NormalTime()
    {
        Time.timeScale = normalTime;
    }

    public void TimeStop()
    {
        Time.timeScale = 0;
    }
}
