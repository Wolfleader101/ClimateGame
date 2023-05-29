using UnityEngine;
using System.Collections.Generic;

public class WaypointSystem : MonoBehaviour
{
    [Range(0.0f, 2.0f)]
    [SerializeField] private float size = 1.0f;

    [SerializeField] public List<Transform> waypoints = new List<Transform>();

    private void OnDrawGizmos()
    {
        foreach(Transform tf in transform)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(tf.position, size);

            if (!waypoints.Contains(tf))
                waypoints.Add(tf);
        }

        Gizmos.color = Color.red;

        for (int i = 0; i < transform.childCount - 1; ++i)
            Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i + 1).position);
    
        Gizmos.DrawLine(transform.GetChild(transform.childCount - 1).position, transform.GetChild(0).position);
    }
}
