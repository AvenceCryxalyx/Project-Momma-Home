using UnityEngine;

[CreateAssetMenu(fileName = "IngredientSpawnerGlobalSettings", menuName = "Scriptable Objects/IngredientSpawnerGlobalSettings")]
public class IngredientSpawnerGlobalSettings : ScriptableObject
{
    public float spawningCooldown = 30f;
    public float spawnLifeTime = 10f;
}
