using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private EnemyAnimations enemyAnimations;

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            StartCoroutine(enemyAnimations.Death());
        }
    }
}
