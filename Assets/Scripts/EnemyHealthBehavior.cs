using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class EnemyHealthBehavior : MonoBehaviour
{
    private Text thisText;
    private int health;
    private static int maxHealth = 20;
    
    void Start()
    {
        thisText = GetComponent<Text>();
        
        // set health initially to be 20
        health = maxHealth;
    }
    
    void Update() 
    {
        // // When Enemy's health goes down
        // if(enemy is hit)
        // {
        //     // subtract x amount from health
        //     health += 0;
        // }

        if(health < maxHealth){
            thisText.color = Color.red;
        }

        // update text of EnemyHealth element
        thisText.text = "Health: " + health + "/" + maxHealth;
    }
}
