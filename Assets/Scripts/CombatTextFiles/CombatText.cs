using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CombatText : MonoBehaviour
{


    public  string text;
    public  Color color;

    public int fontSize;
    [SerializeField] public Transform pfCombatText;

    private const float DISAPPEAR_TIMER_MAX = 1f;

    private TextMeshPro textMeshPro; 
    private float disappearTimer;

    private static int sortOrder;
    public static CombatText Create(Vector2 position, string text, bool isCriticalHit){
        Transform combatTextTransform =  Instantiate(GameAssets.i.pfCombatText, position, Quaternion.identity);
        CombatText combatText = combatTextTransform.GetComponent<CombatText>();
        combatText.Setup(text, isCriticalHit);

        return combatText;
    }
    private void Awake(){
        textMeshPro = transform.GetComponent<TextMeshPro>();
    }

    public void Setup(string text, bool isCriticalHit){
        textMeshPro.text = text;
        if(isCriticalHit){
        textMeshPro.fontSize = 8;
        textMeshPro.color = Color.red;
            
        }else{
        textMeshPro.fontSize = 5;

        }
        color = textMeshPro.color;
        disappearTimer = DISAPPEAR_TIMER_MAX;
        sortOrder++;
        textMeshPro.sortingOrder = sortOrder;
    }
    // Start is called before the first frame update
    void Start()
    { 
        
    }

    // Update is called once per frame
    void Update()
    {
        if(disappearTimer > DISAPPEAR_TIMER_MAX * .9){
            float increaseScaleAmount = .5f;
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        }else{
            float decreaseScaleAmount = .1f;
            transform.localScale -= Vector3.one *decreaseScaleAmount * Time.deltaTime;
        }
        float moveYSpeed = 2;
        transform.position += new Vector3(0,moveYSpeed) *Time.deltaTime;

        disappearTimer -= Time.deltaTime;
        if(disappearTimer < 0){
            float disappearSpeed = 3f;
            color.a -= disappearSpeed *Time.deltaTime;
            textMeshPro.color =  color;
            if(color.a < 0){
                Destroy(gameObject);
            }
        }
    }


}
