using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunShoot : MonoBehaviour
{
    [SerializeField] private int maxAmmo = 10; // Maximum ammo capacity
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletForce = 20f;
    [SerializeField] private float shotDelay = 0.2f; // Delay between shots
    [SerializeField] private Slider ammoSlider;
    [SerializeField] private float ammoRechargeRate = 1f; // Ammo recharge rate per second


    [SerializeField ]private int currentAmmo;
    public bool canShoot = true;
    public bool isRolling = false;
    public float lastShotTime;
    private bool bulletSpawning = false;
    private bool reloading = false;

    private void Start()
    {
        currentAmmo = maxAmmo; // Start with full ammo
        UpdateAmmoUI();
    }

    private void OnEnable()
    {
        canShoot = true;
    }

    private void Update()
    {
        if ((Input.GetButtonDown("Fire1") || Input.GetButton("Fire1")) && canShoot && currentAmmo > 0)
        {
            lastShotTime = Time.time;
            Shoot();
        }

        // Check if ammo is empty and trigger ammo recharge
        if (currentAmmo <= maxAmmo)
        {
            if (reloading == false)
            {
                reloading = true;
                StartCoroutine(RechargeAmmo());
            }
        }
    }

    private void Shoot()
    {
        FindObjectOfType<AudioManager>().Play("Fire");
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
        currentAmmo -= 1;
        UpdateAmmoUI();
    }

    private IEnumerator RechargeAmmo()
    {
        Debug.Log("Recharging ammo");
        yield return new WaitForSeconds(1f / ammoRechargeRate);
        currentAmmo += Mathf.RoundToInt(currentAmmo * 0.2f) + 1; 
        currentAmmo = Mathf.Clamp(currentAmmo, 0, maxAmmo);
        UpdateAmmoUI();
        reloading = false;
    }

    private void UpdateAmmoUI()
    {
        if (ammoSlider != null)
        {
            ammoSlider.value = (float)currentAmmo / maxAmmo;
        }
    }

    private void LateUpdate()
    {
        //if (isRolling == true)
        //{
            //canShoot = false;
       // }

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
