using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("Events")]
    public GameObject eventSystem;

    [Header("Menu Canvas")]
    public GameObject scoreUI;
    public GameObject gameOverMenu;
    public GameObject mainMenu;
    public GameObject controlsMenu;
    public GameObject creditsMenu;

    [Header ("Particles")]
    public GameObject fireworkParty;

    [Header("First Menu Buttons")]
    public GameObject firstButtonMainMenu;
    public GameObject firstButtonGameOver;
    public GameObject firstButtonControls;
    public GameObject firstButtonCredits;

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
        if (controlsMenu.activeSelf)
        {
            ShowControlsMenu(false);
        }
        if (creditsMenu.activeSelf)
        {
            ShowCreditsMenu(false);
        }
        if (!fireworkParty.activeSelf)
        {
            fireworkParty.SetActive(true);
        }


        //Active the firts button in the game
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

    public void ShowControlsMenu(bool show = true)
    {
        controlsMenu.SetActive(show);
        if (show)
        {
            eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(firstButtonControls);
        }
    }

    public void ShowCreditsMenu(bool show = true)
    {
        creditsMenu.SetActive(show);
        if (show)
        {
            eventSystem.GetComponent<EventSystem>().SetSelectedGameObject(firstButtonCredits);
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

    public void GoMainMenu()
    {
        //Click Sound
        SoundManager.instance.PlaySound("UIConfirm");
        //Resete enemies spawn chance and game status
        GameManager.instance.ResetGameStatus();

        ShowGameOverMenu(false);
        ShowCreditsMenu(false);
        ShowControlsMenu(false);
        ShowScoreUI(false);
        ShowMainMenu(true);
    }


    public void GoCredits()
    {
        //Click Sound
        SoundManager.instance.PlaySound("UIConfirm");
        ShowMainMenu(false);
        ShowCreditsMenu(true);
    }

    public void GoControls()
    {
        //Click Sound
        SoundManager.instance.PlaySound("UIConfirm");
        ShowMainMenu(false);
        ShowControlsMenu(true);
    }
}
