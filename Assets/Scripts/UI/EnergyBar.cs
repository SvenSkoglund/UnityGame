using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnergyBar : MonoBehaviour
{

    public Energy energy;
    public Image barImage;

    public TextMeshProUGUI barText;

    private void Awake()
    {
        barImage = transform.Find("Bar").GetComponent<Image>();
        barText = transform.Find("EnergyTextDisplay").GetComponent<TextMeshProUGUI>();
        energy = new Energy();
    }

    private void Update(){
        energy.Update();
        float energyAmount =energy.GetEnergyNormalized();
        int energyDisplayAmaount = (int) Math.Round(energyAmount * 100);
                float maxEnergy = energy.MAX_ENERGY;

        barText.text = energyDisplayAmaount + " / " + maxEnergy;
        barImage.fillAmount = energyAmount;
    }
}

public class Energy
{

    public  float MAX_ENERGY = 100f;
    private float energyAmount;
    private float energyRegenAmount;

    public Energy()
    {
        energyAmount = 0;
        energyRegenAmount = 30f;
    }

    public void Update()
    {
        energyAmount += energyRegenAmount * Time.deltaTime;
        if(energyAmount > MAX_ENERGY){
            energyAmount = MAX_ENERGY;
        }
    }

    public bool TrySpendEnergy(float amount)
    {
        if (energyAmount >= amount)
        {
            energyAmount -= amount;
            return true;
        }
        return false;
    }

    public float GetEnergyNormalized()
    {
        return energyAmount / MAX_ENERGY;
    }

}
