using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health;

    public void TakeDamage(float damage) {
        health -= damage;

        if (health <= 0f) {
            Destroy(gameObject);
            Debug.Log("Enemy Died");
        }
    }
}
