using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] Slider healthSlider;

    //sets the maximum values of the slider to the maxHealth value provided
    public void SetMaxHealth(int maxHeath)
    {
        healthSlider.maxValue = maxHeath;
        healthSlider.value = maxHeath;
    }

    //updates the health bar's value to the current health value
    public void UpdateHealthBar(int curHealth)
    {
        healthSlider.value = curHealth;
    }
}
