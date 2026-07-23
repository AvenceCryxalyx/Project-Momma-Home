using TMPro;
using UnityEngine;

public class InteractableText : MonoBehaviour
{
    public static InteractableText instance;

    private  TMP_Text textMeshPro;

    private void Awake()
    {
        if(instance == null)
            instance = this;

        textMeshPro = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void SetVisibility(bool show)
    {
        if(show == gameObject.activeSelf)
        {
            return;
        }
        gameObject.SetActive(show);
    }

    public void SetText(string text)
    {
        textMeshPro.text = text;
    }
}
