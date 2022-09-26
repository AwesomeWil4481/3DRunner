using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text text;
    public Text hText;
    public GameObject Player;

    public float startPos;

    public bool shouldCount = false;

    bool saved = false;
    void Update()
    {
        int currentScore = Convert.ToInt32(Player.transform.position.x - startPos);
        int finalScore = 0;
        if (Player.GetComponent<PlayerMovement>().gameState == PlayerMovement.PlayerState.Menu || !shouldCount)
        {
            startPos = Player.transform.position.x;
        }
        else if (Player.GetComponent<PlayerMovement>().gameState == PlayerMovement.PlayerState.Playing)
        {
            if (shouldCount)
            {
                text.text = Convert.ToInt32(currentScore).ToString();
                if (PlayerPrefs.GetFloat("High Score") <= currentScore)
                {
                    hText.text = currentScore.ToString();
                }
            }
        }
        else if (Player.GetComponent<PlayerMovement>().gameState == PlayerMovement.PlayerState.Dead) 
        {
            if (!saved)
            {
                finalScore = currentScore;
                saveHighScore(Convert.ToInt32(finalScore));
                saved = true;
            }
        }
    }



    public void saveHighScore(int score)
    {
        if (score >= PlayerPrefs.GetFloat("High Score"))
        {
            PlayerPrefs.SetFloat("High Score", score);
            PlayerPrefs.Save();
        }
    }
}
