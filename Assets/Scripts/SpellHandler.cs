using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellHandler : MonoBehaviour
{
    GameObject player;
    TargetingController targetingController;

    public AudioClip notEnoughEnergyAudioClip;

    public AudioSource notEnoughEnergyAudioSource;

    List<Spell> spells;
    EnergyBar energyBar;
    bool hasSpellPrimed;

    // Start is called before the first frame update
    void Start()
    {
        hasSpellPrimed = false;
        player = GameObject.Find("Player");
        targetingController = player.GetComponent<TargetingController>();
        energyBar = Camera.main.GetComponentInChildren<EnergyBar>();

        AudioSource notEnoughEnergyAudioSource = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        notEnoughEnergyAudioSource.clip = notEnoughEnergyAudioClip;


        populateSpells();
    }

    // Update is called once per frame
    void Update()
    {
        checkForSpellCastButtons();
    }

    void populateSpells()
    {
        spells = player.GetComponent<PlayerClass>().getSpellsForClass();
    }


    void checkForSpellCastButtons()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (energyBar.energy.TrySpendEnergy(spells[0].cost))
            {
                spells[0].Cast();
            }
            else
            {
                notEnoughEnergyAudioSource.Play();
            };
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (energyBar.energy.TrySpendEnergy(spells[1].cost))
            {
                spells[1].Cast();
            }
            else
            {
                notEnoughEnergyAudioSource.Play();
            };
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {

        }
    }

}
