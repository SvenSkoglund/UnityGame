using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : MonoBehaviour
{

    public abstract float range { get; }

    public abstract float damage { get; }

    public abstract float cooldown { get; }
    public abstract float cost { get; }
    public abstract string pathToIcon { get; }

    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public abstract bool Cast();

    public bool isInRange()
    {
        bool isInRange;
        if (range == 0)
        {
            isInRange = true;
        }
        else
        {
            Vector2 playerPosition = player.transform.position;
            Vector2 targetPosition = player.GetComponent<TargetingController>().target.transform.position;
            float distance = Vector2.Distance(playerPosition, targetPosition);

            if (distance <= range)
            {
                isInRange = true;
            }
            else
            {
                isInRange = false;
            }
        }
        return isInRange;
    }

}
