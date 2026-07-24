using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    [SerializeField] private RecipeRequirementsSO so;
    public bool IsComplete { get; private set; }

    public static RecipeManager instance;
    private void Awake()
    {
        if(instance == null)
            instance = this;
    }
}
