using System.Collections.Generic;
using UnityEngine;

public class IngredientSpawnerActivator : MonoBehaviour
{
    [SerializeField] private List<IngredientSpawner> allSpawners;
    [SerializeField] private int maxActiveSpawners;
    [SerializeField] private float activationInterval = 5f;
    [SerializeField] private IngredientSpawner firstToActivate;

    private List<IngredientSpawner> activeSpawners = new List<IngredientSpawner>();
    private List<IngredientSpawner> pool = new List<IngredientSpawner>();
    private float timeElapsed;

    private void Awake()
    {
        foreach (IngredientSpawner spawner in allSpawners)
        {
            spawner.EvtInteracted.AddListener(OnSpawnerInteracted);
            pool.Add(spawner);
        }
    }

    private void Start()
    {
        ActivateFirstSpawner();
    }

    private void Update()
    {
        if (GameManager.instance == null)
            return;

        if (GameManager.instance.CurrentState != GameState.Playing)
            return;

        if (timeElapsed >= activationInterval)
        {
            ActivateRandomSpawner();

            if (pool.Count <= 0)
            {
                pool.AddRange(allSpawners);
            }
            return;
        }

        timeElapsed += Time.deltaTime;
    }

    private void ActivateFirstSpawner()
    {
        firstToActivate.Activate();
        activeSpawners.Add(firstToActivate);
        timeElapsed = 0;
    }

    private void ActivateRandomSpawner()
    {
        if (activeSpawners.Count >= maxActiveSpawners && pool.Count == 0)
            return;

        int rand = Random.Range(0, pool.Count);
        IngredientSpawner active = pool[rand];
        active.Activate();
        activeSpawners.Add(active);
        timeElapsed = 0;
        pool.Remove(active);
    }

    private void OnSpawnerInteracted(IInteractable interactable, InteractionController controller)
    {
        if(interactable is IngredientSpawner)
        {
            IngredientSpawner spawnwer = interactable as IngredientSpawner;

            if(activeSpawners.Contains(spawnwer))
                activeSpawners.Remove(spawnwer);
        }
    }
}
