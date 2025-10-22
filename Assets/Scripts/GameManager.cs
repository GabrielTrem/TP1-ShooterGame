using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text[] uiTexts = new TMP_Text[5];
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private AudioClip victoryMusic;
    [SerializeField] private float fadeOutDuration = 3f;

    private string gameOverText = "GAME OVER";
    private string winText = "CONGRATULATIONS! YOU WON!";
    private InputAction exitGameAction;
    private AudioSource audioSource;
    private bool isGameOver;
    private float initialMusicVolume;
    private Coroutine fadeCoroutine;

    private void Awake()
    {
        isGameOver = false;
        fadeCoroutine = null;
    }

    private void Start()
    {
        uiTexts[3].text = "";
        uiTexts[4].text = "";
        exitGameAction = InputSystem.actions.FindAction("ExitGame");
        audioSource = GetComponent<AudioSource>();

        if (audioSource != null && backgroundMusic != null)
        {
            audioSource.clip = backgroundMusic;
            audioSource.loop = true;
            audioSource.Play();
            initialMusicVolume = audioSource.volume;
        }
    }

    private void Update()
    {
        if (exitGameAction?.WasPerformedThisFrame() == true)
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
        isGameOver = true;
        uiTexts[3].text = gameOverText;
    }

    public void Win()
    {
        if (isGameOver == false) isGameOver = true;
        uiTexts[4].text = winText;

        if (fadeCoroutine == null && audioSource != null)
        {
            fadeCoroutine = StartCoroutine(FadeOutAndPlayVictory());
        }
    }

    //Demander de l'aide à chatgpt pour le fade out(Gab)
    private IEnumerator FadeOutAndPlayVictory()
    {
        float elapsed = 0f;
        float startVol = audioSource.volume;
        initialMusicVolume = startVol;      

        while (elapsed < fadeOutDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / fadeOutDuration);
            audioSource.volume = Mathf.Lerp(startVol, 0f, t);
            yield return null;
        }

        audioSource.volume = 0f;
        audioSource.Stop();
        audioSource.volume = initialMusicVolume;

        if (victoryMusic != null)
        {
            audioSource.clip = victoryMusic;
            audioSource.loop = false;
            audioSource.Play();
        }
        fadeCoroutine = null;
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
