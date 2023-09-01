using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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
    [SerializeField] private Slider dashCooldownSlider;

    private void Start()
    {
        activeMoveSpeed = moveSpeed;

        // Initialize the slider values
        dashCooldownSlider.maxValue = dashCooldown;
        dashCooldownSlider.value = 0;
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
                FindObjectOfType<AudioManager>().Play("Player Roll");
                activeMoveSpeed = dashSpeed;
                dashCounter = dashlengh;
                animator.SetTrigger("Roll");
                gunShoot.enabled = false;
                gunSpriteRender.enabled = !gunSpriteRender.enabled;
            }
        }

        // Update the dash cooldown slider
        dashCooldownSlider.value = dashCooldown - dashCoolCounter;
        if (dashCooldownSlider.value == dashCooldownSlider.maxValue)
        {
            dashCooldownSlider.value = 0;
        }

        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;

            if (dashCounter <= 0)
            {
                activeMoveSpeed = moveSpeed;
                dashCoolCounter = dashCooldown;
                gunShoot.enabled = true;
                gunSpriteRender.enabled = true;
            }
        }

        if (dashCoolCounter > 0)
        {
            dashCoolCounter -= Time.deltaTime;

            // Check if the cooldown is completed and hide the slider
            if (dashCoolCounter <= 0)
            {
                dashCooldownSlider.value = 0;
            }
        }
    }
}
