using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public bool vertical;
    public float changeTime = 3.0f;

    Rigidbody2D rigidbody2D;

    BoxCollider2D collider;

    float timer;
    int direction = 1;
    Animator animator;
    public ParticleSystem smokeEffect;
    public ParticleSystem targetedEffect;

    public CombatTextFactory combatTextFactory;

    bool broken;

    // Start is called before the first frame update
    void Start()
    {
        combatTextFactory = gameObject.AddComponent(typeof(CombatTextFactory)) as CombatTextFactory;
        rigidbody2D = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
        broken = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!broken)
        {
		    return;
        }
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }

        Vector2 position = rigidbody2D.position;

        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed;
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }

        rigidbody2D.MovePosition(position);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }

    public void Fix()
    {
        broken = false;
        rigidbody2D.simulated = false;
        animator.SetTrigger("Fixed");
        smokeEffect.Stop();
    }

    public override string ToString()
    {
        Debug.Log("Enemy");
        return "Enemy";
    }
}
