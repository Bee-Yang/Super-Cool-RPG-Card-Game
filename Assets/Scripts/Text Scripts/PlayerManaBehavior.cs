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
        // update text of PlayerMana element
        thisText.text = "Mana: " + mana + "/" + maxMana;
    }

    public void increaseMana() {
        if (maxMana < 10){
            maxMana += 1;
        }
    }

    public void resetMana() {
        mana = maxMana;
    }

    public void decreaseMana(int cost) {
        mana -= cost;
    }

}
