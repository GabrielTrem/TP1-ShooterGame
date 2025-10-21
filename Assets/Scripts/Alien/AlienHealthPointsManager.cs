using UnityEngine;

public class AlienHealthPointsManager : MonoBehaviour
{
    [SerializeField] private int healthPoints = 1;
    [SerializeField] private AudioClip alienDeathSound;

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
        if (isDead)
        {
            return;
        }

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
        if (isDead)
        {
            return;
        }

        currentHealthPoints -= damage;

        if (currentHealthPoints <= 0)
        {
            isDead = true;

            //Demandé de l'aide à ChatGPT pour ici, je n'arrivais pas à faire jouer le son de l'alien. (Mathieu)
            if (alienDeathSound != null)
            {
                AudioListener listener = Object.FindFirstObjectByType<AudioListener>();
                if (listener != null)
                {
                    AudioSource.PlayClipAtPoint(alienDeathSound, listener.transform.position, 10.0f);
                }
                else
                {
                    AudioSource.PlayClipAtPoint(alienDeathSound, transform.position, 10.0f);
                }
            }

            if (alienController != null)
            {
                alienController.Die();
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}