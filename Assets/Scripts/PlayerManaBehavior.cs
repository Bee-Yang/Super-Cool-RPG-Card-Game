using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManaBehavior : MonoBehaviour
{
    private Text thisText;
    private int mana, maxMana;
    
    void Start()
    {
        thisText = GetComponent<Text>();

        // set mana initially to be 1
        maxMana = 1;
        mana = maxMana;
    }
    
    void Update() 
    {
        // // When Player's turn ends
        // if(player turn ends)
        // {
        //     // add 1 to mana
        //     maxMana += 1;
        // }
        
        // update text of PlayerMana element
        thisText.text = "Mana: " + mana + "/" + maxMana;
    }

}
