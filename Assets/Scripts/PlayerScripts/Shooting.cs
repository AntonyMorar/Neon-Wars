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
    [Range(0.1f, 1f)]
    public float shootSensibility = 0.9f;

    [Header("Firepoint")]
    [Range (0,1)]
    public float firepoinOffset;

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
        GetRightJoystickInput();

        if (Input.GetKeyDown(0))
        {
            Debug.Log("A pressed");
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

        if (GameManager.instance.hasShieldInmunity)
        {
            firePoint.position = transform.position + new Vector3(xInput, -yInput, 0) * (firepoinOffset + 0.2f);
        }
        else
        {
            firePoint.position = transform.position + new Vector3(xInput, -yInput, 0) * firepoinOffset;
        }
    }

    private void GetRightJoystickInput()
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

    void Shoot()
    {
        // Bullet sound
        SoundManager.instance.PlaySound("FireNormal");

        //Intanciate the bullet
        GameObject bullet = Instantiate(simpleBulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        nextFire = fireRate;

        Destroy(bullet, 1.5f);
    }
}
