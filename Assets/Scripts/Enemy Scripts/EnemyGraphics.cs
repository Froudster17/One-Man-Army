using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyGraphics : MonoBehaviour
{
    [SerializeField] private AIPath aiPath;
    [SerializeField] private Sprite hitSprite;
    [SerializeField] private float hitDuration = 1.0f; // Duration to display hit sprite

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite originalSprite;
    [SerializeField] private bool isHit = false;

    private void Start()
    {
        originalSprite = spriteRenderer.sprite;
    }

    private void Update()
    {
        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (aiPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        if (isHit)
        {
            StartCoroutine(DisplayHitSprite());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            isHit = true;
        }
    }

    private IEnumerator DisplayHitSprite()
    {
        spriteRenderer.sprite = hitSprite;
        yield return new WaitForSeconds(hitDuration);
        spriteRenderer.sprite = originalSprite;
        isHit = false;
    }
}