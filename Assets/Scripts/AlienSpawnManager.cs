using System.Collections;
using UnityEngine;

public class AlienSpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject alienPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private int maxAlienInGame = 50;
    [SerializeField] private int maxAliensSpawned = 500;
    [SerializeField] private float timeBetweenSpawns = 2f;
    [SerializeField] private GameManager gameManager;

    private GameObject[] aliensPool;
    private int totalSpawned = 0;

    void Start()
    {
        aliensPool = new GameObject[maxAlienInGame];

        for (int i = 0; i < maxAlienInGame; i++)
        {
            aliensPool[i] = Instantiate(alienPrefab);
            aliensPool[i].SetActive(false);
        }

        StartCoroutine(SpawnAliens());
    }

    IEnumerator SpawnAliens()
    {
        while (totalSpawned < maxAliensSpawned)
        {
            yield return new WaitForSeconds(timeBetweenSpawns);
            SpawnOneAlien();
        }
    }

    private void SpawnOneAlien()
    {
        for (int i = 0; i < aliensPool.Length; i++)
        {
            if (!aliensPool[i].activeSelf)
            {
                Transform spawner = GetRandomActiveSpawner();

                if (spawner != null)
                {
                    aliensPool[i].transform.position = spawner.position;
                    aliensPool[i].SetActive(true);
                    totalSpawned++;
                }
                else
                {
                    gameManager.Win();
                    foreach (var alien in aliensPool)
                    {
                        alien.SetActive(false);
                    }
                }
                return;
            }
        }
    }

    //Demandé de l'aide à ChatGPT pour ici. (Mathieu)
    private Transform GetRandomActiveSpawner()
    {
        int activeCount = 0;
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (spawnPoints[i].gameObject.activeSelf)
            {
                activeCount++;
            }
        }

        if (activeCount == 0)
        {
            return null;
        }

        int randomIndex = Random.Range(0, activeCount);
        int currentIndex = 0;

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (spawnPoints[i].gameObject.activeSelf)
            {
                if (currentIndex == randomIndex)
                {
                    return spawnPoints[i];
                }

                currentIndex++;
            }
        }

        return null;
    }
}