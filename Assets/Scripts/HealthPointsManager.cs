using UnityEngine;

public class HealthPointsManager : MonoBehaviour
{
    [SerializeField] private int healthPoints = 5;
    [SerializeField] private float invincibilityPeriod = 0.5f;
    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip deathSound;

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

    private void Update()
    {
        if (invincibilityPeriodTimeLeft > 0)
        {
            invincibilityPeriodTimeLeft -= Time.deltaTime;
        }
        else
        {
            isInvincible = false;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Alien"))
        {
            AlienController alienController = hit.gameObject.GetComponent<AlienController>();
            if (alienController != null)
            {
                alienController.Die();
            }

            if (!isInvincible)
            {
                LoseHealthPoint();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Alien"))
        {
            LoseHealthPoint();
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
    }

    public void GainHealthPoint()
    {
        currentHealthPoints++;
    }
}