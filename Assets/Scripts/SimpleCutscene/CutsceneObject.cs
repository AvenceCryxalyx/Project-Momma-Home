using UnityEngine;

public class CutsceneObject : MonoBehaviour
{
    public bool Shown { get; private set; }
    [HideInInspector]
    public bool SlightSkip;

    [SerializeField]
    private Transform hiddenTransform;
    [SerializeField]
    private Transform fullShownTransform;
    [SerializeField]
    private float transitionDuration;
    [SerializeField]
    private RectTransform subjectRect;

    private float transitionTime;
    private bool show;
    private bool isShowing;

    void Awake()
    {
        subjectRect.localPosition = hiddenTransform.localPosition;
        isShowing = false;
        Shown = false;
        SlightSkip = false;
    }

    void Update()
    {
        if (!isShowing)
        {
            return;
        }
        if (SlightSkip)
        {
            transitionTime = show ? 1f : 0f;
        }
        float deltaTime = Time.deltaTime * (show ? 1f : -1f);
        transitionTime += deltaTime / transitionDuration;
        subjectRect.localPosition = Vector3.Lerp(hiddenTransform.localPosition, fullShownTransform.localPosition, transitionTime);
        if (transitionTime < 0f || transitionTime > 1f)
        {
            isShowing = false;
            transitionTime = show ? 1f : 0f;
            subjectRect.localPosition = Vector3.Lerp(hiddenTransform.localPosition, fullShownTransform.localPosition, transitionTime);
            if (!show)
            {
                Shown = true;
            }
            SlightSkip = false;
        }
    }

    public void Progress()
    {
        if (isShowing)
        {
            SlightSkip = true;
            return;
        }
        if(transitionTime >= 1f)
        {
            isShowing = true;
            show = false;
            return;
        }
        if (!Shown)
        {
            isShowing = true;
            show = true;
        }
    }
}
