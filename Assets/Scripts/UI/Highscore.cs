using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Highscore : MonoBehaviour
{
    private TextMeshProUGUI highscoreText;

    private void Start()
    {
        highscoreText = GetComponent<TextMeshProUGUI>();
        highscoreText.SetText(GameManager.instance.highScore.ToString());
    }

    void Update()
    {
        highscoreText.SetText(GameManager.instance.highScore.ToString());
    }
}
