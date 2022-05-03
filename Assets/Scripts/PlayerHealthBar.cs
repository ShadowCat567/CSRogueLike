using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] Slider healthSlider;

    public void SetMaxHealth(int maxHeath)
    {
        healthSlider.maxValue = maxHeath;
        healthSlider.value = maxHeath;
    }

    public void UpdateHealthBar(int curHealth)
    {
        healthSlider.value = curHealth;
    }
}
