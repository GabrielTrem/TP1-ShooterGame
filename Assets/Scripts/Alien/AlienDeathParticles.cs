using UnityEngine;

public class AlienDeathParticles : MonoBehaviour
{
    [SerializeField] private ParticleSystem deathParticles;

    public void PlayDeathParticles()
    {
        deathParticles.Play();
        gameObject.SetActive(false);
    }
}
