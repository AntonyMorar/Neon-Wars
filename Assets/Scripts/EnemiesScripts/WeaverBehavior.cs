using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaverBehavior : Enemy
{
    [Header("Weaver")]

    public GameObject []bullets;

    protected override void Start()
    {
        if (!useInspectorParams)
        {
            speed = Constants.weaverSpeed;
            enemyScore = Constants.weaverScore;
            respawnTime = Constants.weaverRespawnTime;
        }

        base.Start();
    }

    protected override void FixedUpdate()
    {

        base.FixedUpdate();
    }
}
