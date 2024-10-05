using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyBehavior enemyBehavior;
    //reference to enemyBehavior script

    [SerializeField] private float health;

    void Start(){
        enemyBehavior = GetComponent<EnemyBehavior>();

        Debug.Log("Base Speed: " + enemyBehavior.baseSpeed);
        Debug.Log("Attack Strength: " + enemyBehavior.baseDamage);
    }

    public void TakeDamage(float damage) {
        health -= damage;

        if (health <= 0f) {
            Destroy(gameObject);
            Debug.Log("Enemy Died");
        }
    }
}
