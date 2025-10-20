using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AlienController : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
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
            transform.position = Vector3.MoveTowards(transform.position, positionSol, 10f * Time.deltaTime);
            yield return null;
        }

        if (navMeshAgent != null)
        {
            navMeshAgent.enabled = true;
        }
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }
}