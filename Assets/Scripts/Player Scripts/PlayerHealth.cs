using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private BoxCollider2D boxCollider2D;
    [SerializeField] private GameObject gun;

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
        if (health <= 0)
        {
            StartCoroutine(Death());
        }

    }

    public IEnumerator Death()
    {
        gun.SetActive(false);
        animator.SetTrigger("Death");
        boxCollider2D.enabled = !enabled;
        rigidbody2D.bodyType = RigidbodyType2D.Static;
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(0);
    }
}
