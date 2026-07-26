using System;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;
    public float TimeRemaining { get; private set; }
    public TimeSpan TimeFormatted { get; private set; }
    [SerializeField] private float MaxTimerSeconds;

    private float timeElapsed;
    public bool IsInitialized { get; private set; }

    public void Initialized()
    {
        IsInitialized = true;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        TimeRemaining = MaxTimerSeconds;
        TimeFormatted = TimeSpan.FromSeconds(MaxTimerSeconds);

    }

    private void Update()
    {
        if (GameManager.instance == null)
            return;

        if (GameManager.instance.CurrentState != GameState.Playing || !IsInitialized)
            return;

        timeElapsed += Time.deltaTime;
        TimeRemaining = MaxTimerSeconds - timeElapsed;
        TimeFormatted = TimeSpan.FromSeconds(TimeRemaining);
    }

    public void AddTime(float TimeAdded)
    {
        timeElapsed -= TimeAdded;
        TimeFormatted.Subtract(TimeSpan.FromSeconds(TimeAdded));
    }

    public void ReduceTime(float TimeReduced)
    {
        timeElapsed += TimeReduced;
        TimeFormatted.Add(TimeSpan.FromSeconds(TimeReduced));
    }
}
