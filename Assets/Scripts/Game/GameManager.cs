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
    public GameSettings GameSettings;
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
        else
            Destroy(this);
        GameSettings = Instantiate(GameSettings);
        DontDestroyOnLoad(this);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(MainSceneName, LoadSceneMode.Single);
    }

    public void ResumePlaying()
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
        Cursor.lockState = CursorLockMode.None;
    }

    public void GameFinished()
    {
        CurrentState = GameState.GameFinished;
        Cursor.lockState = CursorLockMode.None;
    }

    public void BackToMainMenu(bool reload = true)
    {
        CurrentState = GameState.Uninitialized;

        if(reload)
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
