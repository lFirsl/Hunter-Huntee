using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("References")]
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public playScript playerScript;

    private void Start()
    {
        slider = GetComponent<Slider>();
        playerScript = GameObject.Find("Player").GetComponent<playScript>();
        slider.maxValue = playerScript.maxHealth;
    }

    private void Update()
    {
        slider.value = playerScript.currentHealth;
    }
    
    //THE FUNCTIONS BELOW CAN BE USED AS PLAN B
    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(float health)
    {
        slider.value = health;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
    
}