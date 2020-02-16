using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 3.0f;
    Rigidbody2D rigidbody2d;
    public int health { get { return currentHealth; } }
    public int maxHealth = 5;
    int currentHealth;
    float sprintSpeed = 2;
    bool isSprinting = false;
    public GameController gameController;
    public float timeInvincible = 2.0f;
    bool isInvincible;
    bool inMeleeRange = false;
    private float timeSinceAutoAttack;
    public float attackSpeed = 3;
    bool isCharging = false;

    float invincibleTimer;
    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);
    Vector2 targetDirection = new Vector2(1, 0);
    float lastCharge;
    public float chargeEffectRotation;
    public ParticleSystem chargeEffect;

    private GameObject targetEnemy;
    private GameObject rightClickedObject;
    private RaycastHit2D frontmostRaycastHit;
    private SpriteRenderer spriteRenderer;

    public GameObject projectilePrefab;
    public ClickHandler clickHandler;
    public SpellHandler spellHandler;
    public TargetingController targetingController;

    public PlayerClass playerClass;

    EnergyBar energyBar;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        playerClass = GetComponent<Warrior>();
        chargeEffect = GetComponent<ParticleSystem>();
        chargeEffectRotation = GetComponent<ParticleSystem>().shape.rotation.z;
        clickHandler = GetComponent<ClickHandler>();
        spellHandler = GetComponent<SpellHandler>();
        targetingController = GetComponent<TargetingController>();
        timeSinceAutoAttack = attackSpeed;
        lastCharge = Time.time;
        energyBar = transform.Find("pfEnergyBar").Find("ManaBar").GetComponent<EnergyBar>();


    }

    // Update is called once per frame
    void Update()
    {
        checkForSprinting();
        updatePosition();
        updateInvincibleTimer();
        updateTargetDirection();
        updateRange();
        updateCooldowns();

        if (Input.GetKeyDown(KeyCode.C) || isCharging)
        {
            Charge();
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            Cast();
        }
    }

    void moveHorizontal()
    {
        float horizontal = Input.GetAxis("Horizontal");
        Vector2 position = rigidbody2d.position;
        if (isSprinting)
        {
            position.x = position.x + horizontal * Time.deltaTime * speed * sprintSpeed;
        }
        else
        {
            position.x = position.x + horizontal * Time.deltaTime * speed;
        }

    }

    void moveVertical()
    {
        float vertical = Input.GetAxis("Vertical");
        Vector2 position = rigidbody2d.position;
        if (isSprinting)
        {
            position.y = position.y + vertical * Time.deltaTime * speed * sprintSpeed;
        }
        else
        {
            position.y = position.y + vertical * Time.deltaTime * speed;
        }
    }

    float updateRange()
    {
        float distance = -1;
        if (targetEnemy)
        {
            Vector2 heading = targetEnemy.GetComponent<Rigidbody2D>().position - rigidbody2d.position;
            distance = heading.magnitude;
            distance = Math.Abs(Vector2.Distance(targetDirection, rigidbody2d.position));

            if (distance > 0 && distance < .5)
            {
                inMeleeRange = true;
                Debug.Log("In Melee Range");

                if (timeSinceAutoAttack >= attackSpeed)
                {
                    autoAttack();
                }
            }
            else
            {
                inMeleeRange = false;
            }
        }
        return distance;
    }

    void updateCooldowns()
    {
        timeSinceAutoAttack += Time.deltaTime;
    }

    void autoAttack()
    {
        animator.SetTrigger("autoAttack");
        Debug.Log("In autoAttack");
        timeSinceAutoAttack = 0;

        //        animator.SetBool("autoAttack", false);
    }

    void checkForSprinting()
    {
        float sprintCost = 60;
        if (Input.GetButton("sprint"))
        {
            if (energyBar.energy.TrySpendEnergy(sprintCost * Time.deltaTime))
            {
                this.isSprinting = true;
            }
            else
            {
                this.isSprinting = false;
            }
        }
        else
        {
            this.isSprinting = false;
        }
    }

    void updatePosition()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        Vector2 position = rigidbody2d.position;
        if (isSprinting)
        {
            position.x = position.x + horizontal * Time.deltaTime * speed * sprintSpeed;
            position.y = position.y + vertical * Time.deltaTime * speed * sprintSpeed;
        }
        else
        {
            position.x = position.x + horizontal * Time.deltaTime * speed;
            position.y = position.y + vertical * Time.deltaTime * speed;
        }
        rigidbody2d.MovePosition(position);
    }

    public void updatePosition(Vector2 position)
    {

        rigidbody2d.MovePosition(position);
    }

    void updateTargetDirection()
    {

        try
        {
            Vector2 heading = targetEnemy.GetComponent<Rigidbody2D>().position - rigidbody2d.position;
            float distance = heading.magnitude;
            targetDirection = heading / distance; // This is now the normalized direction.
        }
        catch (Exception e)
        {
            targetDirection = Vector2.zero;
        }
    }


    void Charge()
    {
        Vector2 position = rigidbody2d.position;
        if (!isCharging)
        {
            lastCharge = Time.time;
            chargeEffectRotation = Vector2.Angle(position, targetDirection);
            chargeEffect.Play();

        }

        isCharging = true;

        if (isCharging && Time.time - lastCharge < 5)
        {
            if (inMeleeRange)
            {
                isCharging = false;
                chargeEffect.Stop();

                return;
            }
            position = Vector2.MoveTowards(position, targetDirection, speed * Time.deltaTime * sprintSpeed * 2);
            lookDirection.Set(-position.x, -position.y);
            lookDirection.Normalize();


            animator.SetFloat("Look X", lookDirection.x);
            animator.SetFloat("Look Y", lookDirection.y);
            animator.SetFloat("Speed", position.magnitude);
            rigidbody2d.MovePosition(position);


        }
    }

    public void ChangeHealth(int amount)
    {

        if (amount < 0)
        {
            if (isInvincible)
                return;

            isInvincible = true;
            invincibleTimer = timeInvincible;
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log("Health: " + currentHealth + "/" + maxHealth);
    }

    void updateInvincibleTimer()
    {
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }
    }

    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
        Debug.Log(projectileObject);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        Debug.Log(projectile);
        projectile.Launch(targetDirection, 300);

        animator.SetTrigger("Launch");
    }

    void Cast()
    {
        if (targetEnemy)
        {
            targetEnemy.GetComponent<EnemyController>().Fix();
            Debug.Log("cAST ON ENEMY");
        }
        Debug.Log("iNVALID tARGET");

    }

}
