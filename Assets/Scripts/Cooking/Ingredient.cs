using UnityEngine;
using UnityEngine.AI;

public class Ingredient : PickupableObject
{
    [SerializeField] private float distanceOffset = 10f;
    [SerializeField] private IngredientSO so;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private Transform groundChecker;
    [SerializeField] private LayerMask groundMask;
    public string Name { get; private set; }

    private PickupableObject pickUp;
    private SpriteRenderer spriteRend;
    private CharacterController characterController;
    private bool isGrounded;
    //private Animator animator;

    private void Awake()
    {
        if(so != null)
        {
            Initialize(so);
        }
        spriteRend = GetComponentInChildren<SpriteRenderer>();
        pickUp = GetComponent<PickupableObject>();

        pickUp.EvtInteracted.AddListener(OnInteractable);
        //animator = GetComponent<Animator>();
    }

    public void Initialize(IngredientSO so)
    {
        Name = so.Name;
        spriteRend.sprite = so.Image;
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

        if (GameManager.instance.Player == null || !isGrounded)
            return;
        if (!isPlayerClose())
            return;
        Vector3 playerPos = new Vector3(GameManager.instance.Player.gameObject.transform.position.x, transform.position.y, GameManager.instance.Player.gameObject.transform.position.z);
        Vector3 normDir = Vector3.Normalize(playerPos - gameObject.transform.position);
        normDir = Quaternion.AngleAxis(15, Vector3.up) * normDir;

        transform.position -= (normDir * distanceOffset) * Time.deltaTime;
    }

    private void OnInteractable(InteractionController obj)
    {
        GetComponent<Rigidbody>().detectCollisions = false;
    }

    private void OnDrop(PickupableObject obj)
    {
        GetComponent<Rigidbody>().detectCollisions = true;
    }
}
