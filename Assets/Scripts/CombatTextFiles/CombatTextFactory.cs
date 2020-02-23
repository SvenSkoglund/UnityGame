using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public  class CombatTextFactory : MonoBehaviour
{
    Transform canvas;

    List <CombatText> combatTextStack;

    // Start is called before the first frame update
    void Start()
    { 
        combatTextStack = new List<CombatText>(); 
        // canvas = this.transform.parent.transform;
        canvas = GameObject.Find(this.name).transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        displayCombatText();
    }

    void displayCombatText(){

    }
    public void addCombatText(CombatText combatText){
        combatTextStack.Add(combatText);
    }
    public void addCombatText(string text){
        StatusText combatText = gameObject.AddComponent(typeof(StatusText)) as StatusText;
        combatText.text = text;
        combatTextStack.Add(combatText);
    }
}
