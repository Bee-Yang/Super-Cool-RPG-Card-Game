using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyGraveyardCountBehavior : MonoBehaviour
{
    private Text thisText;
    private int count;

    void Start()
    {
        thisText = GetComponent<Text>();
        
        // set count initially to be 0
        count = 0;
    }

    void Update()
    {
        // // When a card goes into graveyard
        // if(graveyard size increases)
        // {
        //     // add 1 to count
        //     count += 1;
        // }
        
        // update text of EnemyGraveyardCount element
        thisText.text = "" + count;
    }
}
