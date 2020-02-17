using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaverBehavior : Enemy
{
    [Header("Weaver")]
    [SerializeField]
    private float lookRadius;

    public GameObject []bullets;

    protected override void Start()
    {
        if (!useInspectorParams)
        {
            speed = Constants.weaverSpeed;
            enemyScore = Constants.weaverScore;
            respawnTime = Constants.weaverRespawnTime;
            lookRadius = Constants.weaverlookRadius;
        }

        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        bullets = GameObject.FindGameObjectsWithTag("Bullet");
        float distance;

        if (bullets.Length > 0) {
            for (int i = 0; i< bullets.Length; i++)
            {
                distance = Vector3.Distance(bullets[0].transform.position, transform.position);
                Debug.Log(distance);
            }
        }
    }

    protected override void FixedUpdate()
    {

        base.FixedUpdate();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
