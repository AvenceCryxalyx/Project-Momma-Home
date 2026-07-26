using UnityEngine;

public class ScreenTransition : MonoBehaviour
{
    public static ScreenTransition Instance;
    [SerializeField]
    private Animator animator;

    int wipeIdleIn = Animator.StringToHash("WipeIdleIn");
    int RemoveOverlay = Animator.StringToHash("RemoveOverlay");
    int StartWipeIn = Animator.StringToHash("StartWipeIn");

    void Awake()
    {
        DontDestroyOnLoad(this);
        Instance ??= this;
        if (Instance != this)
        {
            Destroy(this);
        }
        animator.Play(wipeIdleIn);
    }

    public void WipeIn()
    {
        animator.Play(wipeIdleIn);
        animator.SetTrigger(StartWipeIn);
    }

    public void WipeOut()
    {
        animator.SetTrigger(RemoveOverlay);
    }

    public bool IsBlocked()
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName("Blocking");
    }
}
