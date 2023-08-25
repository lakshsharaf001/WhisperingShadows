using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class MonsterMovement : MonoBehaviour
{
    public Transform[] waypoints;
    public Transform player;
    public float chaseDistance = 10f;
    public string gameOverScene = "GameOver";
    public float waypointMoveSpeed = 3f; // Movement speed while patrolling waypoints
    public float chaseMoveSpeed = 6f;    // Movement speed while chasing the player
    private int currentWaypointIndex = 0;
    private NavMeshAgent agent;

    private enum MonsterState { Waypoint, Chase }
    private MonsterState currentState = MonsterState.Waypoint;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = waypointMoveSpeed; // Set the initial movement speed
        MoveToRandomWaypoint();
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            if (currentState == MonsterState.Waypoint)
            {
                MoveToRandomWaypoint();
            }
            else if (currentState == MonsterState.Chase)
            {
                ChasePlayer();
            }
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer < chaseDistance)
        {
            currentState = MonsterState.Chase;
            agent.speed = chaseMoveSpeed; // Change to chase movement speed
        }
        else
        {
            currentState = MonsterState.Waypoint;
            agent.speed = waypointMoveSpeed; // Change to waypoint movement speed
        }
    }

    void MoveToRandomWaypoint()
    {
        if (waypoints.Length == 0)
        {
            Debug.LogWarning("No waypoints assigned.");
            return;
        }

        int randomIndex = Random.Range(0, waypoints.Length);
        currentWaypointIndex = randomIndex;
        agent.SetDestination(waypoints[currentWaypointIndex].position);
    }

    void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(gameOverScene);
        }
    }
}

