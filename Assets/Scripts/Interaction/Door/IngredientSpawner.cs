using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IngredientSpawner : MonoBehaviour, IInteractable
{
    [System.Serializable]
    public struct SpawnLaunchParameters
    {
        public float minXOffset;
        public float maxXOffset;

        public float minYOffset;
        public float maxYOffset;

        public float minZOffset;
        public float maxZOffset;

        public Vector3 GetRandomLaunchForce()
        {
            float x = Random.Range(minXOffset, maxXOffset);
            float y = Random.Range(minYOffset, maxYOffset);
            float z = Random.Range(minZOffset, maxZOffset);

            return new Vector3(x, y, z);
        }
    }

    [SerializeField] private Vector3 baseDirection;
    [SerializeField] private float baseForce = 100f;
    [SerializeField] private SpawnLaunchParameters spawnLaunchParameters;
    [SerializeField] private Transform SpawnPoint;
    [SerializeField] private InteractedEvent _evtInteracted;
    [SerializeField] private WeightedGachaSO so;
    private Gacha spawnGacha;
    private int spawnAmount;

    private void Awake()
    {
        if(so)
        {
            spawnGacha = new Gacha(so.Infos);
            this.spawnAmount = Random.Range(1, 5);
        }
        if(SpawnPoint == null)
        {
            SpawnPoint = transform;
        }
    }

    public InteractedEvent EvtInteracted => _evtInteracted;

    public void Initialize(WeightedGachaSO so, int spawnAmount)
    {
        spawnGacha = new Gacha(so.Infos);
        this.spawnAmount = spawnAmount;
    }

    public void Interact(InteractionController interactor)
    {
        List<string> spawns = spawnGacha.PullMultiple(spawnAmount);
        foreach(string spawn in spawns)
        {
            PickupableObject pick = IngredientManager.instance.GetIngredient(spawn);
            pick.transform.position = SpawnPoint.transform.position;
            pick.GetComponent<Rigidbody>().AddForce(((transform.forward + baseDirection) + spawnLaunchParameters.GetRandomLaunchForce()) * baseForce);
            pick.GetComponent<Spawn>().OnSpawn();
        }
    }

    public string InteractionText()
    {
        return "Open [Left Click]";
    }

    public bool IsInteractable(InteractionController interactor)
    {
        return spawnGacha != null;
    }
}
