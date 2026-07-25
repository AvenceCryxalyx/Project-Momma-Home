using UnityEngine;

public class PickupableObject : MonoBehaviour, IInteractable
{

    private PickupHandler handler = null;
    [SerializeField] private InteractedEvent _evtInteracted;

    public InteractedEvent EvtInteracted => _evtInteracted;
    public virtual bool OverrideIsInteractable()
    {
        return true;
    }
    public void Interact(InteractionController interactor)
    {
        if (_evtInteracted != null)
        {
            _evtInteracted.Invoke(this,interactor);
        }

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
        GetComponent<Rigidbody>().useGravity = true;
    }

    public bool IsInteractable(InteractionController interactor)
    {
        if(interactor.GetComponent<PickupHandler>().CurrentObject == null && OverrideIsInteractable())
            return true;
        return false;
    }
}
