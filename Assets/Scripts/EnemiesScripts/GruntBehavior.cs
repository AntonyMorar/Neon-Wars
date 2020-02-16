using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntBehavior : Enemy
{
    protected override void Start()
    {
        if (!useInspectorParams)
        {
            speed = Constants.gruntSpeed;
            enemyScore = Constants.gruntScore;
            respawnTime = Constants.gruntRespawnTime;
        }

        base.Start();
    }
}
