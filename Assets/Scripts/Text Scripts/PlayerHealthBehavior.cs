﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

public class PlayerHealthBehavior : MonoBehaviour
{
    private TMP_Text thisText;
    private int health;
    public Slider slider;
    public Image Color;
    public Gradient gradient;
    private static int maxHealth = 20;
    public int Health
    {
        get { return health; }
        set { health = value; }
    }


    void Start()
    {
        thisText = GetComponent<TMP_Text>();
        // set health initially to be 20
        health = maxHealth;
        SetHealth(health);
        SetMaxHealth(health);
    }
    public void SetMaxHealth(int health)
    {
        slider.value = slider.maxValue = health;
        Color.color = gradient.Evaluate(20f);
    }
    void Update()
    {
        // update text of PlayerHealth element
        thisText.text = "Health: " + health + "/" + maxHealth;
    }
    public void SetHealth(int health)
    {
        slider.value = health;
        Color.color = gradient.Evaluate(slider.normalizedValue);
    }


    public void DecreaseHealth(int amount)
    {

        if (health <= amount) health = amount;//prevents negative numbers
        SetHealth(health -= amount);//changes health and sets the health, don't touch
    }
}