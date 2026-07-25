using UnityEngine;
using System.Collections.Generic;

public class RecipeUIController : MonoBehaviour
{
    [SerializeField] private RecipeItemUI Prefab;
    [SerializeField] private Transform container;
    [SerializeField] private Vector3 shownPosition;
    [SerializeField] private Vector3 hiddenPosition;


    private bool isShown = false;
    private List<RecipeItemUI> items = new List<RecipeItemUI>();

    public void AddItem(RecipeManager.RecipeItemInfo item)
    {
        RecipeItemUI newItem = Instantiate(Prefab);
        newItem.Initialize(item);
        items.Add(newItem);
        newItem.transform.parent = container;
    }

    public void OnToggleVisibility()
    {
        if(isShown)
        {
            transform.position = hiddenPosition;
        }
        else
        {
            transform.position = shownPosition;
        }

        isShown = !isShown;
    }
}
