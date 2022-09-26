using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    public void OnButtonClick()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OnMenuButton()
    {
        GameObject.Find("Possibility Manager").GetComponent<PossibiltyHolster>().first = true;
        SceneManager.LoadScene("SampleScene");

    }
}
