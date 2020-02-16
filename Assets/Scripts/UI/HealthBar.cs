using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{

    public Health health;
    public Image barImage;

    public TextMeshProUGUI barText;

    private void Awake()
    {
        barImage = transform.Find("Bar").GetComponent<Image>();
        barText = transform.Find("HealthTextDisplay").GetComponent<TextMeshProUGUI>();
        health = new Health();
    }

    private void Update(){
        health.Update();
        float healthAmount =health.GetHealthNormalized();
        int healthDisplayAmaount = (int) Math.Round(healthAmount * 100);
                float maxHealth = health.MAX_HEALTH;

        barText.text = healthDisplayAmaount + " / " + maxHealth;
        barImage.fillAmount = healthAmount;
    }
}

public class Health
{

    public  float MAX_HEALTH = 100f;
    private float healthAmount;
    public float healthRegenAmount;

    public Health()
    {
        healthAmount = 100;
        healthRegenAmount = 5f;
    }

    public void Update()
    {
        healthAmount += healthRegenAmount * Time.deltaTime;
        if(healthAmount > MAX_HEALTH){
            healthAmount = MAX_HEALTH;
        }
    }

    public bool TrySpendHealth(float amount)
    {
        if (healthAmount >= amount)
        {
            healthAmount -= amount;
            return true;
        }
        return false;
    }

    public float GetHealthNormalized()
    {
        return healthAmount / MAX_HEALTH;
    }

}
