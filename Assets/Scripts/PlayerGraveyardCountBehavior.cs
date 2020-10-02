using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGraveyardCountBehavior : MonoBehaviour
{
    private GameObject graveyard;
    private Text thisText;
    private int count;

    void Start()
    {
        thisText = GetComponent<Text>();
        
        // set count initially to be 0
        count = 0;
        graveyard = GameObject.Find("Board/PlayerGraveyard");
    }

    void Update()
    {
        // When a card goes into graveyard
        if(graveyard.transform.childCount != count)
        {
             // change the count
             count = graveyard.transform.childCount;
        }
        
        // update text of PlayerGraveyardCount element
        thisText.text = "" + count;
    }
}
