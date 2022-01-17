using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Script di prova per la gestione dell'AI
public class AI : MonoBehaviour
{
    private NavMeshAgent agent; 

    public float radius;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //se non c'è un percorso settato definisci una destinazione
        if (!agent.hasPath)
        {
            agent.SetDestination(GetPoint.Instance.GetRandomPoint(transform, radius));
        }
    }

    //visualizza in editor l'area d'interesse
#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

#endif
}
