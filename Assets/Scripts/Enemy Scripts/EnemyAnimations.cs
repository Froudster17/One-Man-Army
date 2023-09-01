using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private CircleCollider2D circleCollider2D;
    [SerializeField] private int pointAmount;


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Bullet"))
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
        FindObjectOfType<AudioManager>().Play("Slime Death");
        animator.SetTrigger("Death");
        circleCollider2D.enabled = !enabled;
        rigidbody2D.bodyType = RigidbodyType2D.Static;
        yield return new WaitForSeconds(1f);
        FindObjectOfType<Points>().AddPoints(pointAmount);
        Destroy(gameObject);
    }
}
