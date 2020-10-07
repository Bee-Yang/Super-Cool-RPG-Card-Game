using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyManaBehavior : MonoBehaviour
{
    private TMP_Text thisText;
    private int mana, maxMana;
    
    void Start()
    {
        thisText = GetComponent<TMP_Text>();

        // set mana initially to be 1
        maxMana = 1;
        mana = maxMana;
    }
    
    void Update() 
    {
        // // When Enemy's turn ends
        // if(enemy turn ends)
        // {
        //     // add 1 to mana
        //     maxMana += 1;
        // }
        
        // update text of EnemyMana element
        thisText.text = "Mana: " + mana + "/" + maxMana;
    }
}
