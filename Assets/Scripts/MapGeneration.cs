using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    public GameObject[] floors;

    public GameObject[] obstacles;

    public GameObject map1;
    public GameObject map2;
    public GameObject map3;
    public GameObject map4;
    public GameObject map5;
    public GameObject map6;
    public GameObject map7;
    public GameObject map8;
    public GameObject map9;
    public GameObject map10;

    public GameObject Checkpoint;

    GameObject possibiltyHolster;

    public int chanceOfObstacle = 80;
    //public GameObject SpawnLocation;

    void Start()
    {
        possibiltyHolster = GameObject.Find("Possibility Manager");
    }

    void Update()
    {

    }

    GameObject generatePrefab(float currentLocation)
    {
        int sizeOfMap = 0;
        int lastObstacle;

        while (sizeOfMap != 20)
        {
            int tileNumber = Random.Range(0, 7);
            int obstacleRoll = Random.Range(1, 100);

            Instantiate(floors[tileNumber], new Vector3(currentLocation, -1, 0), Quaternion.identity);

            if (sizeOfMap % 4 == 0 && obstacleRoll <= chanceOfObstacle)
            {
                lastObstacle = possibiltyHolster.GetComponent<PossibiltyHolster>().lastPlacedObstacle;
                tileNumber = Random.Range(0, 7);
                while(tileNumber == lastObstacle)
                {
                    print("Duplication detected: " + tileNumber);
                    tileNumber = Random.Range(0, 7);
                    print("Rerolled: " + tileNumber);
                }
                lastObstacle = tileNumber;
                possibiltyHolster.GetComponent<PossibiltyHolster>().lastPlacedObstacle = lastObstacle;

                var currentTile = obstacles[tileNumber];
                Instantiate(currentTile, new Vector3(currentLocation, currentTile.transform.position.y, currentTile.transform.position.z), Quaternion.identity);
            }

            currentLocation -= 10;
            sizeOfMap += 1;
        }

        return null;
    }

    public void OnTriggerExit(Collider other)
    {
        generatePrefab(gameObject.transform.position.x + 290);
        Instantiate (Checkpoint, new Vector3(gameObject.transform.position.x + 200,3,0), Quaternion.identity);

        other.GetComponent<PlayerMovement>().Speed += 2;
    }
}
