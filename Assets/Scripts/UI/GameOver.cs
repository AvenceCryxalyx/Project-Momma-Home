using UnityEngine;

public class GameOver : MonoBehaviour
{

    public void ReturnToMainMenu()
    {
        GameManager.instance.BackToMainMenu();
    }

    public void Quit()
    {
        GameManager.instance.ExitGame();
    }
}
