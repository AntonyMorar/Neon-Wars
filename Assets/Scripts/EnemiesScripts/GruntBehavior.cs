  using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntBehavior : MonoBehaviour
{
    [Header("Enemy Status")]
    public float speed = 2f;

    private Rigidbody2D rb;
    private Transform target;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        Vector3 direction = target.position - transform.position;
        direction.Normalize();
        movement = direction;
    }

    private void FixedUpdate()
    {
        MoveEnemy(movement);
    }

    void MoveEnemy(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
    }

    void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            DestroyEnemy();
        }
    }

}
