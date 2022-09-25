using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public int Speed = 15;

    bool canJump = true;
    bool canSlide = true;

    public bool Active = false;
    public bool Started = false;
    public bool EncounteredObs = false;

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
        if (!Started)
        {
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(Speed, 0, 0);
        }
        if (Active)
        {
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(Speed, 0, 0);

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
        else
        {
            if (Started)
            {
                restartButton.SetActive(true);
                gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            }
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
