using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        //Game Satart Sound
        SoundManager.instance.PlaySound("GameStart");
        //Change music
        SoundManager.instance.PauseSound("MenuSoundtrack");
        SoundManager.instance.PlaySound("GameSoundtrack");

        //Change UI Canvas
        UIManager.instance.ShowMainMenu(false);
        UIManager.instance.ShowScoreUI(true);
        // Change game master parameters
        GameManager.instance.gameStart = true;
    }
}
