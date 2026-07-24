using UnityEngine;
using UnityEngine.InputSystem;

public enum GameState
{
    Uninitialized,
    Playing,
    Paused,
    GameOver,
    GameFinished
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private GameObject player;

    public GameObject Player { get { return player; } }

    public GameState CurrentState { get; private set; }

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        if(player == null)
        {
            player = FindFirstObjectByType<PlayerInput>().gameObject;
        }
        DontDestroyOnLoad(this);
    }

    public void StartGame()
    {
        CurrentState = GameState.Playing;
    }

    public void PauseGame()
    {
        CurrentState = GameState.Paused;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void GameOver()
    {
        CurrentState = GameState.GameOver;
    }

    public void GameFinished()
    {
        CurrentState = GameState.GameFinished;
    }

    public void BackToMainMenu()
    {
        CurrentState = GameState.Uninitialized;
    }
}
