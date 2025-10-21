using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text[] uiTexts = new TMP_Text[4];

    private string gameOverText = "GAME OVER";

    private void Start()
    {
        uiTexts[3].text = "";
    }

    public void GameOver()
    {
        uiTexts[3].text = gameOverText;
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
