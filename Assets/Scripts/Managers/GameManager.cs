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
    [Space(10)]
    [Range(1,5)]
    public int multiplier;
    public int scoreToMultiplier;
    public bool multiplayerChanged;
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
        // Chwck if the game is over
        GameOver();

        // Respawn al jugador despues de cierto tiempo
        spawnerMaster.RespawnPlayer();

        //Revisa el multiplier
        CheckMultiplier();
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

    void CheckMultiplier()
    {
        if (scoreToMultiplier >= Constants.pointsForMultiplier2 && multiplier==1)
        {
            multiplier = 2;
            multiplayerChanged = true;
        }
        else if (scoreToMultiplier >= Constants.pointsForMultiplier3 && multiplier == 2)
        {
            multiplier = 3;
            multiplayerChanged = true;
        }
        else if (scoreToMultiplier >= Constants.pointsForMultiplier4 && multiplier == 3)
        {
            multiplier = 4;
            multiplayerChanged = true;
        }
        else if (scoreToMultiplier >= Constants.pointsForMultiplier5 && multiplier == 4)
        {
            multiplier = 5;
            multiplayerChanged = true;
        }
    }

    void GameOver()
    {
        if (GameManager.instance.gameOver)
        {
            //Manage the UI when game is over
            UIManager.instance.ShowGameOverMenu(true);
            UIManager.instance.ShowFireworkParty(true);
            // Change the BG music
            soundManager.PlaySound("GameOver");
            soundManager.PauseSound("GameSoundtrack");
            soundManager.PlaySound("MenuSoundtrack");

            //Make game over false
            gameOver = false;
        }
    }

}
