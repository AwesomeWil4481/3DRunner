using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    public GameObject floor1;
    public GameObject floor2;
    public GameObject floor3;
    public GameObject floor4;
    public GameObject floor5;
    public GameObject floor6;
    public GameObject floor7;
    public GameObject floor8;
    public GameObject floor9;
    public GameObject floor10;

    //public GameObject SpawnLocation;

    void Start()
    {
        //Instantiate(ChosenGameObject(), SpawnLocation.transform.position, Quaternion.identity);
    }

    void Update()
    {

    }
    GameObject ChosenGameObject()
    {
        int Number = Random.Range(1, 10);

        if (Number == 1)
        {
            return floor1;
        }
        if (Number == 2)
        {
            return floor2;
        }
        if (Number == 3)
        {
            return floor3;
        }
        if (Number == 4)
        {
            return floor4;
        }
        if (Number == 5)
        {
            return floor5;
        }
        if (Number == 6)
        {
            return floor6;
        }
        if (Number == 7)
        {
            return floor7;
        }
        if (Number == 8)
        {
            return floor8;
        }
        if (Number == 9)
        {
            return floor9;
        }
        if (Number == 10)
        {
            return floor10;
        }
        return null;   
    }
    public void OnTriggerExit(Collider other)
    {
        Instantiate(ChosenGameObject(), new Vector3 ((gameObject.transform.position.x + 100), -1 ,0 ), Quaternion.identity);
    }
}
