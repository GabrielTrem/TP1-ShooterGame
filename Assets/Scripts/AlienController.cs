using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AlienController : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private GameObject player;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnEnable()
    {
        if (navMeshAgent != null)
        {
            navMeshAgent.enabled = false;
        }

        StartCoroutine(MoveDownToGround());
    }

    IEnumerator MoveDownToGround()
    {
        // Demandé de l'aide à ChatGPT pour ici. (Mathieu)
        Vector3 positionSol = new Vector3(transform.position.x, -74.47f, transform.position.z);

        while (Vector3.Distance(transform.position, positionSol) > 2f)
        {
            transform.position = Vector3.MoveTowards(transform.position, positionSol, 2f * Time.deltaTime);
            yield return null;
        }

        if (navMeshAgent != null)
        {
            navMeshAgent.enabled = true;
        }
    }

    void Update()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);

            if (distance < 6f)
            {
                HealthPointsManager healthPointsManager = player.GetComponent<HealthPointsManager>();
                if (healthPointsManager != null)
                {
                    healthPointsManager.LoseHealthPoint();
                }

                Die();
            }
        }
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }
}