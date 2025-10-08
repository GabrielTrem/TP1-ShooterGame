using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AlienController : MonoBehaviour
{
    [SerializeField] private int pointsDeVie = 1;

    private NavMeshAgent navMeshAgent;
    private Rigidbody rigidBody;
    private Actor actorScript;

    void Start()
    {
 
        navMeshAgent = GetComponent<NavMeshAgent>();
        rigidBody = GetComponent<Rigidbody>();
        actorScript = GetComponent<Actor>();
    }

 
    void OnEnable()
    {
 
        pointsDeVie = 1;

 
        if (navMeshAgent != null)
            navMeshAgent.enabled = false;

        // Désactiver temporairement le script Actor
        if (actorScript != null)
            actorScript.enabled = false;

        // Lancer la descente
        StartCoroutine(DescendreVersSol());
    }

    IEnumerator DescendreVersSol()
    {
        // Position au sol (ajuste le Y selon ton plancher)
        Vector3 positionSol = new Vector3(transform.position.x, 0f, transform.position.z);

        // Descente progressive
        while (Vector3.Distance(transform.position, positionSol) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                positionSol,
                2f * Time.deltaTime
            );
            yield return null; // Attend la prochaine frame
        }

        // Une fois au sol:
        // 1. Activer le NavMesh
        if (navMeshAgent != null)
            navMeshAgent.enabled = true;

        // 2. Activer le script Actor pour qu'il gère la navigation
        if (actorScript != null)
            actorScript.enabled = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Détecter collision avec le joueur (utilise les tags!)
        if (collision.gameObject.CompareTag("Player"))
        {
            pointsDeVie = 0; // Perd tous ses points de vie
            Mourir();
        }
    }

    public void PrendreDegats(int degats)
    {
        pointsDeVie -= degats;
        if (pointsDeVie <= 0)
            Mourir();
    }

    private void Mourir()
    {
        // Recyclage: désactiver au lieu de Destroy
        gameObject.SetActive(false);

        // Tout sera réinitialisé par OnEnable au prochain spawn
    }
}