using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : MonoBehaviour
{

     public abstract double range{ get; }

          public abstract double damage { get; }

     public abstract double cooldown{ get; }
     public abstract double cost{ get; }

    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract bool Cast();


}
