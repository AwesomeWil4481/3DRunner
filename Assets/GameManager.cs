using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject checkpoint;
    public GameObject Player;

    public int speedInc = 0;
    public int obsChance = 0;

    public void Start()
    {
        checkpoint.GetComponent<MapGeneration>().toIncSpeed = 0;
        checkpoint.GetComponent<MapGeneration>().chanceOfObstacle = 0;
        speedInc = 0;
        obsChance = 0;
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void StartButton(GameObject gameObject)
    {
        speedInc = 2;
        obsChance = 100;
        Player.GetComponent<PlayerMovement>().Active = true;
        Player.GetComponent<PlayerMovement>().Started = true;
        gameObject.SetActive(false);
    }
}