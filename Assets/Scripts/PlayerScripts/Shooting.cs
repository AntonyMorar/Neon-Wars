using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header ("Bullets")]
    public GameObject simpleBulletPrefab;

    [Header("Shoot Status")]
    public float bulletForce = 17f;
    public float fireRate = 0.1f; // This is roughly the number of times the ship can be fired in 1 second
    [Range(0.1f, 1.1f)]
    public float shootSensibility = 0.9f;

    [Header("Firepoint")]
    [Range (0,1)]
    public float firepoinOffset = 0.1f;

    private Transform firePoint;
    private float shootAngle;
    private bool movingFirepoint;
    private float nextFire; //Cooldown
    float xInput, yInput;

    private void Start()
    {
        firePoint = GameObject.FindGameObjectWithTag("Firepoint").transform;
    }

    void Update()
    {
        xInput = Input.GetAxis("RSHorizontal");
        yInput = Input.GetAxis("RSVertical");

        if (xInput >= shootSensibility || xInput <= -shootSensibility || yInput >= shootSensibility || yInput <= -shootSensibility)
        {
            movingFirepoint = true;
            if (nextFire > 0)
            {
                nextFire -= Time.deltaTime;
            }
            else
            {
                Shoot();
            }
        }
        else
        {
            movingFirepoint = false;
        }
    }

    private void FixedUpdate()
    {
        // Calculate and apply the shoot angle
        Vector2 shootDir = new Vector2(xInput, yInput);
        if (movingFirepoint)
        {
            shootAngle = Mathf.Atan2(shootDir.x, shootDir.y) * Mathf.Rad2Deg;
        }
        firePoint.rotation = Quaternion.Euler(0, 0, shootAngle - 180f);
        firePoint.position = transform.position + new Vector3(xInput, -yInput, 0) * firepoinOffset;
    }

    void Shoot()
    {
        //Intanciate the buller
        GameObject bullet = Instantiate(simpleBulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        nextFire = fireRate;

        //Todo: Remove when put the walls
        Destroy(bullet, 1f);
    }
}
