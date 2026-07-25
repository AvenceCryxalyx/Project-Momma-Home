using UnityEngine;

[CreateAssetMenu(fileName = "RecipeRequirementsSO", menuName = "Scriptable Objects/RecipeRequirementsSO")]
public class RecipeRequirementsSO : ScriptableObject
{
    [System.Serializable]
    public struct RecipeItem
    {
        public IngredientSO so;
        public int amount;
    }

    public RecipeItem[] Items;
}
