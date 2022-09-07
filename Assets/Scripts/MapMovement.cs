using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMovement : MonoBehaviour
{
    public int Speed;
    void Start()
    {
    }

    void Update()
    {
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(-Speed, 0, 0);

        if (gameObject.transform.rotation.x != 0)
        {
            gameObject.transform.Rotate(0, 0, 0);
        }
        if (gameObject.transform.rotation.y != 0)
        {
            gameObject.transform.Rotate(0, 0, 0);
        }
        if (gameObject.transform.rotation.z != 0)
        {
            gameObject.transform.Rotate(0, 0, 0);
        }
    }

}
