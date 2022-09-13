using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public int Speed = 15;

    bool canJump = true;
    bool canSlide = true;

    public bool Active = true;

    bool activeSlide = false;
    bool activeJump = false;

    public GameObject restartButton;

    void Start()
    {
        
    }

    void Update()
    {
        if (Active)
        {


            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(Speed, 0, 0);

            if (Input.GetKeyDown(KeyCode.UpArrow) && canJump)
            {
                StartCoroutine(Jumping());
            }

            if (Input.GetKeyDown(KeyCode.DownArrow) && canSlide)
            {
                StartCoroutine(Sliding());
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (gameObject.transform.position.z != 3)
                {
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 3);
                }
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (gameObject.transform.position.z != -3)
                {
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + -3);
                }
            }
        }
        else
        {
            restartButton.SetActive(true);
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
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
        activeJump = true;
        activeSlide = false;

        bool clean = true;

        if (canSlide)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, (gameObject.transform.position.y + 1), gameObject.transform.position.z);
        }
        else
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, (gameObject.transform.position.y + 2), gameObject.transform.position.z);
            canSlide = true;
        }
        yield return new WaitForSecondsRealtime(1);
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            clean = false;
        }

        if (activeJump && clean)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, (gameObject.transform.position.y + -1), gameObject.transform.position.z);
            activeJump = false;
        }
        canJump = true;
    }
    IEnumerator Sliding()
    {
        canSlide = false;
        activeSlide = true;
        activeJump = false;

        bool clean = true;

        if (canJump)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, (gameObject.transform.position.y - 1), gameObject.transform.position.z);
        }
        else
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, (gameObject.transform.position.y - 2), gameObject.transform.position.z);
            canJump = true;
        }
        yield return new WaitForSecondsRealtime(1);
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            clean = false;
        }

        if (activeSlide && clean)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, (gameObject.transform.position.y + 1), gameObject.transform.position.z);
            activeSlide = false;
        }
        canSlide = true;
    }
}
