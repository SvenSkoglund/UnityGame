using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Primeable;

public class Rage : Spell
{
    public override float range { get { return 0; } }
    public override float damage { get { return 0; } }
    public override float cooldown { get { return 10; } }
    public override float cost { get { return 0; } }
    public override string pathToIcon { get { return "ArtFiles/RageIcon"; } }


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");

    }

    // Update is called once per frame
    void Update()
    {


    }


    public override bool Cast()
    {
        bool wasCast = false;
         createRageEffect();

        return wasCast;
    }

    void createRageEffect()
    {
        Vector3 position = player.transform.position;
        position.y += 2;
        RageEffect.Create(position);

    }

}
