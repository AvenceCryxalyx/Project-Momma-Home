using UnityEngine;

[CreateAssetMenu(fileName = "Ingredients", menuName = "Scriptable Objects/Ingredients")]
public class IngredientSO : ScriptableObject
{
    public Sprite Image;
    public string Name;
}
