using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManaBehavior : MonoBehaviour
{
    private Text thisText;
    private int mana;
    
    void Start()
    {
        thisText = GetComponent<Text>();
        
        // set mana initially to be 1
        mana = 1;
    }
    
    void Update() 
    {
        // // When Enemy's turn ends
        // if(enemy turn ends)
        // {
        //     // add 1 to mana
        //     mana += 1;
        // }
        
        // update text of EnemyMana element
        thisText.text = "Mana: " + mana;
    }
}
