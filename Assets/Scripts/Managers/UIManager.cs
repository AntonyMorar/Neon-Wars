using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("Menu Status")]
    public GameObject scoreUI;
    public GameObject gameOverMenu;
    public GameObject mainMenu;

    private void Awake()
    {
        MakeSingleton();
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
        if (!mainMenu.activeSelf)
        {
            ShowMainMenu(true);
        }
        if (scoreUI.activeSelf)
        {
            ShowScoreUI(false);
        }
        if (gameOverMenu.activeSelf)
        {
            ShowGameOverMenu(false);
        }
    }

    private void Update()
    {
        if (GameManager.instance.gameOver)
        {
            ShowGameOverMenu(true);
        }
    }

    public void ShowGameOverMenu(bool show = true)
    {
        gameOverMenu.SetActive(show);
    }

    public void ShowMainMenu(bool show = true)
    {
        mainMenu.SetActive(show);
    }

    public void ShowScoreUI(bool show = true)
    {
        scoreUI.SetActive(show);
    }
}
