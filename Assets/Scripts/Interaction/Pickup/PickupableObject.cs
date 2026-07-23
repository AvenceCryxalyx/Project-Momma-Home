using UnityEngine;

public class PickupableObject : MonoBehaviour, IInteractable
{
    public string Name { get; private set; }
    private SpriteRenderer spriteRend;
    private PickupHandler handler = null;
    [SerializeField] private InteractedEvent _evtInteracted;

    public InteractedEvent EvtInteracted => _evtInteracted;

    private void Awake()
    {
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void Initialize(IngredientSO so)
    {
        Name = so.Name;
        spriteRend.sprite = so.Image;
    }

    public void Interact(InteractionController interactor)
    {
        if(interactor.GetComponent<PickupHandler>())
        {
            GetComponent<Rigidbody>().isKinematic = true;
            handler = interactor.GetComponent<PickupHandler>();
            handler.OnPickup(this);
        }
    }

    public void Drop()
    {
        transform.parent = null;
        GetComponent<Rigidbody>().isKinematic = false;
    }

    public string InteractionText()
    {
        return "Pickup [Left click]";
    }

    public bool IsInteractable(InteractionController interactor)
    {
        if(interactor.GetComponent<PickupHandler>().CurrentObject == null)
            return true;
        return false;
    }

    private void Update()
    {
        transform.LookAt(new Vector3(Camera.main.transform.position.x, transform.position.y, Camera.main.transform.position.z));
    }
}
