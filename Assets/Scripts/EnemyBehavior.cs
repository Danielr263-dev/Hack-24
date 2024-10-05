using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    // Default values for the enemies
    public Transform Player; // Player's position
    public float baseSpeed = 2f; // Enemy's default movement speed
    public float baseDamage = 10f; // Base attack damage
    public float attackRange = 2f; // Distance within which the enemy can attack the player

    private float speedMultiplier = 1.0f;
    private float damageMultiplier = 1.0f;

    void Awake() // or use Start()
{
    if (Player == null)
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            Player = playerObject.transform;
            Debug.Log("Player reference assigned to: " + Player.name);
        }
        else
        {
            Debug.LogError("Player GameObject with tag 'Player' not found. Please assign a GameObject with the 'Player' tag.");
        }
    }
}


    void Update()
    {
        // Move towards the player
        MoveTowardsPlayer();

        // If close enough, attack the player
        if (Vector3.Distance(transform.position, Player.position) <= attackRange)
        {
            AttackPlayer();
        }
    }

    void MoveTowardsPlayer()
    {
        // Move towards the player's position at scaled speed
        Vector3 direction = (Player.position - transform.position).normalized;
        transform.position += direction * baseSpeed * speedMultiplier * Time.deltaTime;
    }

    void AttackPlayer()
    {
        // Check if Player is assigned, to avoid errors
        if (Player != null)
        {
            // Get the PlayerHealth component from the Player GameObject
            PlayerHealth playerHealth = Player.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                // Apply baseDamage * damageMultiplier to the player's health
                float totalDamage = baseDamage * damageMultiplier;
                playerHealth.TakeDamage(totalDamage);
                Debug.Log("Attacking the player with damage: " + totalDamage);
            }
            else
            {
                Debug.LogError("No PlayerHealth component found on the Player GameObject.");
            }
        }
        else
        {
            Debug.LogError("Player reference is null, cannot attack.");
        }
    }

    public void SetDifficulty(float speedScale, float damageScale)
    {
        // Set the difficulty scaling factors
        speedMultiplier = speedScale;
        damageMultiplier = damageScale;
    }
}
