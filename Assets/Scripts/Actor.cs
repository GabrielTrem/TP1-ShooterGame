using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Actor : MonoBehaviour
{
    [SerializeField] GameObject goal;
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (goal == null)
        {
            goal = GameObject.FindGameObjectWithTag("Player");
        }
    }

    void Update()
    {
        if (goal == null)
        {
            goal = GameObject.FindGameObjectWithTag("Player");
            return;
        }

        if (navMeshAgent != null && navMeshAgent.enabled && navMeshAgent.isOnNavMesh)
        {
            navMeshAgent.SetDestination(goal.transform.position);
        }
    }

}
