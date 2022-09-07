using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public int Speed = 15;

    bool canJump = true;
    bool canSlide = true;

    bool Active = true;

    void Start()
    {
        
    }

    void Update()
    {
        if (Active)
        {
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(Speed, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && canJump)
        {
            StartCoroutine(Jumping());
        }

        if (Input.GetKeyDown (KeyCode.DownArrow) && canSlide)
        {
            StartCoroutine(Sliding()); 
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Active = false;
        //gameObject.GetComponent<Rigidbody>().velocity = new Vector3((gameObject.transform.position.x + -1), 0, 0);
    }

    IEnumerator Jumping()
    {
        canJump = false;
        canSlide = false;
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, (gameObject.transform.position.y +1),gameObject.transform.position.z);
        yield return new WaitForSecondsRealtime(1);
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, (gameObject.transform.position.y + -1), gameObject.transform.position.z);
        canJump = true;
        canSlide = true;
    }
    IEnumerator Sliding()
    {
        canSlide = false;
        canJump = false;
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, (gameObject.transform.position.y + -1), gameObject.transform.position.z);
        yield return new WaitForSecondsRealtime(1);
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, (gameObject.transform.position.y + 1), gameObject.transform.position.z);
        canSlide = true;
        canJump = true;
    }
}
