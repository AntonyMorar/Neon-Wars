using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePool : MonoBehaviour
{
    public GameObject lifeIconPrefab;

    [SerializeField]
    private int lives;
    [SerializeField]
    private bool liveChange;

    private void Start()
    {
        lives = GameManager.instance.lives;
        RestartLives();
    }

    private void Update()
    {
        if (lives != GameManager.instance.lives)
        {
            liveChange = true;
        }

        if (liveChange)
        {
            if (lives - 1 == GameManager.instance.lives)
            {
                RemoveLife();
                lives = GameManager.instance.lives;
            }
            else if (lives + 1 == GameManager.instance.lives)
            {
                AddLife();
                lives = GameManager.instance.lives;
            }
            else
            {
                RestartLives();
            }
            liveChange = false;
        }
    }

    private void AddLife()
    {
        Instantiate(lifeIconPrefab, transform);
    }

    private void RemoveLife()
    {
        if (transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
    }

    private void RestartLives()
    {
        lives = GameManager.instance.lives;

        if (transform.childCount > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }

        for (int i = 0; i < GameManager.instance.lives - 1; i++)
        {
            AddLife();
        }
    }
}

