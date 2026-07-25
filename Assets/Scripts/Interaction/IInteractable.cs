using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InteractedEvent : UnityEvent<IInteractable,InteractionController> { }

public interface IInteractable
{
    public InteractedEvent EvtInteracted { get; }
    public void Interact(InteractionController interactor);
    public bool IsInteractable(InteractionController interactor);
}
