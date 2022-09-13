using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Highscore : MonoBehaviour
{
    public float highScore;
    public Text highScoreText;

    public void Start()
    {
        highScore = PlayerPrefs.GetFloat("High Score");
        highScoreText.text = highScore.ToString();
    }
}
