using UnityEngine.InputSystem;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField, Range(3f, 10f)] private float hitrange = 3f;
    [SerializeField] private LayerMask layerMask;

    private RaycastHit hit;
    private IInteractable currentTargetInteractable;

    public void OnInteract(InputAction.CallbackContext context)
    {
        if(context.performed && currentTargetInteractable != null)
        {
            currentTargetInteractable.Interact(this);
        }
    }

    private void FixedUpdate()
    {
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, hitrange, layerMask))
        {
            if (hit.collider.GetComponent<IInteractable>() != null)
            {
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();
                if (currentTargetInteractable == interactable)
                {
                    return;
                }
                if (interactable.IsInteractable(this) && currentTargetInteractable != interactable)
                {
                    currentTargetInteractable = interactable;
                    ReticleUIController.instance.UpdateReticle(interactable);
                }
            }
            else if(currentTargetInteractable != null)
            {
                currentTargetInteractable = null;
                ReticleUIController.instance.ClearReticle();
            }
        }
        else if(currentTargetInteractable != null)
        {
            currentTargetInteractable = null;
            ReticleUIController.instance.ClearReticle();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(playerCamera.transform.position, playerCamera.transform.forward);
    }
}
