using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    private string winMessage;
    [SerializeField]
    private string loseMessage;
    [SerializeField]
    private TMP_Text GameoverText;
    [SerializeField]
    private TmpTextTransition textTransitioner;

    public void ReturnToMainMenu()
    {
        GameManager.instance.BackToMainMenu();
    }

    public void Quit()
    {
        GameManager.instance.ExitGame();
    }

    public void ShowEndScreen(bool win)
    {
        textTransitioner.HideAll();
        if (win)
        {
            GameoverText.text = winMessage;
            textTransitioner.PlaySequencedEnterFromCenterBottom();
            return;
        }
        GameoverText.text = loseMessage;
        textTransitioner.PlaySequencedPopEmUp();
    }
}
