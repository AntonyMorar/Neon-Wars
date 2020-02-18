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

    [Header("First Menu Buttons")]
    public GameObject firstButtonMainMenu;
    public GameObject firstButtonGameOver;

    private GameObject currenSelected;

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
        //Active main menu and deactive other menus
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

        if (eventSystem.GetComponent<EventSystem>().firstSelectedGameObject == null)
        {
            eventSystem.GetComponent<EventSystem>().firstSelectedGameObject = firstButtonMainMenu;
            currenSelected = firstButtonMainMenu;
        }
        else
        {
            currenSelected = firstButtonMainMenu;
        }

        //Active Firework party
        ShowFireworkParty(true);
    }

    private void Update()
    {
        if (currenSelected != eventSystem.GetComponent<EventSystem>().currentSelectedGameObject)
        {
            SoundManager.instance.PlaySound("UIChange");
            currenSelected = eventSystem.GetComponent<EventSystem>().currentSelectedGameObject;
        }
    }

    public void ShowMainMenu(bool show = true)
    {
        mainMenu.SetActive(show);
        if (show)
        {

            eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(firstButtonMainMenu);
        }
    }

    public void ShowGameOverMenu(bool show = true)
    {
        gameOverMenu.SetActive(show);
        if (show)
        {
            eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(firstButtonGameOver);
        }
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
