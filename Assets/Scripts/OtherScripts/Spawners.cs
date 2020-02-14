using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawners : MonoBehaviour
{
    [Header("Spawners")]
    public Transform playerSpawner;
    public List<Transform> enemySpawner;

    [Header("Enemies")]
    public List<GameObject> enemies;
    public List<GameObject> spawnParticles;

    static float inverseSpawnChance = 90f;

    private void Update()
    {
        if (!GameManager.instance.isDead)
        {
            if (Random.Range(0, (int)inverseSpawnChance) == 0)
            {
                int randomNumberEnemy = Random.Range(0, enemies.Count);
                GameObject enemyClone = Instantiate(enemies[randomNumberEnemy], enemySpawner[Random.Range(0, enemySpawner.Count)]);

                GameObject cloneSpawner = Instantiate(spawnParticles[randomNumberEnemy], enemyClone.transform);
                Destroy(cloneSpawner, 1.5f);
            }
        }

        if (inverseSpawnChance > 20) inverseSpawnChance -= 0.004f;
    }

    private Vector3 GetSpawnPosition()
    {
        return new Vector3(0, 0, 0);
    }

    private void Reset()
    {
        inverseSpawnChance = 60f;
    }
}
