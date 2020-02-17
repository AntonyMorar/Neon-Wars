using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawners : MonoBehaviour
{
    [Header("Spawners")]
    public Transform playerSpawner;
    public List<Transform> enemySpawner;
    public List<Transform> BHSpawner;

    [Header("Camera Data")]
    public GameObject cameraVM;

    [Header("Player Data")]
    public GameObject playerPrefab;
    public GameObject spawnPlayerParticles;
    private float playerRespawnTime;

    [Header("Enemies Data")]

    public List<GameObject> enemies;
    public List<GameObject> spawnParticles;

    //Enemies
    private int wave;
    private float gruntSpawnChance;
    private float weaverSpawnChance;
    private float BHSpawnChance;

    private void Start()
    {
        playerRespawnTime = Constants.playerRespawnTime;
        gruntSpawnChance = Constants.gruntSpawnChance;
        weaverSpawnChance = Constants.weaverSpawnChance;
        BHSpawnChance = Constants.BHSpawnChance;
        wave = 1;
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
        gruntSpawnChance = Constants.gruntSpawnChance;
        weaverSpawnChance = Constants.weaverSpawnChance;
        BHSpawnChance = Constants.BHSpawnChance;
    }

    void SpawnEnemies()
    {
        if (!GameManager.instance.isDead && GameManager.instance.gameStart)
        {
            SpawnBH();
        }
    }

    void SpawnGruntWave()
    {
        if (Random.Range(0, (int)gruntSpawnChance) == 0)
        {
            GameObject enemyClone = Instantiate(enemies[(int)Constants.enemies.GRUNT], enemySpawner[Random.Range(0, enemySpawner.Count)]);
            //Sonido de Spawn enemigo
            SoundManager.instance.PlaySound("EnemySpawnBlue");
            //Cea y destruye particulas la creacion de jugador
            GameObject cloneExplotion = Instantiate(spawnParticles[(int)Constants.enemies.GRUNT], enemyClone.transform);
            Destroy(cloneExplotion, 1.5f);
        }

        if (gruntSpawnChance > 10) gruntSpawnChance -= 0.004f;
    }

    void SpawnWavers()
    {
        if (Random.Range(0, (int)weaverSpawnChance) == 0)
        {
            GameObject enemyClone = Instantiate(enemies[(int)Constants.enemies.WEAVER], enemySpawner[Random.Range(0, enemySpawner.Count)]);
            //Sonido de Spawn enemigo
            SoundManager.instance.PlaySound("EnemySpawnGreen");
            //Cea y destruye particulas la creacion de jugador
            GameObject cloneExplotion = Instantiate(spawnParticles[(int)Constants.enemies.WEAVER], enemyClone.transform);
            Destroy(cloneExplotion, 1.5f);
        }

        if (weaverSpawnChance > 100) weaverSpawnChance -= 0.004f;
    }

    void SpawnBH()
    {
        if (Random.Range(0, (int)BHSpawnChance) == 0)
        {
            int spawnPos = Random.Range(0, BHSpawner.Count);
            if (BHSpawner[spawnPos].childCount <= 0)
            {
                GameObject enemyClone = Instantiate(enemies[(int)Constants.enemies.BH], BHSpawner[spawnPos]);
                enemyClone.GetComponent<Animator>().enabled = false;
                //Sonido de Spawn enemigo
                SoundManager.instance.PlaySound("EnemySpawnRed");
                //Cea y destruye particulas la creacion de jugador
                GameObject cloneExplotion = Instantiate(spawnParticles[(int)Constants.enemies.BH], enemyClone.transform);
                Destroy(cloneExplotion, 1.5f);
            }
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
            else
            {
                // Play player spawn sound
                SoundManager.instance.PlaySound("PlayerSpawn");
                //Instantiate the player
                GameObject playerClone = Instantiate(playerPrefab, playerSpawner);
                // Agrega las particulas para respawn de jugador
                GameObject spawnClone = Instantiate(spawnPlayerParticles, playerClone.transform);
                Destroy(spawnClone, 2f);
                //Hace link con la camara
                cameraVM.GetComponent<CameraController>().AttatchPlayer();
                playerRespawnTime = Constants.playerRespawnTime;
                GameManager.instance.isDead = false;
            }
        }
    }
}
