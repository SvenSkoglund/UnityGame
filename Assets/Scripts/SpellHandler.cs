using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class SpellHandler : MonoBehaviour
{
    GameObject player;
    TargetingController targetingController;

    List<Spell> spells;

    bool hasSpellPrimed;

    // Start is called before the first frame update
    void Start()
    { 
        hasSpellPrimed = false;
        player =  GameObject.Find("Player");
        targetingController = player.GetComponent<TargetingController>();
        populateSpells();
    }

    // Update is called once per frame
    void Update()
    {
        checkForSpellCastButtons();
    }

    void populateSpells(){
       spells = player.GetComponent<PlayerClass>().getSpellsForClass();
    }

    
    void checkForSpellCastButtons()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            spells[0].Cast();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {

        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {

        }
    }

}
