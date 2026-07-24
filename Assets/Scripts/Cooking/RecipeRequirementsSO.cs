using UnityEngine;

[CreateAssetMenu(fileName = "RecipeRequirementsSO", menuName = "Scriptable Objects/RecipeRequirementsSO")]
public class RecipeRequirementsSO : ScriptableObject
{
    public struct RecipeItem
    {
        public IngredientSO so;
        public int amount;
    }

    public RecipeItem[] items;
}
