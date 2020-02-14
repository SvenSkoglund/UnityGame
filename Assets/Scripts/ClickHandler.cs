using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class ClickHandler : MonoBehaviour
{
    GameObject player;
    TargetingController targetingController;

    // Start is called before the first frame update
    void Start()
    {
        player =  GameObject.Find("Player");
        targetingController = player.GetComponent<TargetingController>();
    }

    // Update is called once per frame
    void Update()
    {
        checkForClicks();
    }

    void checkForClicks()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LeftClick();
        }
        // If the right mouse button is clicked anywhere...
        else if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Right Clicked");
        }
    }
    void LeftClick ()
    {
                    Debug.Log("Left Clicked");

    // Cast a ray straight down.
    Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    RaycastHit2D[] hits = Physics2D.RaycastAll(clickPosition, clickPosition);
                            Debug.Log("mouse position x: "+clickPosition.x);
                            Debug.Log("mouse position y: "+clickPosition.y);


        if (hits.Length != 0){
                        Debug.Log("Target hits found");

            ChooseTargetWithClick(hits);
        }
    }

    public Vector2 getMousePosition(){
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    void  ChooseTargetWithClick(RaycastHit2D[] hits)
    {
        foreach(RaycastHit2D hit in hits)
        {
            if (hit.collider.gameObject.GetComponent<EnemyController>() != null)
            {
                targetingController.target = hit.collider.gameObject;
                Debug.Log("Target set");

            };

        }
    }
}
