using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpellIcon : MonoBehaviour
{

    public TextMeshPro cooldownTimer;
    public Image cooldownShading;
    public float cooldownShadingDivisor;

    private float cooldownTime;
    public bool onCooldown;

    public static SpellIcon Create(int index, string pathToIcon){
        Transform spellIconBarTransform = Camera.main.transform.Find("pfSpellIconBar").GetChild(index).transform;
        Transform spellIconTransform =  Instantiate(GameAssets.i.pfSpellIcon, spellIconBarTransform.position, Quaternion.identity);
        SpellIcon spellIcon = spellIconTransform.GetComponent<SpellIcon>();
        Sprite iconImage =  Resources.Load<Sprite>(pathToIcon);
        spellIconTransform.Find("Icon").GetComponent<Image>().sprite = iconImage;

        spellIconTransform.SetParent(spellIconBarTransform);
        // combatText.Setup(text, isCriticalHit);
        return spellIcon;
    }
    void Start()
    {
        cooldownTimer = transform.Find("CooldownTimer").GetComponent<TextMeshPro>();
        cooldownShading = transform.Find("Shading").GetComponent<Image>();
        onCooldown = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (onCooldown)
        {
            updateCooldownTimerAndBackground();
        }
    }

    void updateCooldownTimerAndBackground()
    {
        if (cooldownTime <= 0)
        {
            onCooldown = false;
            cooldownTimer.text = "";
            cooldownShading.fillAmount = 0;
            return;
        }
        cooldownTime -= Time.deltaTime;
        cooldownTimer.text = (cooldownTime + 1f).ToString().Split('.')[0];
        cooldownShading.fillAmount -= Time.deltaTime / cooldownShadingDivisor;
    }

    public void startCooldownTimer(float time)
    {
        onCooldown = true;
        cooldownTime = time;
        cooldownShadingDivisor = time;
        cooldownShading.fillAmount = 1f;
    }

}
