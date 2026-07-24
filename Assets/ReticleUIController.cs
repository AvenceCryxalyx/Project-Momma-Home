using UnityEngine;
using UnityEngine.UI;

public class ReticleUIController : MonoBehaviour
{
    public Sprite[] reticles;

    public static ReticleUIController instance;
    private Image reticleImage;

    private void Awake()
    {
        if(instance == null)
            instance = this;

        reticleImage = GetComponent<Image>();
    }

    public void UpdateReticle(IInteractable interactable)
    {
        if(interactable is PickupableObject)
        {
            reticleImage.sprite = reticles[1];
        }

        if(interactable is DropoffReceiver)
        {
            reticleImage.sprite = reticles[2];
        }

        if(interactable is IngredientSpawner)
        {
            reticleImage.sprite = reticles[3];
        }
    }

    public void ClearReticle()
    {
        reticleImage.sprite = reticles[0];
    }
}
