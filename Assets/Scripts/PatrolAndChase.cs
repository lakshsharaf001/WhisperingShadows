using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class PatrolAndChase : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float patrolSpeed = 10.0f;
    public float chaseSpeed = 15.0f;
    public float detectionRadius = 10f;
    public Transform playerTransform; // Reference to the player's transform
    public Animator monsterMove;

    private NavMeshAgent navMeshAgent;
    private int currentPatrolIndex = 0;
    private bool isChasing = false;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        // Start patrolling
        StartPatrol();
        monsterMove.SetTrigger("Walk");
    }

    private void Update()
    {
        if (!isChasing)
        {
            Patrol();
            CheckForPlayer();
        }
        else
        {
            ChasePlayer();
        }
    }

    private void StartPatrol()
    {
        navMeshAgent.speed = patrolSpeed;
        navMeshAgent.destination = patrolPoints[currentPatrolIndex].position;
    }

    private void Patrol()
    {
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            navMeshAgent.destination = patrolPoints[currentPatrolIndex].position;
        }
    }

    private void CheckForPlayer()
    {
        if (playerTransform != null && Vector3.Distance(transform.position, playerTransform.position) <= detectionRadius)
        {
            Debug.Log("Player detected, starting chase.");
            isChasing = true;
            navMeshAgent.speed = chaseSpeed;
        }
    }

    private void ChasePlayer()
    {
        if (playerTransform != null)
        {
            navMeshAgent.destination = playerTransform.position;

            if (Vector3.Distance(transform.position, playerTransform.position) < 1.0f)
            {
                Debug.Log("Player caught, loading GameOver scene.");
                SceneManager.LoadScene("GameOver");
            }
        }
    }
}











