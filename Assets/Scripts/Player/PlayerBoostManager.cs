using UnityEngine;

public class PlayerBoostManager : MonoBehaviour
{
    [SerializeField] private int nbOfMissilesGainedFromCollectible = 5;
    [SerializeField] private float amountOfTimeTripleShotCollectible = 10.0f;
    private ProjectileManager projectileManager;
    private HealthPointsManager healthPointsManager;
    void Start()
    {
        projectileManager = GetComponentInChildren<ProjectileManager>();
        healthPointsManager = GetComponent<HealthPointsManager>();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Collectible"))
        {
            Collectible collectible = hit.gameObject.GetComponent<Collectible>();
            CollectibleType collectibleType = collectible.GetCollectibleType();

            if (collectibleType == CollectibleType.HEALTH)
            {
                healthPointsManager.GainHealthPoint();
            }
            else if (collectibleType == CollectibleType.AMMO)
            {
                projectileManager.GainMissiles(nbOfMissilesGainedFromCollectible);
            }
            else if(collectibleType == CollectibleType.SHOOTING_BOOST)
            {
                projectileManager.ActivateTripleShotMode(amountOfTimeTripleShotCollectible);
            }

            hit.gameObject.SetActive(false);
        }
    }
}
