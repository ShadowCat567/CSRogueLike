using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    //this is code I reused from an old project
    //Vector3 localScale;

    [SerializeField] Slider healthSlider;
    [SerializeField] Image healthImg;

    // Start is called before the first frame update
    void Start()
    {
        //localScale = transform.localScale;
    }

    public void SetMaxHealth(int maxHeath)
    {
        healthSlider.maxValue = maxHeath;
        healthSlider.value = maxHeath;
    }

    public void UpdateHealthBar(int curHealth)
    {
        healthSlider.value = curHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
