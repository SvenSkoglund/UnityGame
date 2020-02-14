using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerClass : MonoBehaviour
{

     public abstract string className{ get; }
     public abstract string classDescription{ get; }
     public abstract double HP { get; }
     public abstract double energy{ get; }
     public abstract double stamina{ get; }
     public abstract double strength{ get; }
     public abstract double agility{ get; }
     public abstract double willpower{ get; }
     public abstract double armor{ get; }
     public abstract List<Spell> getSpellsForClass();


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Initialized class - " + className);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
