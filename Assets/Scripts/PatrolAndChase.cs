using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))] // Ensure AudioSource component is present
public class PatrolAndChase : MonoBehaviour
{
    public Transform[] waypoints; // Fixed waypoints for the monster to travel
    public float chaseSpeed = 15.0f;
    public float detectionRadius = 10f;
    public Transform playerTransform; // Reference to the player's transform

    public AudioSource audioSource; // Reference to the AudioSource component

    private NavMeshAgent navMeshAgent;
    private bool isChasing = false;
    private int currentWaypointIndex = 0; // Index of the current waypoint

    public float slowSpeed = 5.0f; // Adjust the value to your desired slower speed

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>(); // Assign the AudioSource component

        // Start movement towards the first waypoint
        SetAgentSpeedAndDestination(currentWaypointIndex);
    }

    private void Update()
    {
        if (!isChasing)
        {
            CheckForPlayer();
        }
        else
        {
            ChasePlayer();
        }

        // Calculate distance between monster and player
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        // Adjust audio volume based on distance
        float maxDistance = detectionRadius * 2.0f; // Adjust as needed
        float volume = Mathf.Lerp(0.0f, 1.0f, 1.0f - (distanceToPlayer / maxDistance));
        audioSource.volume = volume;

        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && !navMeshAgent.pathPending)
        {
            MoveToNextWaypoint();
        }
    }

    private void SetAgentSpeedAndDestination(int waypointIndex)
    {
        navMeshAgent.speed = isChasing ? chaseSpeed : slowSpeed;
        navMeshAgent.destination = waypoints[waypointIndex].position;
    }

    private void CheckForPlayer()
    {
        if (playerTransform != null && Vector3.Distance(transform.position, playerTransform.position) <= detectionRadius)
        {
            Debug.Log("Player detected, starting chase.");
            isChasing = true;
        }
    }

    private void ChasePlayer()
    {
        if (playerTransform != null)
        {
            navMeshAgent.destination = playerTransform.position;
            navMeshAgent.speed = chaseSpeed;

            if (Vector3.Distance(transform.position, playerTransform.position) < 1.0f)
            {
                Debug.Log("Player caught, loading GameOver scene.");
                SceneManager.LoadScene("GameOver");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player caught, loading GameOver scene.");
            SceneManager.LoadScene("GameOver");
        }
    }

    private void MoveToNextWaypoint()
    {
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        SetAgentSpeedAndDestination(currentWaypointIndex);
    }
}
















