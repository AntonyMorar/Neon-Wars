using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        MakeSingleton();
    }

    [Header("Player Status")]
    public int score = 0;
    public int lives = 4;
    public int superPower = 3;
    public int highScore;

    [Header("Game Status")]
    public int aliveEnemies;
    public bool isDead;
    private Dictionary<string, int> highScores;

    private GameObject[] enemies;

    void ResetGameStatus()
    {
        score = 0;
        lives = 4;
        superPower = 3;
        enemies = null;
    }

    public void DestroyAllEnemies()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<Enemy>().DestroyEnemy();
        }
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
}
