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

    [SerializeField] private bool canShoot = true;

    private void Update()
    {
        if ((Input.GetButtonDown("Fire1") || Input.GetButton("Fire1")) && canShoot)
        {
            Debug.Log("Gun Fired");
            Shoot();
            StartCoroutine(ShotDelay());
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
    }

    private IEnumerator ShotDelay()
    {
        canShoot = false;
        yield return new WaitForSeconds(shotDelay);
        canShoot = true;
    }
}
