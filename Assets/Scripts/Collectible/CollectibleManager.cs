using UnityEngine;
using System.Collections.Generic;

public class CollectibleManager : MonoBehaviour
{
    [SerializeField] GameObject healingCollectiblePrefab;
    [SerializeField] GameObject missileCollectiblePrefab;
    [SerializeField] GameObject tripleShotCollectiblePrefab;
    const int NB_OF_COLLECTIBLES = 30;

    private List<GameObject> healingCollectibles;
    private List<GameObject> missileCollectibles;
    private List<GameObject> tripleShotCollectibles;

    void Awake()
    {
        healingCollectibles = new List<GameObject>();
        missileCollectibles = new List<GameObject>();
        tripleShotCollectibles = new List<GameObject>();
    }

    void Start()
    {
        for (int i = 0; i < NB_OF_COLLECTIBLES; i++)
        {
            GameObject newHealingCollectible = Instantiate(healingCollectiblePrefab, Vector3.zero, Quaternion.identity);
            newHealingCollectible.SetActive(false);
            healingCollectibles.Add(newHealingCollectible);
        }
        for (int i = 0; i < NB_OF_COLLECTIBLES; i++)
        {
            GameObject newMissileCollectible = Instantiate(missileCollectiblePrefab, Vector3.zero, Quaternion.identity);
            newMissileCollectible.SetActive(false);
            missileCollectibles.Add(newMissileCollectible);
        }
        for (int i = 0; i < NB_OF_COLLECTIBLES; i++)
        {
            GameObject newTripleShotCollectible = Instantiate(tripleShotCollectiblePrefab, Vector3.zero, Quaternion.identity);
            newTripleShotCollectible.SetActive(false);
            tripleShotCollectibles.Add(newTripleShotCollectible);
        }
    }

    void Update()
    {
        
    }
}
