using UnityEngine;

public enum GameState
{
    Uninitialized,
    Playing,
    Paused,
    GameOver,
}

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;

    public GameState CurrentState { get; private set; }

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
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

    public void BackToMainMenu()
    {
        CurrentState = GameState.Uninitialized;
    }
}
