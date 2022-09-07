using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text text;
    public GameObject Player;
    void Update()
    {
        text.text = Convert.ToInt32(Player.transform.position.x).ToString();
    }
}
