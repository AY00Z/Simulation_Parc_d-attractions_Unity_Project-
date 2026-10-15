using UnityEngine;
using UnityEngine.AI;

public class Visitor : MonoBehaviour
{
    public Transform destination;

    void Start()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();

        agent.SetDestination(destination.position);
        //agent.destination = destination.position;
    }
}