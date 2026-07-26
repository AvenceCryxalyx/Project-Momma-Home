using UnityEngine;

public class PausedMenuUIController : MonoBehaviour
{
    [SerializeField] private GameObject allButtons;
    [SerializeField] private GameObject options; 

    private GameState prevGameState;

    private void OnEnable()
    {
        prevGameState = GameManager.instance.CurrentState;
        GameManager.instance.PauseGame();
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        AudioManager.instance.UpdateBGMSuspension(true);
    }

    private void OnDisable()
    {
        switch (prevGameState)
        {
            case GameState.Uninitialized:
                GameManager.instance.BackToMainMenu(false);
                break;
            case GameState.Playing:
                GameManager.instance.ResumePlaying();
                break;
            case GameState.Paused:
                break;
            case GameState.GameOver:
                break;
            case GameState.GameFinished:
                break;
            default:
                break;
        }
        AudioManager.instance.UpdateBGMSuspension(false);
        Cursor.lockState = CursorLockMode.Locked;
        GameManager.instance.Player.GetComponent<MovementController>().enabled = true;
        GameManager.instance.Player.GetComponentInChildren<LookController>().enabled = true;
        Time.timeScale = 1f;
    }


    public void ShowOptions()
    {
        allButtons.SetActive(false);
        options.SetActive(true);
    }

    public void HideOptions()
    {
        options.SetActive(false);
        allButtons.SetActive(true);
    }

    public void SHOWBASE()
    {
        allButtons.SetActive(false);
    }
}
