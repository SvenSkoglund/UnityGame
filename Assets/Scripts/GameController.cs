using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;

    // Start is called before the first frame update
    void Start()
    {
        enemy1 = GameObject.Find("enemy1");
        enemy2 = GameObject.Find("enemy2");
        enemy3 = GameObject.Find("enemy3");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
