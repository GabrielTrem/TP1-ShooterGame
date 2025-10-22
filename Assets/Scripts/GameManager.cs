using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text[] uiTexts = new TMP_Text[5];

    private string gameOverText = "GAME OVER";
    private string winText = "CONGRATULATIONS! YOU WON!";

    private InputAction exitGameAction;

    private void Start()
    {
        uiTexts[3].text = "";
        uiTexts[4].text = "";
        exitGameAction = InputSystem.actions.FindAction("ExitGame");
    }

    private void Update()
    {
        if (exitGameAction.WasPerformedThisFrame())
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
    }

    public void GameOver()
    {
        uiTexts[3].text = gameOverText;
    }

    public void Win()
    {
        uiTexts[4].text = winText;
    }

    public void UpdateHealthPoints(int healthPoints) 
    {
        uiTexts[0].text = ": " + healthPoints.ToString();
    }

    public void UpdateNbOfMissiles(int nbOfMissiles)
    {
        uiTexts[1].text = ": " + nbOfMissiles.ToString();
    }

    public void UpdateTripleShotTimeRemaining(float timeRemaining)
    {
        uiTexts[2].text = ": " + timeRemaining.ToString();
    }
}
