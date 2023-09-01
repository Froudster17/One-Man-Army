using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private EnemyAnimations enemyAnimations;
    [SerializeField] private Slider healthBar;

    private void Start()
    {
        healthBar.gameObject.SetActive(false); // Hide the health bar on start

        healthBar.maxValue = health;
        healthBar.value = health;
    }

    public void TakeDamage(int amount)
    {
        if (!healthBar.gameObject.activeSelf)
        {
            healthBar.gameObject.SetActive(true); // Show the health bar when taking damage
        }

        health -= amount;
        healthBar.value = health;

        AudioManager audioManager = FindObjectOfType<AudioManager>();
        if (audioManager != null)
        {
            audioManager.Play("Slime Hit");
        }

        if (health <= 0)
        {
            StartCoroutine(enemyAnimations.Death());
        }
    }
}
