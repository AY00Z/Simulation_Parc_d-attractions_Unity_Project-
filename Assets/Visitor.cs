using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// A park visitor: picks a POI, walks to its entrance, waits if it is full,
/// visits it, comes out through the exit, then picks another POI.
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class Visitor : MonoBehaviour
{
    private enum State { Idle, Walking, Waiting, Visiting }

    [Tooltip("Distance to the entrance at which the visitor counts as arrived.")]
    [SerializeField] private float arrivalDistance = 1.5f;

    private NavMeshAgent agent;
    private Renderer[] renderers;
    private Collider[] colliders;

    private State state = State.Idle;
    private POI target;
    private POI lastVisited;
    private float visitEndTime;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        renderers = GetComponentsInChildren<Renderer>();
        colliders = GetComponentsInChildren<Collider>();
    }

    private void Start() => GoToNextPOI();

    private void Update()
    {
        switch (state)
        {
            case State.Idle:
                GoToNextPOI(); // retries until a POI exists
                break;

            case State.Walking:
                if (HasArrived()) state = State.Waiting;
                break;

            case State.Waiting:
                if (target.TryEnter(this)) EnterPOI();
                break;

            case State.Visiting:
                if (Time.time >= visitEndTime) ExitPOI();
                break;
        }
    }

    private void GoToNextPOI()
    {
        target = PickRandomPOI();
        if (target == null)
        {
            state = State.Idle;
            return;
        }

        agent.SetDestination(target.EntrancePosition);
        state = State.Walking;
    }

    // Random POI, avoiding the one just visited when there is a choice.
    private POI PickRandomPOI()
    {
        int count = POI.All.Count;
        if (count == 0) return null;
        if (count == 1) return POI.All[0];

        POI choice;
        do { choice = POI.All[Random.Range(0, count)]; }
        while (choice == lastVisited);
        return choice;
    }

    private bool HasArrived()
    {
        return !agent.pathPending && agent.remainingDistance <= arrivalDistance;
    }

    private void EnterPOI()
    {
        // The visitor is "inside": hidden and removed from navigation so it doesn't block others.
        agent.enabled = false;
        SetPresent(false);
        visitEndTime = Time.time + target.visitTime;
        state = State.Visiting;
    }

    private void ExitPOI()
    {
        target.Leave(this);

        // Come out at the closest walkable point to the exit.
        Vector3 exitPos = target.ExitPosition;
        if (NavMesh.SamplePosition(exitPos, out NavMeshHit hit, 5f, NavMesh.AllAreas))
            exitPos = hit.position;

        transform.position = exitPos;
        agent.enabled = true;
        agent.Warp(exitPos);
        SetPresent(true);

        lastVisited = target;
        GoToNextPOI();
    }

    private void SetPresent(bool present)
    {
        foreach (Renderer r in renderers) r.enabled = present;
        foreach (Collider c in colliders) c.enabled = present;
    }
}