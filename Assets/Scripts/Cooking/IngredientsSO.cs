using UnityEngine;

[CreateAssetMenu(fileName = "Ingredients", menuName = "Scriptable Objects/Ingredients")]
public class IngredientSO : ScriptableObject
{
    public Sprite AliveSprite;
    public Sprite ExpiredSprite;
    public string Name;
}
