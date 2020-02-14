using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Status")]
    public float speed = 2.5f;
    public int enemyScore = 10;
    [SerializeField]
    private bool isRespawning;
    public float respawnTime = 0.7f;

    [Header("Enemy Editors")]
    public GameObject explotion;

    protected Rigidbody2D rb;
    private Transform playerTarget;
    private Vector2 movement;
    private PolygonCollider2D pCollider;

    void Start()
    {
        //Obtiene el colider del objeto y lo desabilita
        pCollider = GetComponent<PolygonCollider2D>();
        pCollider.isTrigger = true;
        //Esta reapareciando cuando se iniciañliza el enemigo
        isRespawning = true;
        //Añade enemigo al contador global
        GameManager.instance.aliveEnemies += 1;
        rb = GetComponent<Rigidbody2D>();
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        Vector3 direction = playerTarget.position - transform.position;
        direction.Normalize();
        movement = direction;

        RespawnTimer();
    }

    private void FixedUpdate()
    {
        MoveEnemy(movement);
    }

    void MoveEnemy(Vector2 direction)
    {
        if (!isRespawning)
        {
            rb.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            DestroyEnemy();
            SendToGameManager();
        } 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            DestroyEnemy();
            SendToGameManager();
        }
    }

    void RespawnTimer()
    {
        if (respawnTime <= 0)
        {
            pCollider.isTrigger = false;
            isRespawning = false;
        }
        else
        {
            respawnTime -= Time.deltaTime;
        }
    }

    public virtual void DestroyEnemy()
    {
        //Explotion particle system play
        GameObject cloneExplotion = (GameObject)Instantiate(explotion, transform.position, Quaternion.identity);
        Destroy(cloneExplotion, 0.8f);

        Destroy(gameObject);
    }

    void SendToGameManager()
    {
        //Add points to player score
        GameManager.instance.aliveEnemies -= 1;
        GameManager.instance.score += enemyScore;
    }
}
