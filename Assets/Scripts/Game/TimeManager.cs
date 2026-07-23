using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;
    public float TimeRemaining { get; private set; }
    [SerializeField] private float MaxTimerSeconds;

    private float timeElapsed;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        if (GameStateManager.instance.CurrentState != GameState.Playing)
            return;

        timeElapsed += Time.deltaTime;
        TimeRemaining = MaxTimerSeconds - timeElapsed;
    }

    public void AddTime(float TimeAdded)
    {
        timeElapsed -= TimeAdded;
    }

    public void ReduceTime(float TimeReduced)
    {
        timeElapsed += TimeReduced;
    }
}
