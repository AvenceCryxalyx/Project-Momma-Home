using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Ingredient : PickupableObject
{
    [SerializeField] private float distanceOffset = 10f;
    [SerializeField] private IngredientSO so;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private Transform groundChecker;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private ParticleSystem godrays;
    [SerializeField] private SpriteRenderer wings;
    public string Name { get; private set; }

    
    private Rigidbody rb;
    private Coroutine ExpiredCor;
    private PickupableObject pickUp;
    private SpriteRenderer spriteRend;
    private CharacterController characterController;
    private bool isGrounded;
    private bool isExpired;
    //private Animator animator;

    private void Awake()
    {
        if(so != null)
        {
            Initialize(so);
        }
        rb = GetComponent<Rigidbody>();
        spriteRend = GetComponentInChildren<SpriteRenderer>();
        pickUp = GetComponent<PickupableObject>();

        pickUp.EvtInteracted.AddListener(OnInteractable);
    }

    public void Initialize(IngredientSO so)
    {
        this.so = so;
        Name = so.Name;
        spriteRend.sprite = so.AliveSprite;
    }

    private bool isPlayerClose()
    {
        if (GameManager.instance.Player == null)
            return false;

        return Vector3.Distance(GameManager.instance.Player.transform.position, gameObject.transform.position) < 15f;
    }

    private void Update()
    {
        if(spriteRend)
            spriteRend.transform.LookAt(new Vector3(Camera.main.transform.position.x, transform.position.y, Camera.main.transform.position.z));

        isGrounded = Physics.CheckSphere(groundChecker.position, groundDistance, groundMask);
        if (GameManager.instance == null)
            return;

        if (GameManager.instance.Player == null || !isGrounded)
            return;
        if (!isPlayerClose())
            return;
        Vector3 playerPos = new Vector3(GameManager.instance.Player.gameObject.transform.position.x, transform.position.y, GameManager.instance.Player.gameObject.transform.position.z);
        Vector3 normDir = Vector3.Normalize(playerPos - gameObject.transform.position);
        normDir = Quaternion.AngleAxis(15, Vector3.up) * normDir;

        transform.position -= (normDir * distanceOffset) * Time.deltaTime;
    }

    private void OnInteractable(IInteractable interactable, InteractionController obj)
    {
        rb.detectCollisions = false;
    }

    private void OnDrop(PickupableObject obj)
    {
        rb.detectCollisions = true;
    }

    public void OnSpawned(Spawn spawn)
    {
        rb.useGravity = true;
        isExpired = false;
    }

    public void OnExpired(Spawn spawn)
    {
        isExpired = true;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        spriteRend.sprite = so.ExpiredSprite;
        ExpiredCor = StartCoroutine(OnExpiredTask(spawn));
    }

    public void OnDespawned(Spawn spawn)
    {
        if (wings != null)
        {
            wings.gameObject.SetActive(false);
        }
        if (godrays != null)
        {
            godrays.Stop(false, ParticleSystemStopBehavior.StopEmitting);
        }
        StopAllCoroutines();
        ExpiredCor = null;
        gameObject.PoolOrDestroy();
        spriteRend.sprite = so.AliveSprite;
    }

    private IEnumerator OnExpiredTask(Spawn spawn)
    {
        if (wings != null)
        {
            wings.gameObject.SetActive(true);
        }
        if (godrays != null)
        {
            godrays.Play();
        }
        GetComponent<Rigidbody>().useGravity = false;
        yield return new WaitForSeconds(2);
        spawn.OnDespawn();
    }

    public override bool OverrideIsInteractable()
    {
        return 
    }
}
