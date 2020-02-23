using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Primeable;

public class Cripple : Spell
{
    public override float range { get { return 1; } }
    public override float damage { get { return 10; } }
    public override float cooldown { get { return 10; } }
    public override float cost { get { return 20; } }
    public override string pathToIcon { get { return "ArtFiles/CrippleIcon"; } }

    [SerializeField] public LineRenderer crippleEffect;

    public float effectFadeSpeed;
    public float effectAlpha;

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
        effectAlpha = 1f;

        Vector2 playerPosition = player.GetComponent<Rigidbody2D>().position;
        GameObject target = player.GetComponent<TargetingController>().target;
        Vector2 targetPosition = target.transform.position;

        if (Vector2.Distance(playerPosition, targetPosition) <= range)
        {
            CrippleEffect.Create(target);
        }

        wasCast = true;

        Debug.Log("Cast CrippleAttack");
        return wasCast;
    }

    public bool Prime()
    {
        bool wasCast = false;
        return wasCast;
    }

}
