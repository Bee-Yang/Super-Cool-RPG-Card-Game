using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerManaBehavior : MonoBehaviour
{
    private TMP_Text thisText;
    private int mana, maxMana;
    public int Mana {
        get { return mana; }
        set { mana = value; }
    }
    
    void Start()
    {
        thisText = GetComponent<TMP_Text>();

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
