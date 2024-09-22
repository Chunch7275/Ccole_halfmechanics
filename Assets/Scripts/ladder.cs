using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ladder : MonoBehaviour
{
    public float climbSpeed = 3f; // Speed of climbing the ladder
    private bool isClimbing = false; // Whether the player is currently climbing

    private Rigidbody playerRigidbody;
    private CharacterController playerController;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Get the player's Rigidbody and CharacterController components
            playerRigidbody = other.GetComponent<Rigidbody>();
            playerController = other.GetComponent<CharacterController>();

            if (playerRigidbody != null && playerController != null)
            {
                // Disable the rigidbody's gravity when climbing
                playerRigidbody.useGravity = false;
                isClimbing = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Re-enable the rigidbody's gravity when leaving the ladder
            if (playerRigidbody != null)
            {
                playerRigidbody.useGravity = true;
            }
            isClimbing = false;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (isClimbing && other.CompareTag("Player"))
        {
            // Climb up when the player is holding the "up" key (default: W or Up Arrow)
            float verticalInput = Input.GetAxis("Vertical");

            if (verticalInput > 0f)
            {
                Vector3 climbDirection = new Vector3(0f, climbSpeed, 0f);
                playerController.Move(climbDirection * Time.deltaTime);
            }
        }
    }
}