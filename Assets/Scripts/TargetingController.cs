using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingController : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    public GameObject target;
    public GameObject targetIcon;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        targetIcon = GameObject.Find("TargetIcon");
    }

    // Update is called once per frame
    void Update()
    {
        updateTargetIconPosition();
    }

    public void changeTarget(Vector2 position)
    {
      //  position.y += 2;
        rigidbody2d.position = position;
    }

    void updateTargetIconPosition()
    {       
        if(target != null) { 
            Vector2 targetPosition = target.GetComponent<Rigidbody2D>().position;
            if (targetPosition != null)
            {
               //  targetPosition.y += 2;
                targetIcon.GetComponent<Rigidbody2D>().position = targetPosition;
            }
        }else {

        }
    }

    void ChooseTargetWithKey()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            target = GameObject.Find("GameController").GetComponent<GameController>().enemy1;
            Debug.Log("Targeted: "+target.name);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            target = GameObject.Find("GameController").GetComponent<GameController>().enemy2;
            Debug.Log("Targeted: "+target.name);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            target = GameObject.Find("GameController").GetComponent<GameController>().enemy3;
            Debug.Log("Targeted: "+target.name);
        }
    }
}
