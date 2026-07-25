using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
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

    public WeightedGachaSO weights;
    [SerializeField] private IngredientSpawnerSO so;
    [SerializeField] private Transform SpawnPoint;
    [SerializeField] private InteractedEvent _evtInteracted;

    private Animator animator;
    private Coroutine SpawnCor;
    private Gacha spawnGacha;
    private int spawnAmount;
    private bool isActivated;

    private void Awake()
    {
        if(so)
        {
            spawnGacha = new Gacha(weights.Infos);
            this.spawnAmount = Random.Range(1, weights.Infos.Length);
        }
        if(SpawnPoint == null)
        {
            SpawnPoint = transform;
        }
        if(animator ==  null)
        {
            animator = GetComponent<Animator>();
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
        if (_evtInteracted != null)
        {
            _evtInteracted.Invoke(this, interactor);
        }

        animator.Play("Open");

        SpawnCor = StartCoroutine(SpawnTask());
    }

    private void DoSpawn()
    {
        List<string> spawns = spawnGacha.PullMultiple(spawnAmount);
        foreach (string spawn in spawns)
        {
            PickupableObject pick = IngredientManager.instance.GetIngredient(spawn);
            pick.transform.position = SpawnPoint.transform.position;
            pick.GetComponent<Rigidbody>().AddForce(((transform.forward + so.BaseDirection) + so.SpawnLaunchParameters.GetRandomLaunchForce()) * so.BaseForce);
            pick.GetComponent<Spawn>().OnSpawn();
            isActivated = false;
        }
    }

    public bool IsInteractable(InteractionController interactor)
    {
        return spawnGacha != null && isActivated;
    }

    public void Activate()
    {
        animator.Play("Shake");
        isActivated = true;
    }

    private IEnumerator SpawnTask()
    {
        yield return new WaitForSeconds(0.4f);
        DoSpawn();
        yield return null;
        animator.Play("Idle");
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
