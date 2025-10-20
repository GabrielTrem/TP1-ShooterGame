using UnityEngine;

public class AlienHealthPointsManager : MonoBehaviour
{
    [SerializeField] private int healthPoints = 1;

    private int currentHealthPoints;
    private AlienController alienController;
    private bool isDead = false;

    void Start()
    {
        alienController = GetComponent<AlienController>();
    }

    void OnEnable()
    {
        currentHealthPoints = healthPoints;
        isDead = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDead) return;

        if (other.CompareTag("Bullet"))
        {
            if (other != null && other.gameObject != null)
            {
                other.gameObject.SetActive(false);
                TakeDamage(1);
            }
        }

        if (other.CompareTag("Player"))
        {
            TakeDamage(healthPoints);
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealthPoints -= damage;

        if (currentHealthPoints <= 0)
        {
            isDead = true;

            if (alienController != null)
            {
                alienController.Die();
            }
        }
    }
}