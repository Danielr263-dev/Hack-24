using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f; // Maximum health of the player
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth; // Initialize player's health to maximum at the start
    }

    // Method to take damage
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("Player took damage. Current health: " + currentHealth);

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    // Handle player death
    void Die()
    {
        Debug.Log("Player Died");
        // Additional death logic (e.g., respawn, game over screen, etc.)
    }
}
