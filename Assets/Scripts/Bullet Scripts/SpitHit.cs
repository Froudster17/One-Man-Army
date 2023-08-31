using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitHit : MonoBehaviour
{
    [SerializeField] private GameObject hitEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Inflict damage to the enemy if it has an EnemyHealth component
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(1);
        }

        // Instantiate and destroy hit effect
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 0.3f);

        // Destroy the bullet
        Destroy(gameObject);
    }
}
