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

    [Range(0.0f, 5.0f), SerializeField]
    private float minWaitTime = 1.0f;
    [Range(0.0f, 5.0f), SerializeField]
    private float maxWaitTime = 2.0f;

    private int index = 1;

    private bool reverse = false;
    private bool end = false;
    private bool moving = true;

    private bool valid = false;

    private void Awake()
    {
        if(WPS != null) 
        {
            valid = true; waypoints = WPS.waypoints.ToArray();
        }
        else valid = false;
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        if (agent == null)
            Debug.LogError("No attached NavMeshAgent found!");        
        
        if (animator == null)
            Debug.LogError("No attached Animator found!");

        if (valid)
        {
            if (waypoints.Length > 0 && waypoints[0] != null)
            {
                // Set target to the first element in waypoints
                target = waypoints[index];

                // Move NavMeshAgent towards target
                agent.SetDestination(target.position);
            }
        }
    }

    private IEnumerator MoveToNextWaypoint()
    {
        if (!reverse)
            index++;
        
        if (index < waypoints.Length && !reverse)
        {
            if (index == 1)
                yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));

            target = waypoints[index];   
        }
        else
        {
            if (!end)
            {
                end = true;
                yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
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
        var blendAmount = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("Blend", Mathf.Clamp(blendAmount, 0.0f, 0.5f));

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
