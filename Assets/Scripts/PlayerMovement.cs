using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public int Speed = 15;

    bool canJump = true;
    bool canSlide = true;

    public enum PlayerState 
    {
        Menu, 
        Playing, 
        Dead
    }

    public PlayerState gameState;

    //public bool Active = false;
    //public bool Started = false;
    //public bool EncounteredObs = false;

    bool activeSlide = false;
    bool activeJump = false;

    bool allowed = true;

    public GameObject restartButton;

    Vector2 fingerDown;
    Vector2 fingerUp;

    public float swipeThreshold = 20f;

    void checkSwipe()
    {
        if (verticalMove() > swipeThreshold && verticalMove() > horizontalValMove())
        {
            if (fingerDown.y - fingerUp.y > 0 && canJump)//up swipe
            {
                StartCoroutine(Jumping());
            }
            else if (fingerDown.y - fingerUp.y < 0 && canSlide)//Down swipe
            {
                StartCoroutine(Sliding());
            }
            fingerUp = fingerDown;
        }

        else if (horizontalValMove() > swipeThreshold && horizontalValMove() > verticalMove())
        {
            if (fingerDown.x - fingerUp.x > 0 && allowed)//Right swipe
            {
                allowed = false;
                if (gameObject.transform.position.z >= -1)
                {
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + -3);
                }
            }
            else if (fingerDown.x - fingerUp.x < 0 && allowed)//Left swipe
            {
                allowed = false;
                if (gameObject.transform.position.z <= 1)
                {
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 3);
                }
            }
            fingerUp = fingerDown;
        }
    }

    float verticalMove()
    {
        return Mathf.Abs(fingerDown.y - fingerUp.y);
    }

    float horizontalValMove()
    {
        return Mathf.Abs(fingerDown.x - fingerUp.x);
    }


    void Update()
    {
        if (gameState == PlayerState.Menu || gameState == PlayerState.Playing)
        {
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(Speed, 0, 0);
            if (gameState == PlayerState.Playing)
            {
                foreach (Touch touch in Input.touches)
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        fingerDown = touch.position;
                        fingerUp = touch.position;
                    }
                    if (touch.phase == TouchPhase.Moved)
                    {
                        fingerDown = touch.position;
                        checkSwipe();
                    }
                    if (touch.phase == TouchPhase.Ended)
                    {
                        allowed = true;
                    }
                }



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

                    if (gameObject.transform.position.z <= 3)
                    {
                        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 3);
                    }
                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    if (gameObject.transform.position.z >= -3)
                    {
                        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + -3);
                    }
                }
            }
        }
        else if (gameState == PlayerState.Dead)
        {
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            restartButton.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        gameState = PlayerState.Dead;
        //gameObject.GetComponent<Rigidbody>().velocity = new Vector3((gameObject.transform.position.x + -1), 0, 0);
    }

    IEnumerator resetPosition()
    {
        yield return new WaitForSecondsRealtime(1);

        gameObject.transform.position = new Vector3(gameObject.transform.position.x, (2.4f), gameObject.transform.position.z);
        canJump = true;
        canSlide = true;
    }

    IEnumerator Jumping()
    {
        canJump = false;
        activeJump = true;
        activeSlide = false;

        if (canSlide)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, (gameObject.transform.position.y + 1), gameObject.transform.position.z);
            StopAllCoroutines();
        }
        else
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, (gameObject.transform.position.y + 2), gameObject.transform.position.z);
            StopAllCoroutines();

            canSlide = true;
        }
        StartCoroutine("resetPosition");

        yield return new WaitForSecondsRealtime(0);
    }
    IEnumerator Sliding()
    {
        canSlide = false;
        activeSlide = true;
        activeJump = false;

        if (canJump)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, (gameObject.transform.position.y - 1), gameObject.transform.position.z);
            StopAllCoroutines();
        }
        else
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, (gameObject.transform.position.y - 2), gameObject.transform.position.z);
            StopAllCoroutines();

            canJump = true;
        }
        StartCoroutine(resetPosition());

        yield return new WaitForSecondsRealtime(0);
    }
}
