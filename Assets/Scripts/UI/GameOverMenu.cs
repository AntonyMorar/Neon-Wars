using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameOverMenu : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    void Update()
    {
        scoreText.SetText("FINAL SCORE: " + GameManager.instance.score.ToString());
    }

    public void GoMainMenu()
    {
        //Click Sound
        SoundManager.instance.PlaySound("UIConfirm");
        //Resete enemies spawn chance and game status
        GameManager.instance.ResetGameStatus();

        UIManager.instance.ShowGameOverMenu(false);
        UIManager.instance.ShowScoreUI(false);
        UIManager.instance.ShowMainMenu(true);
    }
}
