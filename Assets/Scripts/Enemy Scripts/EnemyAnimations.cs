using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private CircleCollider2D circleCollider2D;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            Hit();
        }
    }

    private void Hit()
    {
        animator.SetTrigger("Hit");
    }

    public IEnumerator Death()
    {
        animator.SetTrigger("Death");
        circleCollider2D.enabled = !enabled;
        rigidbody2D.bodyType = RigidbodyType2D.Static;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
