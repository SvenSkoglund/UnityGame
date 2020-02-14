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
        if(combatTextStack.Count > 0){

        foreach(CombatText combatText in combatTextStack){
            GameObject UItextGO = new GameObject("Text2");
            UItextGO.transform.SetParent(canvas);

            RectTransform trans = UItextGO.AddComponent<RectTransform>();
            trans.anchoredPosition = canvas.position;

            Text text = UItextGO.AddComponent<Text>();
            text.text = combatText.text;
            text.fontSize = combatText.fontSize;
            text.color = combatText.color;
        }
        }
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
