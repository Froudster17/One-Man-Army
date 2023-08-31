using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Vector2 playerInput;
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private Animator animator;
    [SerializeField] private int moveSpeed;
    private float activeMoveSpeed;
    public float dashSpeed;

    private float dashCounter;
    private float dashCoolCounter;
    public float dashlengh = .5f, dashCooldown = 1f;

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
            }
        }

        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;

            if (dashCounter <= 0)
            {
                activeMoveSpeed = moveSpeed;
                dashCoolCounter = dashCooldown;
            }
        }

        if (dashCoolCounter > 0)
        {
            dashCoolCounter -= Time.deltaTime;
        }
    }
}
