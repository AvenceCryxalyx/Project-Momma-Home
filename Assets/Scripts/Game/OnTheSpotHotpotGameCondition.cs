using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OnTheSpotHotpotGameCondition : MonoBehaviour
{
    [SerializeField] private GameOver GameOverObject;
    [SerializeField] private List<GameObject> GameplayUI;
    [SerializeField] private SimpleCutsceneDirector introDirector;
    [SerializeField] private PausedMenuUIController pauseController;
    [SerializeField] private AudioClip SceneBGM;

    private void Awake()
    {
        if(AudioManager.instance)
            AudioManager.instance.UpdateBGMSuspension(true);
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        GameManager.instance.Player.GetComponent<MovementController>().enabled = false;
        GameManager.instance.Player.GetComponentInChildren<LookController>().enabled = false;
        introDirector.ProgressCutScene();
    }

    public void StartGameplay()
    {
        Cursor.lockState = CursorLockMode.Locked;
        GameManager.instance.Player.GetComponent<MovementController>().enabled = true;
        GameManager.instance.Player.GetComponentInChildren<LookController>().enabled = true;
        foreach (GameObject go in GameplayUI)
        {
            go.SetActive(true);
        }
        if (AudioManager.instance)
            AudioManager.instance.PlayBGM(SceneBGM);
        TimeManager.instance.Initialized();
        introDirector.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (GameManager.instance == null || !TimeManager.instance.IsInitialized)
            return;

        if(TimeManager.instance.TimeRemaining <= 0 && GameManager.instance.CurrentState == GameState.Playing)
        {
            GameManager.instance.GameOver();
            GameOverObject.gameObject.SetActive(true);
            GameOverObject.ShowEndScreen(false);
            GameOverObject.GetComponent<SFXListPlayer>().Play(0);
            GameManager.instance.Player.GetComponent<MovementController>().enabled = false;
            GameManager.instance.Player.GetComponentInChildren<LookController>().enabled = false;
        }

        if(GameManager.instance.CurrentState == GameState.Playing && RecipeManager.instance.IsComplete)
        {
            GameManager.instance.GameFinished();
            GameOverObject.gameObject.SetActive(true);
            GameOverObject.ShowEndScreen(true);
            GameOverObject.GetComponent<SFXListPlayer>().Play(1);
            GameManager.instance.Player.GetComponent<MovementController>().enabled = false;
            GameManager.instance.Player.GetComponentInChildren<LookController>().enabled = false;
        }
    }

    public void ToggleEsc(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (GameManager.instance.CurrentState != GameState.Paused)
            {
                GameManager.instance.Player.GetComponent<MovementController>().enabled = false;
                GameManager.instance.Player.GetComponentInChildren<LookController>().enabled = false;
                pauseController.gameObject.SetActive(true);
            }
        }
    }
}
