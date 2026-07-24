using System.Collections.Generic;
using UnityEngine;

public class IngredientManager : MonoBehaviour
{
    public static IngredientManager instance;
    public Ingredient Prefab;

    [SerializeField]private List<IngredientSO> IngredientsList;
    private Dictionary<string, IngredientSO> Ingredients = new Dictionary<string, IngredientSO>();
    private PoolSourceController poolController;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        poolController = GetComponent<PoolSourceController>();

        foreach(IngredientSO so in IngredientsList)
        {
            Ingredients.Add(so.Name, so);
        }
    }

    public PickupableObject GetIngredient(string name)
    {
        Ingredient newPick = poolController.SourceCollection.GetObject(Prefab);
        newPick.Initialize(Ingredients[name]);
        newPick.GetComponent<Rigidbody>().isKinematic = false;
        newPick.transform.parent = null;
        return newPick;
    }
}
