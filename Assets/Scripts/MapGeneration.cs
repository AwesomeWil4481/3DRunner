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

    public int chanceOfObstacle = 80;
    //public GameObject SpawnLocation;

    void Start()
    {
        //generatePrefab(-1000);
    }

    void Update()
    {

    }

    GameObject generatePrefab(float currentLocation)
    {
        int sizeOfMap = 0;

        while (sizeOfMap != 20)
        {
            int tileNumber = Random.Range(0, 9);
            int obstacleRoll = Random.Range(1, 100);

            Instantiate(floors[tileNumber], new Vector3(currentLocation, -1, 0), Quaternion.identity);

            if (sizeOfMap % 4 == 0 && obstacleRoll >= chanceOfObstacle)
            {
                tileNumber = Random.Range(0, 9);
                var currentTile = obstacles[tileNumber];
                Instantiate(currentTile, new Vector3(currentLocation, currentTile.transform.position.y, currentTile.transform.position.z), Quaternion.identity);
            }

            currentLocation -= 10;
            sizeOfMap += 1;
        }

        return null;
    }



    GameObject ChosenGameObject()
    {
        int Number = Random.Range(1, 10);

        print(Number);

        if (Number == 1)
        {
            return map1;
        }
        if (Number == 2)
        {
            return map2;
        }
        if (Number == 3)
        {
            return map3;
        }
        if (Number == 4)
        {
            return map4;
        }
        if (Number == 5)
        {
            return map5;
        }
        if (Number == 6)
        {
            return map6;
        }
        if (Number == 7)
        {
            return map7;
        }
        if (Number == 8)
        {
            return map8;
        }
        if (Number == 9)
        {
            return map9;
        }
        if (Number == 10)
        {
            return map10;
        }
        return null;   
    }
    public void OnTriggerExit(Collider other)
    {
        generatePrefab(gameObject.transform.position.x + 290);
        Instantiate (Checkpoint, new Vector3(gameObject.transform.position.x + 200,3,0), Quaternion.identity);
    }
}
