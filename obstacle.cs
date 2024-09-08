using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private playerMovement playerMovementScript;

    void Start()
    {
        // Find the player object by tag and get the playerMovement script
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            playerMovementScript = player.GetComponent<playerMovement>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player collided with this obstacle
        if (other.gameObject.CompareTag("Player"))
        {
            // Trigger the game over functionality on the player's script
            playerMovementScript.GameOver();
        }
    }
}
