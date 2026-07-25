using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class IngredientManager : MonoBehaviour
{
    public static IngredientManager instance;
    public Ingredient Prefab;

    [SerializeField] private IngredientsGlobalSettings settings;
    [SerializeField] private List<IngredientSO> IngredientsList;
    [SerializeField] private int maxActiveIngredientsSpawners;
    private Dictionary<string, IngredientSO> Ingredients = new Dictionary<string, IngredientSO>();
    private PoolSourceController poolController;

    private List<IngredientSpawner> activeSpawners = new List<IngredientSpawner>();

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
        newPick.GetComponent<Spawn>().Setup(settings.spawnLifeTime);
        return newPick;
    }
}
