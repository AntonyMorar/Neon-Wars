using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Ship Status")]
    public float shipSpeed;
    [Range (0,1)]
    public float rotationInterpolation = 0.4f;
    [Range (0, 0.2f)]
    public float rotationSensibility = 0.085f;

    [Header("Ship In Game")]
    public bool hasShield;
    private bool itsMoving;

    [Header("Ship Editors")]
    public GameObject explotion;
    public ParticleSystem sparks;

    private Rigidbody2D rb;
    private float shipAngle;
    float xInput, yInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get the user input
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");

        if (xInput >= rotationSensibility || xInput <= -rotationSensibility || yInput >= rotationSensibility || yInput <= -rotationSensibility) {
            itsMoving = true;
            sparks.Play();
        }
        else
        {
            itsMoving = false;
            sparks.Stop();
        }

    }

    private void FixedUpdate()
    {
        // Move the player spaceship
        rb.velocity = new Vector2(xInput, yInput) * shipSpeed * Time.fixedDeltaTime;
        GetRotation();
    }

    // Rotate the player spaceship
    void GetRotation()
    {
        Vector2 lookDir = new Vector2(-xInput, yInput);

        if (itsMoving)
        {
            shipAngle = Mathf.Atan2(lookDir.x, lookDir.y) * Mathf.Rad2Deg;
        }

        if (rb.rotation <= -90 && shipAngle >= 90){
            rb.rotation += 360;
            rb.rotation = Mathf.Lerp(rb.rotation, shipAngle, rotationInterpolation);
        }

        if (rb.rotation >= 90 && shipAngle <= -90){
            rb.rotation -= 360;
            rb.rotation = Mathf.Lerp(rb.rotation, shipAngle, rotationInterpolation);
        }else
        {
            rb.rotation = Mathf.Lerp(rb.rotation, shipAngle, rotationInterpolation);
        }
    }

    void DestroyPlayer()
    {
        //Explotion particle system play
        GameObject cloneExplotion = (GameObject)Instantiate(explotion, transform.position, Quaternion.identity);
        Destroy(cloneExplotion, 0.8f);

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            GameManager.instance.isDead = true;
            GameManager.instance.DestroyAllEnemies();

            DestroyPlayer();
        }
    }


}
