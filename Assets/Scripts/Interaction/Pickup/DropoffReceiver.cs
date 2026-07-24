using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DroppedOffEvent : UnityEvent<PickupableObject> { }

public class DropoffReceiver : MonoBehaviour, IInteractable
{
    public DroppedOffEvent EvtDroppedOff;
    [SerializeField] private InteractedEvent _evtInteracted;
    public InteractedEvent EvtInteracted => _evtInteracted;

    public void DropOff(PickupableObject pickup)
    {
        if (pickup == null)
            return;

        if (EvtDroppedOff != null)
        {
            EvtDroppedOff.Invoke(pickup);
        }

        pickup.gameObject.PoolOrDestroy();
    }

    public void Interact(InteractionController interactor)
    {
        if(_evtInteracted != null)
        {
            _evtInteracted.Invoke(interactor);
        }
        interactor.GetComponent<PickupHandler>().DropTo(this);
    }

    public string InteractionText()
    {
        return "Add to Pot [E]";
    }

    public bool IsInteractable(InteractionController interactor)
    {
        return interactor.GetComponent<PickupHandler>().CurrentObject != null;
    }
}
