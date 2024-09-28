using UnityEngine;
using UnityEngine.AI;

public class FakePathing : MonoBehaviour
{
    public Transform[] waypoints; 
    public Transform player; 
    public float chaseRange = 5f; 

    private int currentWaypointIndex = 0;
    private NavMeshAgent agent;
    private Vector3 initialPosition;
    private bool isChasing = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        initialPosition = transform.position;

        if (waypoints.Length > 0)
        {
            agent.SetDestination(waypoints[currentWaypointIndex].position);
        }
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= chaseRange)
        {
            isChasing = true;
            agent.SetDestination(player.position);
        }
        else if (isChasing && distanceToPlayer <= chaseRange * 2)
        {
            agent.SetDestination(player.position);
        }
        else if (isChasing && distanceToPlayer > chaseRange * 2)
        {
            isChasing = false;
            GoToNextWaypoint();
        }
        else if (!isChasing && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            GoToNextWaypoint();
        }

        if (distanceToPlayer <= agent.stoppingDistance)
        {
            Debug.Log("game over");
        }
    }

    void GoToNextWaypoint()
    {
        if (waypoints.Length == 0)
            return;

        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        agent.SetDestination(waypoints[currentWaypointIndex].position);
    }
}
/*
 * resources used *
 * https://www.youtube.com/watch?v=UjkSFoLxesw&ab_channel=Dave%2FGameDevelopment
 * https://medium.com/@dnwesdman/modular-stealth-enemy-ai-in-unity-3133327d48c
 */