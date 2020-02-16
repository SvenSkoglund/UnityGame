using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : MonoBehaviour
{

     public abstract float range{ get; }

          public abstract float damage { get; }

     public abstract float cooldown{ get; }
     public abstract float cost{ get; }

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
