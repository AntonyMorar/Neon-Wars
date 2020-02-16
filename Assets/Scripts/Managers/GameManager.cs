using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Spawner Status")]
    public Spawners spawnerMaster;

    [Header("Player Status")]
    public int score;
    public int lives;
    public int superPower;
    [Range(1,10)]
    public int multiplier;
    [HideInInspector]
    public int highScore;

    [Header("Game Status")]
    public int aliveEnemies;
    public bool isDead;
    public bool gameOver;
    public bool gameStart;
    private Dictionary<string, int> highScores;

    private GameObject[] enemies;

    //Cache
    private SoundManager soundManager;

    private void Awake()
    {
        MakeSingleton();
        highScore = 0;
    }

    private void MakeSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        // Restart initial paramteres
        ResetGameStatus();

        //Caching
        soundManager = SoundManager.instance;
        if (soundManager == null)
        {
            Debug.LogError("GameManager: No sound manager found in the scene");
        }

        soundManager.PlaySound("MenuSoundtrack");
    }

    private void Update()
    {
        //Revisa si el juego termino
        if (lives <= 0)
        {
            //Game Over values
            gameOver = true;
            gameStart = false;
        }
        // Respawn al jugador despues de cierto tiempo
        spawnerMaster.RespawnPlayer();
    }

    public void ResetGameStatus()
    {
        CheckHighscore();
        score = 0;
        lives = Constants.lives;
        superPower = Constants.superPower;
        multiplier = Constants.multiplierInit;
        DestroyAllEnemies();
        isDead = true;
        gameOver = false;
        gameStart = false;
        spawnerMaster.ResetSpawnChance();
    }

    public void DestroyAllEnemies()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<Enemy>().DestroyEnemy();
        }
        aliveEnemies = 0;
    }

    void CheckHighscore()
    {
        if (score > highScore)
        {
            highScore = score;
        }
    }
}
