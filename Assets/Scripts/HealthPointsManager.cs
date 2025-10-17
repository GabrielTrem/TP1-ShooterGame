using UnityEngine;

public class HealthPointsManager : MonoBehaviour
{
    [SerializeField] private int healthPoints = 5;
    [SerializeField] private float invincibilityPeriod = 0.5f;
    private int currentHealthPoints;
    private float invincibilityPeriodTimeLeft;
    private bool isInvincible;
    void Awake()
    {
        currentHealthPoints = healthPoints;
        invincibilityPeriodTimeLeft = 0f;
        isInvincible = false;
    }

    private void Update()
    {
        if(invincibilityPeriodTimeLeft > 0)
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
        if (hit.gameObject.CompareTag("Alien") && !isInvincible)
        {
            LoseHealthPoint();
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
    }

    public void GainHealthPoint()
    {
        currentHealthPoints++;
    }
}
