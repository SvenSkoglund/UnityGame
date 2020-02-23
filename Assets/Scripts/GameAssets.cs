using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _i;

    public static GameAssets i{
        get{
            if(_i == null) _i = (Instantiate(Resources.Load("GameAssets")) as GameObject).GetComponent<GameAssets>();
            return _i;
        }
    }

    public Transform pfCombatText;

    public Transform pfSpellIcon;

    public ClickHandler clickHandler;

    public Transform dashEffect;

    public Transform rageEffect;
    public Transform spinEffect;
    public Transform cutEffect;
    public Transform crippleEffect;

}
