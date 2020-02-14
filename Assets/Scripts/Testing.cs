 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{

    [SerializeField] private Transform pfDashDeffect;
    void Start()
    {

        // CombatText.Create(Vector2.zero, "TEST"); 
        Instantiate (pfDashDeffect, Vector3.zero, Quaternion.identity);   
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            bool isCriticalHit = Random.RandomRange(0 ,100) < 30;
            CombatText.Create(Camera.main.ScreenToWorldPoint(Input.mousePosition),"TEST", isCriticalHit);
        }
    }
}
