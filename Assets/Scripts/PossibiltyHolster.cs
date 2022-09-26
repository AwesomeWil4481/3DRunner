using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossibiltyHolster : MonoBehaviour
{
    public int lastPlacedObstacle = -1;
    public bool first = true;

    public PossibiltyHolster instance;

    public void Awake()
    {
        if (GameObject.Find("Possibility Manager").GetComponent<PossibiltyHolster>().instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
