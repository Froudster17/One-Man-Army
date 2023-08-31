using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private Slider healthBar;

    private void Start()
    {
        healthBar.maxValue = health;
        healthBar.value = health;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        healthBar.value = health;
        FindObjectOfType<AudioManager>().Play("Player Hit");
    }
}
