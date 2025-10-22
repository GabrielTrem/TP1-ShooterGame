using System.Collections;
using UnityEngine;

public class MissileExplosion : MonoBehaviour
{
    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private GameObject missileBody;
    private SphereCollider sphereCollider;
    private bool isExploding;
    private int damage;

    private void Awake()
    {
        isExploding = false;
    }

    private void Start()
    {
        sphereCollider = explosion.gameObject.GetComponent<SphereCollider>();
        damage = GetComponentInParent<Projectile>().GetDamage();
    }

    private void OnEnable()
    {
        isExploding = false;
        missileBody.SetActive(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isExploding)
        {
            gameObject.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            sphereCollider.enabled = true;
            isExploding = true;
            missileBody.SetActive(false);
            StartCoroutine(PlayExplosionParticules());
        }
    }

    private IEnumerator PlayExplosionParticules()
    {
        explosion.Play();

        yield return new WaitForSeconds(explosion.main.duration);

        sphereCollider.enabled = false;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if((other.gameObject.CompareTag("Alien") || other.gameObject.CompareTag("AlienSpawn")) && isExploding)
        {
            other.gameObject.GetComponent<HealthPointsManager>().TakeDamage(damage);
        }
    }
}
