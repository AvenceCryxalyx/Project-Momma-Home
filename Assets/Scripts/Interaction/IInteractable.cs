using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InteractedEvent : UnityEvent<InteractionController> { }

public interface IInteractable
{
    public InteractedEvent EvtInteracted { get; }
    public void Interact(InteractionController interactor);
    public string InteractionText();
    public bool IsInteractable(InteractionController interactor);
}
