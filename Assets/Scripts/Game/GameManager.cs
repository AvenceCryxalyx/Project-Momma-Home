using System.Collections;
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

    private bool sceneLoading;

    public void Awake()
    {
        if (instance == null)
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
        IEnumerator loadAsync()
        {
            this.sceneLoading = true;
            AsyncOperation sceneLoading = SceneManager.LoadSceneAsync(MainSceneName, LoadSceneMode.Single);
            sceneLoading.allowSceneActivation = false;
            yield return new WaitUntil(() => ScreenTransition.Instance.IsBlocked());
            sceneLoading.allowSceneActivation = true;
            this.sceneLoading = false;
        }
        if (sceneLoading)
        {
            return;
        }
        ScreenTransition.Instance.WipeIn();
        StartCoroutine(loadAsync());
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

        if (reload)
        {
            IEnumerator loadAsync()
            {
                this.sceneLoading = true;
                AsyncOperation sceneLoading = SceneManager.LoadSceneAsync(MainMenuScene, LoadSceneMode.Single);
                sceneLoading.allowSceneActivation = false;
                yield return new WaitUntil(() => ScreenTransition.Instance.IsBlocked());
                sceneLoading.allowSceneActivation = true;
                this.sceneLoading = false;
            }
            if (sceneLoading)
            {
                return;
            }
            ScreenTransition.Instance.WipeIn();
            StartCoroutine(loadAsync());
            //SceneManager.LoadScene("MainMenu");
        }
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ScreenTransition.Instance.WipeOut();
        if (scene.name == MainSceneName)
        {
            if (Player == null)
            {
                Player = FindFirstObjectByType<PlayerInput>().gameObject;
                CurrentState = GameState.Playing;
            }
        }
    }
}
