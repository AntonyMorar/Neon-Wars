using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        //Click Sound
        SoundManager.instance.PlaySound("UIConfirm");
        //Game Satart Sound
        SoundManager.instance.PlaySound("GameStart");
        //Change music
        SoundManager.instance.PauseSound("MenuSoundtrack");
        SoundManager.instance.PlaySound("GameSoundtrack");

        //Change UI Canvas
        UIManager.instance.ShowMainMenu(false);
        UIManager.instance.ShowScoreUI(true);
        UIManager.instance.ShowFireworkParty(false);
        // Change game master parameters
        GameManager.instance.gameStart = true;
    }
}
