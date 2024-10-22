using UnityEngine;
using UnityEngine.AI;

public class FakePathing : MonoBehaviour
{
    public Transform[] waypoints;
    public Transform player;
    public float chaseRange = 5f;
    public float chaseSpeedMultiplier = 1.3f; // Multiplier for chase speed
    public AudioClip walkingSound; // Walking sound clip

    private int currentWaypointIndex = 0;
    private NavMeshAgent agent;
    private Vector3 initialPosition;
    private bool isChasing = false;
    private bool isWalkingSoundPlaying = false; // Track if the walking sound is playing

    // Reference to the Animator component
    private Animator animator;

    // Reference to the AudioSource component
    private AudioSource audioSource;

    // Reference to the GameManager (assumes a public gameOver boolean)
    private GameManager gameManager;

    // Reference to the Rigidbody component (if any)
    private Rigidbody rb;

    // Variable to store the base speed of the NavMeshAgent
    private float baseSpeed;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        initialPosition = transform.position;

        // Initialize the animator reference
        animator = GetComponent<Animator>();

        // Store the base speed of the agent
        baseSpeed = agent.speed;

        // Initialize the AudioSource reference
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not found on the enemy!");
        }

        // Initialize the Rigidbody reference (if any)
        rb = GetComponent<Rigidbody>();

        // Find and reference the GameManager (assuming it's tagged as "GameManager")
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        if (gameManager == null)
        {
            Debug.LogError("GameManager not found!");
        }
    }

    void Update()
    {
        // Check if the game is over, and stop the enemy if it is
        if (gameManager != null && gameManager.gameOver)
        {
            StopAndFreezeEnemy();
            return; // Exit the Update function to prevent further movement
        }

        // Ensure that the animations are playing if the game is not over
        if (animator != null)
        {
            animator.speed = 1; // Ensure animations are playing normally
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= chaseRange)
        {
            if (!isChasing)
            {
                isChasing = true;
                agent.speed = baseSpeed * chaseSpeedMultiplier; // Increase speed when chasing
                animator.SetBool("isChasing", true);
            }
            agent.SetDestination(player.position);
        }
        else if (isChasing && distanceToPlayer <= chaseRange * 2)
        {
            agent.SetDestination(player.position);
        }
        else if (isChasing && distanceToPlayer > chaseRange * 2)
        {
            isChasing = false;
            agent.speed = baseSpeed; // Reset speed when no longer chasing
            GoToNextWaypoint();
            animator.SetBool("isChasing", false);
        }
        else if (!isChasing && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            GoToNextWaypoint();
        }

        HandleWalkingSound(); // Call the method to manage walking sound
    }

    void GoToNextWaypoint()
    {
        if (waypoints.Length == 0)
            return;

        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        agent.SetDestination(waypoints[currentWaypointIndex].position);
    }

    // Method to handle walking sound
    void HandleWalkingSound()
    {
        // Check if the agent is moving (i.e., the velocity is not zero)
        if (agent.velocity.magnitude > 0 && !isWalkingSoundPlaying)
        {
            // Play the walking sound if it's not already playing
            if (walkingSound != null)
            {
                audioSource.clip = walkingSound;
                audioSource.loop = true;
                audioSource.Play();
                isWalkingSoundPlaying = true;
            }
        }
        else if (agent.velocity.magnitude == 0 && isWalkingSoundPlaying)
        {
            // Stop the walking sound if the agent stops moving
            audioSource.Stop();
            isWalkingSoundPlaying = false;
        }
    }

    // Method to stop the enemy completely, freeze movement, and pause animations
    void StopAndFreezeEnemy()
    {
        // Stop the NavMeshAgent's pathfinding and movement
        agent.isStopped = true;  // Stop pathfinding
        agent.ResetPath();       // Clear the current path to ensure no residual movement
        agent.velocity = Vector3.zero; // Ensure velocity is zero

        // Disable the NavMeshAgent component to prevent further updates
        agent.enabled = false;

        // Freeze the Rigidbody if it exists
        if (rb != null)
        {
            rb.isKinematic = true; // Prevent further physics interactions
        }

        // Pause animations by setting animator speed to 0
        if (animator != null)
        {
            animator.speed = 0; // Freeze all animations
        }

        // Stop the walking sound if playing
        if (audioSource != null && isWalkingSoundPlaying)
        {
            audioSource.Stop();
            isWalkingSoundPlaying = false;
        }

        Debug.Log("Enemy movement and animations frozen due to game over.");
    }
}