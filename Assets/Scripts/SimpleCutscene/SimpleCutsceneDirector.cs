using UnityEngine;

public class SimpleCutsceneDirector : MonoBehaviour
{
    [SerializeField]
    private CutsceneObject[] cutsceneObjects;

    private int currentIndex;

    void Awake()
    {
        currentIndex = 0;
    }

    public void ProgressCutScene()
    {
        if (currentIndex >= cutsceneObjects.Length)
        {
            return;
        }
        cutsceneObjects[currentIndex].Progress();
        if (cutsceneObjects[currentIndex].Shown)
        {
            currentIndex++;
            if (currentIndex >= cutsceneObjects.Length)
            {
                return;
            }
            cutsceneObjects[currentIndex].Progress();
        }
    }

    public bool AllDone()
    {
        for (int i = 0; i < cutsceneObjects.Length; i++)
        {
            if (!cutsceneObjects[i].Shown)
            {
                return false;
            }
        }
        return true;
    }
}
