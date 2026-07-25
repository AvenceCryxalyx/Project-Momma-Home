using UnityEngine;
using UnityEngine.InputSystem;

public class OnTheSpotHotpotGameCondition : MonoBehaviour
{
    [SerializeField] private GameObject GameOverObject;
    private void Update()
    {
        if (GameManager.instance == null)
            return;
        if(TimeManager.instance.TimeRemaining <= 0 && GameManager.instance.CurrentState == GameState.Playing)
        {
            GameManager.instance.GameOver();
            GameOverObject.SetActive(true);
            GameManager.instance.Player.GetComponent<MovementController>().enabled = false;
            GameManager.instance.Player.GetComponentInChildren<LookController>().enabled = false;
        }

        if(GameManager.instance.CurrentState == GameState.Playing && RecipeManager.instance.IsComplete)
        {
            GameManager.instance.GameFinished();
            GameOverObject.SetActive(true);
            GameManager.instance.Player.GetComponent<MovementController>().enabled = false;
            GameManager.instance.Player.GetComponentInChildren<LookController>().enabled = false;
        }
    }

    public void ToggleEsc(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (GameManager.instance.CurrentState == GameState.Paused)
            {
                GameManager.instance.Player.GetComponent<MovementController>().enabled = false;
                GameManager.instance.Player.GetComponentInChildren<LookController>().enabled = false;
            }
            else
            {
                GameManager.instance.Player.GetComponent<MovementController>().enabled = true;
                GameManager.instance.Player.GetComponentInChildren<LookController>().enabled = true;
            }
        }
    }
}
