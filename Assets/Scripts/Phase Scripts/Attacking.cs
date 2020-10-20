using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attacking : MonoBehaviour
{
    private TurnControllerBehavior turnController;
    private Transform playingField;

    void Awake()
    {
        // Assign the turnController if it is null
        if (!turnController)
        {
            turnController = this.GetComponent<TurnControllerBehavior>();
        }
    }

    void OnEnable()
    {
        // Find the playing field of the current player
        if (turnController.IsPlayerTurn)
        {
            playingField = GameObject.Find("PlayerPlayingField").transform;
        }
        else
        {
            playingField = GameObject.Find("OpponentPlayingField").transform;
        }

        // Check if the player has creatures in play
        if (playingField.childCount > 0)
        {
            Transform card;

            // For loop to go through all cards on the playing field
            for(int i = 0; i < playingField.childCount; i++)
            {
                card = playingField.GetChild(i);

                // Set the canAttack status of the card to true
                card.GetComponent<CardBehavior>().SetCanAttack(true);

                // Make the image of the card darker
                card.GetChild(0).GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
                card.GetChild(1).GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
