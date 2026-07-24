using UnityEngine;
using UnityEngine.Events;

public class Spawned : UnityEvent<Spawn> { }
public class Expired : UnityEvent<Spawn> { }
public class Despawn : UnityEvent<Spawn> { }

public class Spawn :  Poolable
{

    public Spawned EvtSpawned = new Spawned();
    public Expired EvtExpired = new Expired();
    public Despawn EvtDespawn = new Despawn();

    public float LifeTime { get; private set; }
    public bool IsAlive { get; private set; }

    private float timeElapsed = 0f;
    public void Setup(float lifeTime)
    {
        LifeTime = lifeTime;
    }

    public void OnSpawn()
    {
        timeElapsed = 0;
        IsAlive = true;
        if (EvtSpawned != null)
        {
            EvtSpawned.Invoke(this);
        }
    }

    public void OnExpired()
    {
        IsAlive = false;
        if(EvtExpired != null)
        {
            EvtExpired.Invoke(this);
        }
    }

    public void OnDespawn()
    {
        if (EvtDespawn != null)
        {
            EvtDespawn.Invoke(this);
        }
    }

    public void Update()
    {
        if (!IsAlive)
        {
            return;
        }

        timeElapsed += Time.deltaTime;
    }
}
