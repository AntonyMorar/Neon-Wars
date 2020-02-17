using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BalckHoleBehavior : MonoBehaviour
{
    [SerializeField]
    protected bool useInspectorParams;

    [Header("Black Hole Status")]
    [SerializeField]
    private int lives;
    public int enemyScore;
    private bool isRespawning;
    public float respawnTime;

    [Header("Black Hole Editors")]
    public GameObject bigExplotion;
    public GameObject blackHoleParticles;
    public GameObject floatingPoints;
    public GameObject floatingMultiplier;

    private CircleCollider2D cCollider;
    GameObject cloneBlackHoleParticles;

    void Start()
    {
        SetInitialValues();
        //Obtiene el colider del objeto y lo desabilita
        cCollider = GetComponent<CircleCollider2D>();
        cCollider.isTrigger = true;
        //Esta reapareciando cuando se iniciañliza el enemigo
        isRespawning = true;
        //Añade enemigo al contador global
        GameManager.instance.aliveEnemies += 1;
    }

    void SetInitialValues()
    {
        if (!useInspectorParams)
        {
            lives = Constants.BHlives;
            enemyScore = Constants.BHScore;
            respawnTime = Constants.BHRespawnTime;
        }
    }

    void Update()
    {
        RespawnTimer();
    }

    void RespawnTimer()
    {
        if (respawnTime <= 0)
        {
            cCollider.isTrigger = false;
            isRespawning = false;
        }
        else
        {
            respawnTime -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            HitBH(true);
        }
    }

    void HitBH(bool hitByPlayer = false)
    {
        if (lives == Constants.BHlives)
        {
            //Enables the animation
            gameObject.GetComponent<Animator>().enabled = true;
            //Active the rotation particles
            cloneBlackHoleParticles = Instantiate(blackHoleParticles, transform.position, Quaternion.identity);
            cloneBlackHoleParticles.transform.SetParent(transform);

            gameObject.GetComponent<PointEffector2D>().forceMagnitude = -40f;
            //gameObject.GetComponentInChildren<GameObject>().GetComponent<PointEffector2D>().forceMagnitude = 65f;
        }else if (lives == (Constants.BHlives -1))
        {
            gameObject.GetComponent<PointEffector2D>().forceMagnitude = -45f;
        }
        else if (lives == (Constants.BHlives - 2))
        {
            gameObject.GetComponent<PointEffector2D>().forceMagnitude = -50f;
        }

        lives -= 1;

        if (lives <= 0)
        {
            //Explotion particle system play
            GameObject cloneExplotion = Instantiate(bigExplotion, transform.position, Quaternion.identity);
            Destroy(cloneExplotion, 1.5f);
            DestroyBH(hitByPlayer);
        }
        else
        {
            //Explotion particle system play
            GameObject cloneExplotion = Instantiate(bigExplotion, transform.position, Quaternion.identity);
            Destroy(cloneExplotion, 1.5f);
        }
    }

    public void DestroyBH(bool destroyByPlayer = false)
    {
        //Intancia el score que da el enemigo si fue destruido por el jugador
        if (destroyByPlayer)
        {
            //AddComponentMenu sound
            SoundManager.instance.PlaySound("EnemyExplode");
            //Add Floating points
            PopFloatingPoints();
            if (GameManager.instance.multiplayerChanged)
            {
                PopMultiplierPoints();
            }
            SendToGameManager();
        }

        Destroy(gameObject);
    }

    private void PopFloatingPoints()
    {
        GameObject cloneFloatingPoints = Instantiate(floatingPoints, transform.position, Quaternion.identity);
        int totalEnemyScore = enemyScore * GameManager.instance.multiplier;
        cloneFloatingPoints.transform.GetChild(0).GetComponent<TextMeshPro>().SetText(totalEnemyScore.ToString());
        Destroy(cloneFloatingPoints, 1f);
    }

    private void PopMultiplierPoints()
    {
        // Play Multiplier sound
        SoundManager.instance.PlaySound("Multiplier");
        //Instanciate the UI
        GameObject cloneFloatingPoints = Instantiate(floatingMultiplier, new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z), Quaternion.identity);
        cloneFloatingPoints.transform.GetChild(0).GetComponent<TextMeshPro>().SetText("MULTIPLIER X" + GameManager.instance.multiplier.ToString());
        Destroy(cloneFloatingPoints, 1.3f);
        GameManager.instance.multiplayerChanged = false;
    }

    void SendToGameManager()
    {
        //Add points to player score
        GameManager.instance.aliveEnemies -= 1;
        GameManager.instance.score += enemyScore * GameManager.instance.multiplier;
        GameManager.instance.scoreToMultiplier += enemyScore * GameManager.instance.multiplier;
    }
}
