using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Primeable;

public class Dash : Spell, Primeable
{
    public override double range { get { return 10; } }
    public override double damage { get { return 10; } }
    public override double cooldown { get { return 10; } }
    public override double cost { get { return 10; } }

    [SerializeField] public LineRenderer dashEffect;

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
        UpdateLineColor();

    }

    public void UpdateLineColor()
    {
        if (dashEffect != null)
        {

            effectAlpha -= Time.deltaTime * effectFadeSpeed;
            Color start = Color.white;
            start.a = effectAlpha;
            Color end = Color.red;
            end.a = effectAlpha;
            dashEffect.startColor = start;
            dashEffect.endColor = end;
        }
    }

    public override bool Cast()
    {
        bool wasCast = false;
        effectAlpha = 1f;

        Vector2 mousePosition = player.GetComponent<ClickHandler>().getMousePosition();
        Vector2 playerPosition = player.GetComponent<Rigidbody2D>().position;
        RaycastHit2D[] hits = Physics2D.LinecastAll(playerPosition, mousePosition);
        foreach (RaycastHit2D hit in hits)
        {

            if (hit != null && hit.collider != null)
            {
                if (hit.collider.gameObject.GetComponent<EnemyController>() != null)
                {
                    // CombatText combatText = hit.collider.gameObject.AddComponent(typeof(CombatText)) as CombatText;
                    // combatText.text= damage.ToString();
                    CombatText.Create(hit.collider.gameObject.transform.position, 100.ToString(), false);
                    StatusText.Create(hit.collider.gameObject.transform.position, "Stunned", false);
                    hit.collider.gameObject.GetComponent<CombatTextFactory>().addCombatText("Hit Enemy with dash");
                    Debug.Log("Hit Enemy with dash");

                }
            }
        }

        player.GetComponent<Rigidbody2D>().position = mousePosition;
        DashEffect.Create(playerPosition, mousePosition);
        wasCast = true;

        Debug.Log("Cast Dash");
        return wasCast;
    }
    public void generateEffect(Vector2 startPosition, Vector2 endPosition)
    {
        // dashEffect = Instantiate(GameAssets.i.dashEffect, gameObject.transform.position, Quaternion.identity);
        dashEffect.sortingLayerName = "Bottom";
        dashEffect.sortingOrder = 0;
        dashEffect.startWidth = .1f;
        dashEffect.endWidth = .5f;
        dashEffect.useWorldSpace = true;
        dashEffect.startColor = Color.white;
        dashEffect.endColor = Color.red;
        dashEffect.material = new Material(Shader.Find("Sprites/Default"));
        effectFadeSpeed = .01f;
        effectAlpha = 1f;
        Vector3[] positions = new Vector3[2];
        positions[0] = startPosition;
        positions[1] = endPosition;
        dashEffect.SetPositions(positions);
        effectAlpha = 1f;


    }
    public bool Prime()
    {
        bool wasCast = false;
        return wasCast;
    }

}
