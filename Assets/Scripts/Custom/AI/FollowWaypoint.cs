using UnityEngine;
using UnityEngine.AI;

using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class FollowWaypoint : MonoBehaviour
{
    [SerializeField] private WaypointSystem WPS;
    private Transform[] waypoints;

    private Transform target;

    private NavMeshAgent agent;
    private Animator animator;

    private int index = 1;

    private bool reverse = false;
    private bool end = false;
    private bool moving = true;

    private void Awake() => waypoints = WPS.waypoints.ToArray();

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        if (agent == null)
            Debug.LogError("No attached NavMeshAgent found!");        
        
        if (animator == null)
            Debug.LogError("No attached Animator found!");

        if (waypoints.Length > 0 && waypoints[0] != null)
        {
            // Set target to the first element in waypoints
            target = waypoints[index];

            // Move NavMeshAgent towards target
            agent.SetDestination(target.position);
        }
    }

    private IEnumerator MoveToNextWaypoint()
    {
        if (!reverse)
            index++;
        
        if (index < waypoints.Length && !reverse)
        {
            if (index == 1)
                yield return new WaitForSeconds(Random.Range(3.0f, 5.0f));

            target = waypoints[index];   
        }
        else
        {
            if (!end)
            {
                end = true;
                yield return new WaitForSeconds(Random.Range(3.0f, 5.0f));
            }

            index--;
            reverse = true;

            if (index == 0)
            {
                reverse = false;
                end = false;
            }
            
            target = waypoints[index];
        }

        agent.SetDestination(target.position);
        moving = true;
    }

    private void Update()
    {
        animator.SetFloat("Blend", agent.velocity.magnitude / agent.speed);

        if (target != null)
        {
            if (Vector3.Distance(transform.position, target.position) <= 2.0f && moving)
            {
                moving = false;
                StartCoroutine(MoveToNextWaypoint());
            }
        }
    }
}
