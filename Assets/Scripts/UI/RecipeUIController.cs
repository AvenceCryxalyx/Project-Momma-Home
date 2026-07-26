using UnityEngine;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine.InputSystem;

public class RecipeUIController : MonoBehaviour
{
    [SerializeField] private RecipeItemUI Prefab;
    [SerializeField] private Transform container;
    [SerializeField] private RectTransform fullShownTransform;
    [SerializeField] private RectTransform hiddenTransform;
    [SerializeField] private float transitionDuration;
    [SerializeField] private AudioSource slideInSFX;
    [SerializeField] private AudioSource slideOutSFX;

    private RectTransform thisRect;
    private float transitionTime;
    private bool show = false;
    private bool isShowing = false;
    private List<RecipeItemUI> items = new List<RecipeItemUI>();

    void Awake()
    {
        thisRect = GetComponent<RectTransform>();
        foreach(Transform child in container)
        {
            Destroy(child.gameObject);
        }
    }

    public void AddItem(RecipeManager.RecipeItemInfo item)
    {
        RecipeItemUI newItem = Instantiate(Prefab);
        newItem.Initialize(item);
        items.Add(newItem);
        newItem.transform.parent = container;
        newItem.transform.localScale = Vector3.one;
    }

    public void OnToggleVisibility(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            show = !show;
            isShowing = true;
            slideInSFX.time = 0;
            slideOutSFX.time = 0;
            if(show)
                slideInSFX.Play();
            else
                slideOutSFX.Play();
        }
    }

    void Update()
    {
        if (!isShowing)
        {
            return;
        }
        float deltaTime = Time.deltaTime * (show ? 1f : -1f);
        transitionTime += deltaTime / transitionDuration;
        thisRect.localPosition = Vector3.Lerp(hiddenTransform.localPosition, fullShownTransform.localPosition, transitionTime);
        if (transitionTime < 0f || transitionTime > 1f)
        {
            isShowing = false;
            transitionTime = show ? 1f : 0f;
            thisRect.localPosition = Vector3.Lerp(hiddenTransform.localPosition, fullShownTransform.localPosition, transitionTime);
        }
    }
}
