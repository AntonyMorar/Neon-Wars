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
    public float shieldTime = 2f;
    public bool hasShield;
    private bool itsMoving;

    [Header("Ship Editors")]
    public GameObject explotion;
    public ParticleSystem sparks;

    private Rigidbody2D rb;
    private PolygonCollider2D playeColider;
    private float shipAngle;
    float xInput, yInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playeColider = GetComponent<PolygonCollider2D>();
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "BH")
        {
            GameManager.instance.lives -= 1;
            GameManager.instance.isDead = true;
            GameManager.instance.DestroyAllEnemies();

            DestroyPlayer();
        }

        if (collision.gameObject.tag == "Wall")
        {
            // Play ShipHitwall Sound
            SoundManager.instance.PlaySound("ShipHitwall");
        }
    }

    void DestroyPlayer()
    {
        // Play ShipExplode sound
        SoundManager.instance.PlaySound("ShipExplode");
        //Explotion particle system play and destroy
        GameObject cloneExplotion = (GameObject)Instantiate(explotion, transform.position, Quaternion.identity);
        Destroy(cloneExplotion, 0.8f);
        //Destry the player game object
        Destroy(gameObject);

        // Set true gameOver if the lives are over 
        if (GameManager.instance.lives <= 0)
        {
            GameManager.instance.gameOver = true;
            GameManager.instance.gameStart = false;
        }

        //Remove the score multiplayer
        GameManager.instance.multiplier = 1;
        GameManager.instance.scoreToMultiplier = 0;
    }


}
