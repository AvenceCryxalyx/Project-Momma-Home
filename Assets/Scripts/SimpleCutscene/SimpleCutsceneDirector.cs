using UnityEngine;
using UnityEngine.Events;

public class SimpleCutsceneDirector : MonoBehaviour
{
    [SerializeField]
    private CutsceneObject[] cutsceneObjects;
    public UnityEvent EvtCutsceneDone = new UnityEvent();
    private int currentIndex;

    private bool IsFinished = false;

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

    private void Update()
    {
        if(AllDone() && !IsFinished)
        {
            IsFinished = true;

            if (EvtCutsceneDone != null)
            {
                EvtCutsceneDone.Invoke();
            }
        }
    }
}
