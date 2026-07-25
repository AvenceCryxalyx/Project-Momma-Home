using UnityEngine;
using UnityEngine.UI;
public class MainMenuUIController : MonoBehaviour
{
    [SerializeField] private Button startBtn;
    [SerializeField] private Button quitBtn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        startBtn.onClick.AddListener(StartGame);
        quitBtn.onClick.AddListener (QuitGame);
    }

    private void StartGame()
    {
        GameManager.instance.StartGame();
    }

    private void QuitGame()
    {
        GameManager.instance.ExitGame();
    }
}
