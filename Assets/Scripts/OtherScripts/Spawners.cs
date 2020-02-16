using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawners : MonoBehaviour
{
    [Header("Spawners")]
    public Transform playerSpawner;
    public List<Transform> enemySpawner;

    [Header("Camera Data")]
    public GameObject cameraVM;

    [Header("Player Data")]
    public GameObject playerPrefab;
    private float playerRespawnTime;

    [Header("Enemies Data")]
    public List<GameObject> enemies;
    public List<GameObject> spawnParticles;

    static float inverseSpawnChance = Constants.inverseSpawnChance;

    private void Start()
    {
        playerRespawnTime = Constants.playerRespawnTime;
    }

    private void Update()
    {
        SpawnEnemies();
    }

    private Vector3 GetSpawnPosition()
    {
        return new Vector3(0, 0, 0);
    }

    public void ResetSpawnChance()
    {
        inverseSpawnChance = Constants.inverseSpawnChance;
    }

    void SpawnEnemies()
    {
        if (!GameManager.instance.isDead && GameManager.instance.gameStart)
        {
            if (Random.Range(0, (int)inverseSpawnChance) == 0)
            {
                int randomNumberEnemy = Random.Range(0, enemies.Count);
                GameObject enemyClone = Instantiate(enemies[randomNumberEnemy], enemySpawner[Random.Range(0, enemySpawner.Count)]);
                //Sonido de Spawn enemigo
                SoundManager.instance.PlaySound("EnemySpawnBlue");
                //Cea y destruye particulas de la explosión
                GameObject cloneExplotion = Instantiate(spawnParticles[randomNumberEnemy], enemyClone.transform);
                Destroy(cloneExplotion, 1.5f);
            }

            if (inverseSpawnChance > 20) inverseSpawnChance -= 0.004f;
        }
    }

    public void RespawnPlayer()
    {
        if (GameManager.instance.isDead && GameManager.instance.gameStart)
        {
            if (playerRespawnTime >= 0)
            {
                playerRespawnTime -= Time.deltaTime;
            }
            else if (!GameManager.instance.gameOver)
            {
                // Play player spawn sound
                SoundManager.instance.PlaySound("PlayerSpawn");
                //Instantiate the player
                Instantiate(playerPrefab, playerSpawner);
                cameraVM.GetComponent<CameraController>().AttatchPlayer();
                playerRespawnTime = Constants.playerRespawnTime;
                GameManager.instance.isDead = false;
            }
        }
    }
}
