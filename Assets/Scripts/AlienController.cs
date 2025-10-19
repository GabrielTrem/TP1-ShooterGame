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
        StartCoroutine(MoveDownToGround());
    }

    private IEnumerator MoveDownToGround()
    {
        // Demandé de l'aide à ChatGPT pour ici. (Mathieu)
        Vector3 positionSol = new Vector3(transform.position.x, -74.47f, transform.position.z);

        while (Vector3.Distance(transform.position, positionSol) > 2f)
        {
            transform.position = Vector3.MoveTowards(transform.position, positionSol, 2f * Time.deltaTime);
            yield return null;
        }

        navMeshAgent.enabled = true;
        navMeshAgent.Warp(transform.position);
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }
}
