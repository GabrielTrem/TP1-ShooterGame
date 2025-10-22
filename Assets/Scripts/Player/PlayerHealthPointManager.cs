using System;
using UnityEngine;

public class PlayerHealthPointsManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private int healthPoints = 5;
    [SerializeField] private float invincibilityPeriod = 0.5f;
    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip deathSound;
    private CharacterController characterController;

    private int currentHealthPoints;
    private float invincibilityPeriodTimeLeft;
    private bool isInvincible;
    private AudioSource audioSource;

    void Awake()
    {
        currentHealthPoints = healthPoints;
        invincibilityPeriodTimeLeft = 0f;
        isInvincible = false;
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        if (gameManager != null) { 
            gameManager.UpdateHealthPoints(currentHealthPoints);
        }
        characterController = gameObject.GetComponent<CharacterController>();
    }

    void Update()
    {
        if (invincibilityPeriodTimeLeft > 0)
        {
            invincibilityPeriodTimeLeft -= Time.deltaTime;
        }
        else
        {
            isInvincible = false;
        }
        if(currentHealthPoints <= 0)
        {
            gameObject.SetActive(false);
            gameManager.GameOver();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Alien"))
        {
            if (!isInvincible && characterController.isGrounded)
            {
                LoseHealthPoint();
            }
        }
    }

    public void LoseHealthPoint()
    {
        currentHealthPoints--;
        isInvincible = true;
        invincibilityPeriodTimeLeft = invincibilityPeriod;

        if (currentHealthPoints <= 0)
        {
            if (audioSource != null && deathSound != null)
            {
                audioSource.PlayOneShot(deathSound, 1.0f);
            }
        }
        else
        {
            if (audioSource != null && hurtSound != null)
            {
                audioSource.PlayOneShot(hurtSound, 1.0f);
            }
        }
        gameManager.UpdateHealthPoints(currentHealthPoints);
    }

    public void GainHealthPoint()
    {
        currentHealthPoints++;
        gameManager.UpdateHealthPoints(currentHealthPoints);
    }
}