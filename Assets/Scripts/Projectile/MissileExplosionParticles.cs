using System.Collections;
using UnityEngine;

public class MissileExplosion : MonoBehaviour
{
    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private GameObject missileBody;
    private bool isExploding;

    private void Awake()
    {
        isExploding = false;
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
            isExploding = true;
            missileBody.SetActive(false);
            StartCoroutine(PlayExplosionAndDisable());
        }
    }

    private IEnumerator PlayExplosionAndDisable()
    {
        explosion.Play();

        yield return new WaitForSeconds(explosion.main.duration);

        gameObject.SetActive(false);
    }
}
