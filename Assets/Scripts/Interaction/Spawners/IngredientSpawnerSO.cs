using UnityEngine;
using static IngredientSpawner;

[CreateAssetMenu(fileName = "IngredientSpawnerSO", menuName = "Scriptable Objects/IngredientSpawnerSO")]
public class IngredientSpawnerSO : ScriptableObject
{
    public int minSpawnAmount = 1;
    public int maxSpawnAmount = 5;
    public Vector3 BaseDirection = Vector3.up;
    public float BaseForce = 2f;
    public SpawnLaunchParameters SpawnLaunchParameters;
}
