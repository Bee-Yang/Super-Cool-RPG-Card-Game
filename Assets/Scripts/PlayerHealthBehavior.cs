using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBehavior : MonoBehaviour
{
    private Text thisText;
    private int health;
    
    void Start()
    {
        thisText = GetComponent<Text>();
        
        // set health initially to be 20
        health = 20;
    }
    
    void Update() 
    {
        // // When Player's health goes down
        // if(player is hit)
        // {
        //     // subtract x amount from health
        //     health += 0;
        // }

        // update text of PlayerHealth element
        thisText.text = "Health: " + health;
    }
}
