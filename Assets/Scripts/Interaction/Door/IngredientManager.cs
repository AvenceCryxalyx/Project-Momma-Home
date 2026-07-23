using System.Collections.Generic;
using UnityEngine;

public class IngredientManager : MonoBehaviour
{
    public static IngredientManager instance;
    public PickupableObject Prefab;

    [SerializeField]private List<IngredientSO> IngredientsList;
    private Dictionary<string, PickupableObject> Ingredients;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        Ingredients = new Dictionary<string, PickupableObject>();
        foreach(IngredientSO so in IngredientsList)
        {
            PickupableObject ingredient = Instantiate(Prefab);
            ingredient.GetComponent<Rigidbody>().isKinematic = true;
            ingredient.transform.parent = transform;
            ingredient.Initialize(so);
            Ingredients.Add(ingredient.Name, ingredient);
        }
    }

    public PickupableObject GetIngredient(string name)
    {
        PickupableObject newPick = Instantiate(Ingredients[name]);
        newPick.GetComponent<Rigidbody>().isKinematic = false;
        newPick.transform.parent = null;
        return newPick;
    }
}
