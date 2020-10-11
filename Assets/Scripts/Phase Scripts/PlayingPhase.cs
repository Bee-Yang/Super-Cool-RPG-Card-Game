using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayingPhase : MonoBehaviour
{
    private static double timeDelay = 0.5;
    private TurnControllerBehavior turnController;
    private PlayerManaBehavior playerMana;
    private double time;
    private bool start;

    public GameObject playerTurn, enemyTurn, notEnoughManaNotification, outOfMana;

    void OnEnable()
    {
        if(!turnController)
        {
            turnController = this.GetComponent<TurnControllerBehavior>();
        }

        time = 0;
        start = true;

        if(turnController.IsPlayerTurn)
        {
            turnController.EnableDraggingForPlayer();
        }
    }
 
    // Start is called before the first frame update
    void Start()
    {
        playerMana = GameObject.Find("PlayerMana").GetComponent<PlayerManaBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        TurnNotification();

        // Temporary code to directly enter battle phase on opponents turn after draw phase
        if(!turnController.IsPlayerTurn && !this.start)
        {
            turnController.DisableAllPhases();
            turnController.SetPhase(3);
        }
        // Make sure that mana notifications are not showing up during opponent's turn
        if(!turnController.IsPlayerTurn)
        {
	notEnoughManaNotification.SetActive(false);
        }
        // Check to see if mana is depleted, then show notification if so
	if(playerMana.Mana <= 0) {
		outOfMana.SetActive(true);
	} else {
		outOfMana.SetActive(false);
	}
    }

    private void SetStart(bool status)
    {
        this.start = status;
    }

    public void TurnNotification()
    {
        if (this.start)
        {
            // Add time delay to the timer
            this.time += Time.deltaTime;

            if (this.time > timeDelay)
            {
                // Notify the user about whose turn it is by enabling/disabling the turn notification after a time delay
                if (turnController.IsPlayerTurn)
                {
                    playerTurn.SetActive(!playerTurn.activeSelf);
                }
                else
                {
                    enemyTurn.SetActive(!enemyTurn.activeSelf);
                }

                // Disable this method once user has been notified about whose turn it is
                if (!playerTurn.activeSelf && !enemyTurn.activeSelf)
                {
                    this.start = false;

                    if (turnController.IsPlayerTurn)
                    {
                        GameObject.Find("EndTurnButton").GetComponent<Button>().enabled = true;
                    }
                }

                // Reset the timer
                this.time = 0;
            }
        }
    }

    public bool CardCanBePlayed(GameObject card)
    {
        // Get the mana cost of the card to be played
        int manaCost = card.GetComponent<CardAttributes>().GetCost();

        // Get the condition for if the card can be played
        bool canBePlayed = manaCost <= playerMana.Mana;

        // Check to see if card can be played
        if (canBePlayed)
        {
            // Decrease the player's mana by the card's cost
            playerMana.decreaseMana(manaCost);
	// Disable Mana Notification if it's still up
	notEnoughManaNotification.SetActive(false);
        }
        else
        {
            // Debug statement for not enough mana
            Debug.Log("Not Enough Mana");
	// Enable Mana Notification to show that you don't have enough mana to play that card
	notEnoughManaNotification.SetActive(true);
        }

        // Return condition of whether the card can be played
        return canBePlayed;
    }
}
