using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
    [SerializeField] private string MainMenuScene = "MainMenu";
    [SerializeField] private string MainSceneName = "MainScene";
    public static GameManager instance;

    public GameObject Player { get; private set; }

    public GameState CurrentState { get; private set; }

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(this);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(MainSceneName, LoadSceneMode.Single);
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
        Cursor.lockState = CursorLockMode.None;
    }

    public void GameFinished()
    {
        CurrentState = GameState.GameFinished;
        Cursor.lockState = CursorLockMode.None;
    }

    public void BackToMainMenu()
    {
        CurrentState = GameState.Uninitialized;
        SceneManager.LoadScene("MainMenu");
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == MainSceneName)
        {
            if (Player == null)
            {
                Player = FindFirstObjectByType<PlayerInput>().gameObject;
                CurrentState = GameState.Playing;
            }
        }
    }
}
