using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeAttack : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform firePoint;

    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private GameObject bulletPrefab;

    private bool canFire = true;

    private void Update()
    {
        RotateTowardsPlayer();
    }

    private void RotateTowardsPlayer()
    {
        Vector3 directionToPlayer = player.position - firePoint.position;
        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        firePoint.rotation = rotation;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canFire == false)
        {
            return;
        }

        if (collision.CompareTag("Player"))
        {
            canFire = false;
            StartCoroutine(RangeAttack());

        }
    }


    private IEnumerator RangeAttack()
    {
        animator.SetTrigger("Attack");
        rigidbody2D.bodyType = RigidbodyType2D.Static;
        yield return new WaitForSeconds(1f);
        rigidbody2D.bodyType = RigidbodyType2D.Dynamic;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * 10f, ForceMode2D.Impulse);
        canFire = true;
    }
}
