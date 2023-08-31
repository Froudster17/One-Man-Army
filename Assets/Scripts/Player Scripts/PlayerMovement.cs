using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Vector2 playerInput;
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private Animator animator;
    [SerializeField] private int moveSpeed;
    [SerializeField] private float activeMoveSpeed;
    [SerializeField] private float dashSpeed;

    [SerializeField] private float dashCounter;
    [SerializeField] private float dashCoolCounter;
    [SerializeField] private float dashlengh = .5f, dashCooldown = 1f;

    [SerializeField] private GunShoot gunShoot;
    [SerializeField] private SpriteRenderer gunSpriteRender;

    private void Start()
    {
        activeMoveSpeed = moveSpeed;
    }

    private void Update()
    {
        playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rigidbody2D.velocity = playerInput.normalized * activeMoveSpeed;

        animator.SetFloat("Velocity", playerInput.sqrMagnitude);

        if (playerInput.x < 0)
        {
            Vector3 newScale = transform.localScale;
            newScale.x = -1;
            transform.localScale = newScale;
        }
        else if (playerInput.x > 0)
        {
            Vector3 newScale = transform.localScale;
            newScale.x = 1;
            transform.localScale = newScale;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (dashCoolCounter <= 0 && dashCounter <= 0)
            {
                activeMoveSpeed = dashSpeed;
                dashCounter = dashlengh;
                animator.SetTrigger("Roll");
                gunShoot.isRolling = true;
                gunSpriteRender.enabled = !gunSpriteRender.enabled;
            }
        }

        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;

            if (dashCounter <= 0)
            {
                activeMoveSpeed = moveSpeed;
                dashCoolCounter = dashCooldown;
                gunShoot.isRolling = false;
                gunSpriteRender.enabled = true;
            }
        }

        if (dashCoolCounter > 0)
        {
            dashCoolCounter -= Time.deltaTime;
        }
    }
}
