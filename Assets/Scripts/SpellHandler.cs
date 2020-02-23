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
    List<SpellIcon> spellIcons;
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
        generateSpellIcons(spells);
    }

    void generateSpellIcons(List<Spell> spells)
    {
        spellIcons = new List<SpellIcon>();
        for (int i = 0; i < spells.Count; i++)
        {
            spellIcons.Add(SpellIcon.Create(i, spells[i].pathToIcon));
        }
    }

    void checkForSpellCastButtons()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            tryCastingSpellAtIndex(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            tryCastingSpellAtIndex(1);

        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            tryCastingSpellAtIndex(2);

        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            tryCastingSpellAtIndex(3);

        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            tryCastingSpellAtIndex(4);

        }
    }
    void tryCastingSpellAtIndex(int index)
    {
        if (!spellIcons[index].onCooldown)
        {
            if(spells[index].isInRange())
            if (energyBar.energy.TrySpendEnergy(spells[index].cost))
            {
                spells[index].Cast();
                spellIcons[index].startCooldownTimer(spells[index].cooldown);
            }
        }
        else
        {
            notEnoughEnergyAudioSource.Play();
        };
    }

}
