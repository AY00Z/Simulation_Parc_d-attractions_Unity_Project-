using System.Collections.Generic;
using UnityEngine;

public class POI : MonoBehaviour
{
    public static readonly List<POI> All = new List<POI>();

    [Header("Visit")]
    [Min(0f)] public float visitTime = 5f;
    [Min(1)] public int maxVisitors = 3;

    [Header("Access points")]
    public Transform entrance;
    public Transform exit;

    private readonly HashSet<Visitor> visitorsInside = new HashSet<Visitor>();

    public int VisitorsInside => visitorsInside.Count;
    public bool IsFull => visitorsInside.Count >= maxVisitors;

    public Vector3 EntrancePosition => entrance != null ? entrance.position : transform.position;
    public Vector3 ExitPosition => exit != null ? exit.position : transform.position;

    private void OnEnable() => All.Add(this);
    private void OnDisable() => All.Remove(this);

    public bool TryEnter(Visitor visitor)
    {
        if (IsFull) return false;
        visitorsInside.Add(visitor);
        return true;
    }

    public void Leave(Visitor visitor) => visitorsInside.Remove(visitor);

    private void OnDrawGizmos()
    {
        if (entrance != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(entrance.position, 0.5f);
            Gizmos.DrawLine(transform.position, entrance.position);
        }
        if (exit != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(exit.position, 0.5f);
            Gizmos.DrawLine(transform.position, exit.position);
        }
    }
}