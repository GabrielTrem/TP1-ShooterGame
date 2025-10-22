using UnityEngine;

public class HealthPointsManager : MonoBehaviour
{
    [SerializeField] private int healthPoints = 1;
    [SerializeField] private AudioClip alienDeathSound;
    [SerializeField] private CollectibleManager collectibleManager;

    private int currentHealthPoints;
    private bool isDead = false;

    void OnEnable()
    {
        currentHealthPoints = healthPoints;
        isDead = false;
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Projectile"))
        {
            TakeDamage(other.gameObject.GetComponentInParent<Projectile>().GetDamage());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDead)
        {
            return;
        }

        if (other.gameObject.CompareTag("Projectile"))
        {
            TakeDamage(other.gameObject.GetComponentInParent<Projectile>().GetDamage());
        }

        if (other.CompareTag("Player") && gameObject.CompareTag("Alien"))
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
            gameObject.SetActive(false);
            if (CompareTag("Alien"))
            {
                collectibleManager.DropCollectible(transform.position);
            }
        }
    }
}