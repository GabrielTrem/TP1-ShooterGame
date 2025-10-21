using System;
using UnityEngine;

public class HealthPointsManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private int healthPoints = 5;
    [SerializeField] private float invincibilityPeriod = 0.5f;
    private CharacterController characterController;

    private int currentHealthPoints;
    private float invincibilityPeriodTimeLeft;
    private bool isInvincible;

    void Awake()
    {
        currentHealthPoints = healthPoints;
        invincibilityPeriodTimeLeft = 0f;
        isInvincible = false;
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
                //LoseHealthPoint();
            }
        }
    }

    public void LoseHealthPoint()
    {
        currentHealthPoints--;
        isInvincible = true;
        invincibilityPeriodTimeLeft = invincibilityPeriod;
        gameManager.UpdateHealthPoints(currentHealthPoints);
    }

    public void GainHealthPoint()
    {
        currentHealthPoints++;
        gameManager.UpdateHealthPoints(currentHealthPoints);
    }
}
