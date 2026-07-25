using UnityEngine;
using UnityEngine.InputSystem;

public class PickupHandler : MonoBehaviour
{
    [SerializeField] private Transform holdingArea;

    public PickupableObject CurrentObject { get; private set; }

    public void OnPickup(PickupableObject pickup)
    {
        CurrentObject = pickup;
        CurrentObject.transform.position = holdingArea.transform.position;
        CurrentObject.transform.parent = holdingArea;
    }

    public void DropTo(DropoffReceiver target = null)
    {
        if(target != null)
        {
            target.DropOff(CurrentObject);
            CurrentObject = null;
        }
    }

    public void OnDrop(InputAction.CallbackContext context)
    {
        if (CurrentObject != null && context.performed)
        {
            CurrentObject.Drop();
            CurrentObject = null;
        }
    }
}
