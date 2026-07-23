using UnityEngine;
using UnityEngine.Events;
public class InteractedEvent : UnityEvent<IInteractable> { }

public interface IInteractable
{
    public InteractedEvent EvtInteracted { get; }
    public void Interact(InteractionController interactor);
    public string InteractionText();
    public bool IsInteractable(InteractionController interactor);
}
