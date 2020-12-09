using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bloodthirst : MonoBehaviour
{
    private AITargeting AITarget;
    private TurnControllerBehavior turnController;
    private EffectTargetList targetList;
    private Transform playerField, opponentField, playerHand;
    CardAttributes attributes;
    CardBehavior behavior;
    private bool effectUsed;

    void Awake()
    {
        // Set the variables to the correct values
        this.AITarget = GameObject.Find("AI").GetComponent<AITargeting>();
        this.turnController = GameObject.Find("TurnController").GetComponent<TurnControllerBehavior>();
        this.targetList = GameObject.Find("Utility").GetComponent<EffectTargetList>();
        this.playerField = GameObject.Find("PlayerPlayingField").transform;
        this.opponentField = GameObject.Find("OpponentPlayingField").transform;
        this.playerHand = GameObject.Find("PlayerHand").transform;
        this.effectUsed = false;
    }

    void OnEnable()
    {
        // Enable the EffectTargetList script
        this.targetList.enabled = true;

        // Check if it is the player's turn
        if (this.turnController.IsPlayerTurn)
        {
            // Disable the end turn and start battle buttons
            GameObject.Find("EndTurnButton").GetComponent<Button>().interactable = false;
            GameObject.Find("StartBattleButton").GetComponent<Button>().interactable = false;

            // Disable dragging for the player
            foreach (Transform card in this.playerHand)
            {
                this.behavior = card.GetComponent<CardBehavior>();

                this.behavior.SetDraggable(false);
            }

            //Iterate through each card in the opponent's field
            foreach (Transform card in this.playerField)
            {
                // Get the behavior of the card
                this.behavior = card.GetComponent<CardBehavior>();

                // Set its targetable status to true
                this.behavior.Targetable = true;

                // Add it to the list of targetable cards
                this.targetList.targets.Add(card.gameObject);
            }
        }
        else
        {
            //Iterate through each card in the opponent's field
            foreach (Transform card in this.opponentField)
            {
                // Add the card to the list of targetable cards
                this.targetList.targets.Add(card.gameObject);
            }
        }

        // Set the status of cannotPlay
        this.targetList.cannotPlay = this.targetList.targets.Count == 0;    // Check if the list of targetable cards is zero

        // Check if the card can be played
        if (!this.targetList.cannotPlay)
        {
            // Check if it is the player's turn
            if (this.turnController.IsPlayerTurn)
            {
                // Clear the list of targetable cards
                this.targetList.targets.Clear();
            }
            else
            {
                // Enable the AI targeting script
                this.AITarget.enabled = true;
            }
        }
    }

    void OnDisable()
    {
        // Check if it is the player's turn
        if (this.turnController.IsPlayerTurn)
        {
            // Enable the end turn and start battle buttons
            GameObject.Find("EndTurnButton").GetComponent<Button>().interactable = true;
            GameObject.Find("StartBattleButton").GetComponent<Button>().interactable = true;

            // Enable dragging for the player
            foreach (Transform card in this.playerHand)
            {
                this.behavior = card.GetComponent<CardBehavior>();

                this.behavior.SetDraggable(true);
            }
        }
        // Check if the AI targeting script is enabled
        else if (AITarget.enabled)
        {
            // Disable the AI targeting script
            AITarget.enabled = false;
        }

        // Reset the status of effectUsed to false
        this.effectUsed = false;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the targeting is done, the effect has not been used, and it can be played
        if (this.targetList.targetDone && !this.effectUsed && !this.targetList.cannotPlay)
        {
            // Set the status of effectUsed to true
            this.effectUsed = true;

            // Iterate through each card in the list of targets
            foreach (GameObject card in this.targetList.targets)
            {
                // Get the behavior of the card
                this.attributes = card.GetComponent<CardAttributes>();

                // Increase the card's attack by 3
                this.attributes.SetAttack(this.attributes.GetAttack() + 3);
            }

            // Set the effect done status to true
            this.targetList.effectDone = true;

            // Disable this script
            this.enabled = false;
        }
    }
}
