using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : PlayerClass
{


    public override string className { get { return "Warrior"; } }
    public override string classDescription { get { return "A beefy bruiser"; } }
    public override double HP { get { return 10; } }
    public override double energy { get { return 10; } }
    public override double stamina { get { return 10; } }
    public override double strength { get { return 10; } }
    public override double agility { get { return 10; } }
    public override double willpower { get { return 10; } }
    public override double armor { get { return 10; } }


    Dash dash;

    Rage rage;

    SpinAttack spinAttack;

    Cut cut;

    Cripple cripple;
    // Start is called before the first frame update
    void Start()
    {
        initializeSpells();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void initializeSpells()
    {
        dash = gameObject.AddComponent(typeof(Dash)) as Dash;
        rage = gameObject.AddComponent(typeof(Rage)) as Rage;
        spinAttack = gameObject.AddComponent(typeof(SpinAttack)) as SpinAttack;
        cut = gameObject.AddComponent(typeof(Cut)) as Cut;
        cripple = gameObject.AddComponent(typeof(Cripple)) as Cripple;

    }
    public override List<Spell> getSpellsForClass()
    {
        initializeSpells();
        List<Spell> spellsForClass = new List<Spell>();
        spellsForClass.Add(dash);
        spellsForClass.Add(rage);
        spellsForClass.Add(spinAttack);
        spellsForClass.Add(cut);
        spellsForClass.Add(cripple);
        return spellsForClass;
    }

}
