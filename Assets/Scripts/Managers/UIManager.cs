using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("Events")]
    public GameObject eventSystem;

    [Header("Menu Status")]
    public GameObject scoreUI;
    public GameObject gameOverMenu;
    public GameObject mainMenu;
    public GameObject fireworkParty;

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
        if (!fireworkParty.activeSelf)
        {
            fireworkParty.SetActive(true);
        }

        //Active Firework party
        ShowFireworkParty(true);
    }

    private void Update()
    {

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

    public void ShowFireworkParty(bool show = true)
    {
        fireworkParty.GetComponent<FireworkParty>().isActiveParty = show;
    }
}
