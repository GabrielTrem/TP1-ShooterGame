using UnityEngine;
using System.Collections.Generic;
using JetBrains.Annotations;

public class CollectibleManager : MonoBehaviour
{
    [SerializeField] GameObject healingCollectiblePrefab;
    [SerializeField] GameObject missileCollectiblePrefab;
    [SerializeField] GameObject tripleShotCollectiblePrefab;
    [SerializeField] int dropRate = 15;
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

            GameObject newMissileCollectible = Instantiate(missileCollectiblePrefab, Vector3.zero, Quaternion.identity);
            newMissileCollectible.SetActive(false);
            missileCollectibles.Add(newMissileCollectible);

            GameObject newTripleShotCollectible = Instantiate(tripleShotCollectiblePrefab, Vector3.zero, Quaternion.identity);
            newTripleShotCollectible.SetActive(false);
            tripleShotCollectibles.Add(newTripleShotCollectible);
        }
    }

    public void DropCollectible(Vector3 dropPosition)
    {
        int roll = Random.Range(1, dropRate + 1);
        if(roll == 1)
        {
            GameObject collectible = null;

            int collectibleType = Random.Range(0, 3);

            switch (collectibleType)
            {
                case 0:
                    collectible = GetAvailableHealingCollectible();
                    break;
                case 1:
                    collectible = GetAvailableMissileCollectible();
                    break;
                case 2:
                    collectible = GetAvailableTripleShotCollectible();
                    break;
            }

            collectible.transform.position = dropPosition;
            collectible.SetActive(true);
        }
    }

    private GameObject GetAvailableHealingCollectible()
    {
        foreach (GameObject healingCollectible in healingCollectibles)
        {
            if (!healingCollectible.activeSelf)
            {
                return healingCollectible;
            }
        }
        GameObject newHealingCollectible = Instantiate(healingCollectiblePrefab, Vector3.zero, Quaternion.identity);
        newHealingCollectible.SetActive(true);
        healingCollectibles.Add(newHealingCollectible);
        return newHealingCollectible;
    }

    private GameObject GetAvailableMissileCollectible()
    {
        foreach (GameObject missileCollectible in missileCollectibles)
        {
            if (!missileCollectible.activeSelf)
            {
                return missileCollectible;
            }
        }
        GameObject newMissileCollectible = Instantiate(missileCollectiblePrefab, Vector3.zero, Quaternion.identity);
        newMissileCollectible.SetActive(true);
        missileCollectibles.Add(newMissileCollectible);
        return newMissileCollectible;
    }

    private GameObject GetAvailableTripleShotCollectible()
    {
        foreach (GameObject tripleShotCollectible in tripleShotCollectibles)
        {
            if (!tripleShotCollectible.activeSelf)
            {
                return tripleShotCollectible;
            }
        }
        GameObject newTripleShotCollectible = Instantiate(tripleShotCollectiblePrefab, Vector3.zero, Quaternion.identity);
        newTripleShotCollectible.SetActive(true);
        tripleShotCollectibles.Add(newTripleShotCollectible);
        return newTripleShotCollectible;
    }
}
