using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    private string winMessage;
    [SerializeField]
    private string loseMessage;
    [SerializeField]
    private Sprite winSprite;
    [SerializeField]
    private Sprite loseSprite;
    [SerializeField]
    private TMP_Text GameoverText;
    [SerializeField]
    private Image GameoverDisplay;
    [SerializeField]
    private Animation GameoverDisplayAnimation;
    [SerializeField]
    private TmpTextTransition textTransitioner;

    public void ReturnToMainMenu()
    {
        GameManager.instance.BackToMainMenu();
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void ShowEndScreen(bool win)
    {
        textTransitioner.HideAll();
        StartCoroutine(showLiveReaction());
        if (win)
        {
            GameoverDisplay.overrideSprite = winSprite;
            GameoverText.text = winMessage;
            textTransitioner.PlaySequencedEnterFromCenterBottom();
            return;
        }
        GameoverDisplay.overrideSprite = loseSprite;
        GameoverText.text = loseMessage;
        textTransitioner.PlaySequencedPopEmUp();
    }

    private IEnumerator showLiveReaction()
    {
        GameoverDisplay.color = new Color(1f, 1f, 1f, 0f);
        yield return new WaitForSeconds(1.5f);
        GameoverDisplayAnimation.Play("Show");
    }
}
