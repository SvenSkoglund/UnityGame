using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Primeable;

public class SpinAttack : Spell, Primeable
{
    public override float range { get { return 0; } }
    public override float damage { get { return 10; } }
    public override float cooldown { get { return 10; } }
    public override float cost { get { return 20; } }
    public override string pathToIcon { get { return "ArtFiles/SpinAttackIcon"; } }

    [SerializeField] public LineRenderer spinEffect;

    public float effectFadeSpeed;
    public float effectAlpha;

    public float radius;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        radius = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }


    public override bool Cast()
    {
        bool wasCast = false;
        SpinAttackEffect.Create(radius);
        effectAlpha = 1f;

        Vector2 playerPosition = player.GetComponent<Rigidbody2D>().position;
        RaycastHit2D[] hits = Physics2D.CircleCastAll(playerPosition, radius, playerPosition);
        foreach (RaycastHit2D hit in hits)
        {

            if (hit != null && hit.collider != null)
            {
                if (hit.collider.gameObject.GetComponent<EnemyController>() != null)
                {
                    // CombatText combatText = hit.collider.gameObject.AddComponent(typeof(CombatText)) as CombatText;
                    // combatText.text= damage.ToString();
                    CombatText.Create(hit.collider.gameObject.transform.position, 100.ToString(), false);
                    hit.collider.gameObject.GetComponent<CombatTextFactory>().addCombatText("Hit Enemy with spin");
                    Debug.Log("Hit Enemy with spin");

                }
            }
        }

        wasCast = true;

        Debug.Log("Cast SpinAttack");
        return wasCast;
    }

    public bool Prime()
    {
        bool wasCast = false;
        return wasCast;
    }

}
