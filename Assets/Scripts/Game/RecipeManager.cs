using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]


public class RecipeManager : MonoBehaviour
{
    [System.Serializable]
    public class RecipeItemInfo
    {
        public UnityEvent OnCompleted = new UnityEvent();

        public RecipeRequirementsSO.RecipeItem item;
        public bool IsDone { get { return AmountNeeded <= 0; } }
        public int AmountNeeded { get { return item.amount - AmountGathered; } }
        public int AmountGathered { get; private set; }

        public void Initialize(RecipeRequirementsSO.RecipeItem item)
        {
            this.item = item;
            AmountGathered = 0;
        }

        public void OnProgress(int amount)
        {
            if (IsDone)
                return;
            AmountGathered += amount;
        }
    }

    public bool IsComplete { get; private set; }

    public static RecipeManager instance;

    [SerializeField] private DropoffReceiver recipeReceiver;
    [SerializeField] private RecipeUIController uiController;
    [SerializeField] private RecipeRequirementsSO[] requirementsList;

    private Dictionary<string, RecipeItemInfo> CurrentItems = new Dictionary<string, RecipeItemInfo>();
    public RecipeItemInfo ActiveItem { get; private set; }

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }

    private void Start()
    {
        int count = requirementsList.Length;
        RecipeRequirementsSO so;
        if (count > 1)
        {
            int index = UnityEngine.Random.Range(0, count - 1);
            so = requirementsList[index];
        }
        else
        {
            so = requirementsList[0];
        }

        foreach (RecipeRequirementsSO.RecipeItem item in so.Items)
        {
            RecipeItemInfo itemInfo = new RecipeItemInfo();
            itemInfo.Initialize(item);
            CurrentItems.Add(item.so.Name, itemInfo);
        }

        foreach (RecipeItemInfo info in CurrentItems.Values)
        {
            uiController.AddItem(info);
        }

        recipeReceiver.EvtDroppedOff.AddListener(OnSubmittedIngredient);
    }

    public void OnSubmittedIngredient(PickupableObject pickup)
    {
        Ingredient ingredient = pickup.GetComponent<Ingredient>();

        if (ingredient == null)
            return;
        if (!CurrentItems.ContainsKey(ingredient.Name))
            return;
        //if(ingredient.Name == ActiveItem.item.so.Name)
        //{
            CurrentItems[ingredient.Name].OnProgress(1);
        //}

        IsComplete = CurrentItems.Values.All(x => x.IsDone);
    }
}
