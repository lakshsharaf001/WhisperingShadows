using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))] // Ensure AudioSource component is present
public class PatrolAndChase : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float patrolSpeed = 10.0f;
    public float chaseSpeed = 15.0f;
    public float detectionRadius = 10f;
    public Transform playerTransform; // Reference to the player's transform

    public AudioSource audioSource; // Reference to the AudioSource component

    private NavMeshAgent navMeshAgent;
    private bool isChasing = false;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>(); // Assign the AudioSource component

        // Start patrolling
        StartPatrol();
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
    }

    private void StartPatrol()
    {
        navMeshAgent.speed = patrolSpeed;
        int randomIndex = Random.Range(0, patrolPoints.Length);
        navMeshAgent.destination = patrolPoints[randomIndex].position;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player caught, loading GameOver scene.");
            SceneManager.LoadScene("GameOver");
        }
    }
}














