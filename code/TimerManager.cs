using System;
using UnityEngine;
using System.Diagnostics;

public class TimerManager : MonoBehaviour
{

    public static string Timer = @"00:00:00.000";
    public bool IsPlaying;
    public float TotalSeconds;

    void Start()
    {
        IsPlaying = true;
    }

    void Update()
    {
        if (Time.timeScale == 0)
        {
            IsPlaying = false;
        }

        if (IsPlaying)
        {
            Timer = StopwatchTimer();
        }
    }

    string StopwatchTimer()
    {
        TotalSeconds += Time.deltaTime;
        TimeSpan timespan = TimeSpan.FromSeconds(TotalSeconds);
        string timer = string.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            timespan.Hours, timespan.Minutes, timespan.Seconds, timespan.Milliseconds);

        return timer;
    }

}