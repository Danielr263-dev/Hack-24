using UnityEngine;

[RequireComponent(typeof(Collider2D))] // Ensures a Collider2D component is present
[RequireComponent(typeof(Rigidbody2D))] // Ensures a Rigidbody2D component is present
public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private float maxHealth = 100f; // Maximum health of the player
    private float currentHealth;    // Current health of the player

    [SerializeField]
    private float damageOnCollision = 20f; // Amount of health reduced on collision with an enemy

    private Rigidbody2D rb2d;

    void Start()
    {
        // Initialize player's health to maximum at the start
        currentHealth = maxHealth;

        // Get Rigidbody2D component
        rb2d = GetComponent<Rigidbody2D>();
        
        // Set Rigidbody2D to kinematic to prevent physical forces affecting the player
        rb2d.bodyType = RigidbodyType2D.Kinematic;

        // Ensure Collider2D is set as trigger to detect collisions properly
        Collider2D collider = GetComponent<Collider2D>();
        collider.isTrigger = true;
    }

    // Method to take damage
    public void TakeDamage(float damage)
    {
        currentHealth -= damage; // Reduce current health by the damage amount
        Debug.Log("Player took damage. Current health: " + currentHealth);

        // Check if health is zero or less
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    // Handle player death
    private void Die()
    {
        Debug.Log("Player Died");

        // Stop the game by setting the time scale to zero
        Time.timeScale = 0f;

        // Optionally, display a game over UI or provide feedback to the player
        // Example: UIManager.Instance.ShowGameOverScreen();
    }

    // Collision with an enemy causes the player to take damage and the enemy to die
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Reduce player health
            TakeDamage(damageOnCollision);

            // Destroy the enemy that collided with the player
            Destroy(collision.gameObject);

            // Log that enemy was destroyed, but it does not count as a kill
            Debug.Log("Enemy destroyed on collision, but it does not count as a kill.");
        }
    }
}
