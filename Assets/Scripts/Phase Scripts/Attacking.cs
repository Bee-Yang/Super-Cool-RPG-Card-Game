using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attacking : MonoBehaviour
{
    private TurnControllerBehavior turnController;
    private BattleController battleController;
    private Transform playingField;

    void Awake()
    {
        // Assign the controllers
        turnController = this.GetComponent<TurnControllerBehavior>();
        battleController = this.GetComponent<BattleController>();
    }

    void OnEnable()
    {
        // Find the playing field of the current player
        if (turnController.IsPlayerTurn)
        {
            playingField = GameObject.Find("PlayerPlayingField").transform;

            // Check if the player has creatures in play
            if (playingField.childCount > 0)
            {
                Transform card;

                // For loop to go through all cards on the playing field
                for (int i = 0; i < playingField.childCount; i++)
                {
                    // Get the card at index i
                    card = playingField.GetChild(i);

                    // Set the canAttack status of the card to true
                    card.GetComponent<CardBehavior>().SetCanAttack(true);

                    // Make the image of the card darker
                    card.GetChild(0).GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
                    card.GetChild(1).GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
                }
            }
        }
        /**********************************************************************/
        // Temporary code for manual testing
        else
        {
            playingField = GameObject.Find("OpponentPlayingField").transform;
        }
        /**********************************************************************/

        /**********************************************************************
        // Temporary code for manual testing
        if (playingField.childCount > 0)
        {
            Transform card;

            // For loop to go through all cards on the playing field
            for (int i = 0; i < playingField.childCount; i++)
            {
                // Get the card at index i
                card = playingField.GetChild(i);

                // Set the canAttack status of the card to true
                card.GetComponent<CardBehavior>().SetCanAttack(true);

                // Make the image of the card darker
                card.GetChild(0).GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
                card.GetChild(1).GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
            }
        }
        /**********************************************************************/
    }

    void OnDisable()
    {
        // Find the playing field of the current player
        if (turnController.IsPlayerTurn)
        {
            // Check if the player has creatures in play
            if (playingField.childCount > 0)
            {
                Transform card;

                // For loop to go through all cards on the playing field
                for (int i = 0; i < playingField.childCount; i++)
                {
                    // Get the card at index i
                    card = playingField.GetChild(i);

                    // Set the canAttack status of the card to false
                    card.GetComponent<CardBehavior>().SetCanAttack(false);
                }
            }

            // Disable the end turn button
            GameObject.Find("EndTurnButton").GetComponent<Button>().interactable = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the attacking player has creatures in play
        if (playingField.childCount == 0)
        {
            // Automatically end the player's turn
            battleController.SetPhase(0);

            // Disable this script
            this.enabled = false;
        }
        // Check if the AI is the one attacking
        else if (!turnController.IsPlayerTurn)
        {
            // Enable the AI's attacking script

            /**************** Temporary Code ******************/
            // Temporary code AI to go to next phase of battle
            battleController.SetPhase(2);
            /**************** Temporary Code ******************/

            this.enabled = false;
        }
    }
}
