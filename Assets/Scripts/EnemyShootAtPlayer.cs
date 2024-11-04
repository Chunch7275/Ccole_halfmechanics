using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootAtPlayer : MonoBehaviour
{
    public float shootingRange = 10f;    // Distance at which enemy starts shooting
    public float fireRate = 1f;          // Time between shots (in seconds)
    public int damage = 10;              // Damage dealt to the player
    public GameObject player;            // Reference to the player object
    public AudioClip shootingSound;      // Reference to the shooting sound clip
    public ParticleSystem shootingParticles;  // Reference to the particle system

    private float nextTimeToFire = 0f;   // Time until the next shot can be fired
    private playerstats playerstats;     // Reference to the player's PlayerStats script

    // Reference to the Animator component
    private Animator animator;

    // Reference to the AudioSource component
    private AudioSource audioSource;

    // Reference to the GameManager (assumes a public gameOver boolean)
    private GameManager gameManager;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player"); // Assuming the player has a tag "Player"
        }

        playerstats = player.GetComponent<playerstats>();

        if (playerstats == null)
        {
            Debug.LogError("PlayerStats script not found on the player!");
        }

        // Get the Animator component
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("Animator component not found on the enemy!");
        }

        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not found on the enemy!");
        }

        // Check if the ParticleSystem is assigned
        if (shootingParticles == null)
        {
            Debug.LogError("ParticleSystem not assigned to the enemy!");
        }

        // Find and reference the GameManager (assuming it's tagged as "GameManager")
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        if (gameManager == null)
        {
            Debug.LogError("GameManager not found!");
        }
    }

    void Update()
    {
        shootingRange = shootingRange * (playerstats.stealthValue/5);
        // Check if the game is over, and stop all actions if it is
        if (gameManager != null && gameManager.gameOver)
        {
            StopEnemyActions();
            return; // Exit the Update function to prevent further actions
        }

        if (player != null)
        {
            // Calculate the distance between enemy and player
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            // Check if the player is within shooting range
            if (distanceToPlayer <= shootingRange)
            {
                // Shoot at the player if it's time to shoot again
                if (Time.time >= nextTimeToFire)
                {
                    Shoot();
                    nextTimeToFire = Time.time + 1f / fireRate;
                }
            }
        }
    }

    void Shoot()
    {
        // Damage the player by calling the TakeDamage method in the PlayerStats script
        if (playerstats != null)
        {
            playerstats.TakeDamage(damage);
            Debug.Log("Enemy shot the player! Player health: " + playerstats.health);

            // Trigger the "Shoot" animation
            if (animator != null)
            {
                animator.SetTrigger("Shoot");
            }

            // Play the shooting sound if available
            if (audioSource != null && shootingSound != null)
            {
                audioSource.PlayOneShot(shootingSound);
            }

            // Play the shooting particle system
            if (shootingParticles != null)
            {
                shootingParticles.Play();
            }
        }
    }

    // Method to stop all actions (animations, sounds, particle effects) when the game is over
    void StopEnemyActions()
    {
        // Stop animations by setting animator speed to 0
        if (animator != null)
        {
            animator.speed = 0;
        }

        // Stop any playing audio
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        // Stop the particle system if it's playing
        if (shootingParticles != null && shootingParticles.isPlaying)
        {
            shootingParticles.Stop();
        }

        Debug.Log("Enemy actions stopped due to game over.");
    }
}