using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShoot : MonoBehaviour
{
    [SerializeField] private int ammo;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletForce = 20f;
    [SerializeField] private float shotDelay = 0.2f; // Delay between shots

    public bool canShoot = true;
    public bool isRolling = false;
    public float lastShotTime;

    private void Update()
    {
        if ((Input.GetButtonDown("Fire1") || Input.GetButton("Fire1")) && canShoot)
        {
            Shoot();
            lastShotTime = Time.time;
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
    }

    private void LateUpdate()
    {

        if (isRolling == false)
        {
            if (Time.time - lastShotTime > shotDelay)
            {
                canShoot = true;
            }
            else
            {
                canShoot = false;
            }
        }
    }
}
