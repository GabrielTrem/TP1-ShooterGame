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

        //Si on a un agent, alors on donne tout de suite la destination.
        //Dès qu'un agent a une destination il se déplace vers elle
        //Sinon il ne bouge pas.
        if (navMeshAgent != null)
            navMeshAgent.destination = goal.transform.position;
    }

    void Update()
    {
        //Par comparaison, si on a pas d'agent, on se d�place en ligne droite vers la cible... et ça marche pas super.
        if (navMeshAgent == null)
            transform.position = Vector3.MoveTowards(transform.position, goal.transform.position, 5f * Time.deltaTime);

        //Notez que dans le update ou dans une méthode appellée dans ce même update, on peut changer la destination par
        //navMeshAgent.destination = [Un nouvel objet quelconque].transform.position;
        //
        //Ou alors on peut changer son mode de déplacement selon nos besoins
        //navMeshAgent.destination = null;
        //[Autre moyen de déplacement scripté qui n'implique pas le navmesh]

    }
}
