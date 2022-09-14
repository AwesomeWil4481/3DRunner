using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    public GameObject[] floors;

    public GameObject[] obstacles;

    public GameObject parentObject;

    public GameObject Checkpoint;

    public int currentParent = -1;

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

    GameObject generatePrefab(float currentLocation, int _currentParent)
    {
        int sizeOfMap = 0;
        int lastObstacle;
        int sizeOfArray = obstacles.Count() - 1;

        while (sizeOfMap != 20)
        {
            int tileNumber = Random.Range(0, sizeOfArray);
            int obstacleRoll = Random.Range(1, 100);

            var thisTile = Instantiate(floors[Random.Range(0, (floors.Count()) - 1)], new Vector3(currentLocation, -1, 0), Quaternion.identity);
            thisTile.transform.parent = GameObject.Find("Parent Object "+ _currentParent).transform;

            if (sizeOfMap % 4 == 0 && obstacleRoll <= chanceOfObstacle)
            {
                lastObstacle = possibiltyHolster.GetComponent<PossibiltyHolster>().lastPlacedObstacle;
                tileNumber = Random.Range(0, sizeOfArray);
                while (tileNumber == lastObstacle)
                {
                    print("Duplication detected: " + tileNumber);
                    tileNumber = Random.Range(0, sizeOfArray);
                    print("Rerolled: " + tileNumber);
                }
                lastObstacle = tileNumber;
                possibiltyHolster.GetComponent<PossibiltyHolster>().lastPlacedObstacle = lastObstacle;

                var currentTile = obstacles[tileNumber];

                
                var currentObstacle = Instantiate(currentTile, new Vector3(currentLocation, currentTile.transform.position.y, currentTile.transform.position.z), Quaternion.identity);
                currentObstacle.transform.parent = thisTile.transform;          
            }

            currentLocation -= 10;
            sizeOfMap += 1;
        }

        return null;
    }

    public void OnTriggerExit(Collider other)
    {
        int currentDeletion = currentParent;

        if(currentDeletion == 1)
        {
            currentDeletion = 3;
        }
        else
        {
            currentDeletion -= 1;
        }
        if (other.name == "Player")
        {
            foreach(Transform child in GameObject.Find("Parent Object "+ currentDeletion).transform)
            {
                Destroy(child.gameObject);
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            generatePrefab(gameObject.transform.position.x + 290, gameObject.GetComponent<MapGeneration>().currentParent);
            var checkpoint = Instantiate(Checkpoint, new Vector3(gameObject.transform.position.x + 200, 3, 0), Quaternion.identity);

            if (gameObject.GetComponent<MapGeneration>().currentParent == 3)
            {
                checkpoint.GetComponent<MapGeneration>().currentParent = 1;
            }
            else
            {
                checkpoint.GetComponent<MapGeneration>().currentParent = gameObject.GetComponent<MapGeneration>().currentParent + 1;
            }

            checkpoint.transform.parent = GameObject.Find("Parent Object " + gameObject.GetComponent<MapGeneration>().currentParent).transform;
            print("Parent Object " + currentParent);

            other.GetComponent<PlayerMovement>().Speed += 2;
        }
    }
}
