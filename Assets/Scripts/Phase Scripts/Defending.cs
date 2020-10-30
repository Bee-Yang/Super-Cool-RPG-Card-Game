using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Defending : MonoBehaviour
{
    private static double timeDelay = 0.5;
    private TurnControllerBehavior turnController;
    private BattleController battleController;
    private Transform attackingField, blockingField;
    private Timer timer;
    private bool hasAttackCard, start;
    private int cardsForBlocking;

    private GameObject currentAttackCard;

    private List<GameObject> blockingCards;

    public GameObject CurrentAttackCard
    {
        get { return this.currentAttackCard; }
        set { this.currentAttackCard = value; }
    }

    public List<GameObject> BlockingCards
    {
        get { return this.blockingCards; }
        set { this.blockingCards = value; }
    }

    void Awake()
    {
        // Assign the controllers
        turnController = this.GetComponent<TurnControllerBehavior>();
        battleController = this.GetComponent<BattleController>();

        timer = GameObject.Find("Utility").GetComponent<Timer>();

        // Instantiate the list
        this.blockingCards = new List<GameObject>();
    }

    void OnEnable()
    {
        // Reset the values for the variables
        this.currentAttackCard = null;
        this.blockingCards.Clear();
        this.hasAttackCard = false;
        this.cardsForBlocking = 0;

        // Find the playing field of the current player
        if (turnController.IsPlayerTurn)
        {
            attackingField = GameObject.Find("PlayerPlayingField").transform;
            blockingField = GameObject.Find("OpponentPlayingField").transform;
        }
        else
        {
            attackingField = GameObject.Find("OpponentPlayingField").transform;
            blockingField = GameObject.Find("PlayerPlayingField").transform;

            GameObject.Find("StartBattleButton").GetComponent<Button>().interactable = true;
        }

        // Find the attacking card
        FindAttackCard();

        // Check if the attacking player has an attacking card
        if (currentAttackCard)
        {
            // Outline the attacking card
            currentAttackCard.GetComponent<Outline>().enabled = true;
        }

        if (blockingField.childCount > 0)
        {
            Transform card;

            // For loop to go through all cards on the blocking field
            for (int i = 0; i < blockingField.childCount; i++)
            {
                // Get the card at index i
                card = blockingField.GetChild(i);

                // Check if the card has already blocked this battle phase
                if (!card.GetComponent<CardBehavior>().Blocked)
                {
                    // Increment counter for cards that can be used for blocking
                    this.cardsForBlocking++;

                    // Check if it is the AI's turn
                    if (!turnController.IsPlayerTurn)
                    {
                        // Set the canBlock status of the card to true
                        card.GetComponent<CardBehavior>().SetCanBlock(true);
                    }
                }
            }
        }

        start = true;
        timer.SetTimeDelay(timeDelay);
        timer.enabled = true;
    }

    void OnDisable()
    {
        // Check if the player was blocking
        if (!turnController.IsPlayerTurn)
        {
            // Check if the player has creatures in play
            if (blockingField.childCount > 0)
            {
                Transform card;

                // For loop to go through all cards on the playing field
                for (int i = 0; i < blockingField.childCount; i++)
                {
                    // Get the card at index i
                    card = blockingField.GetChild(i);

                    // Set the canBlock status of the card to false
                    card.GetComponent<CardBehavior>().SetCanBlock(false);
                }
            }
        }

        if (timer.enabled)
        {
            timer.enabled = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (this.start)
        {
            if (timer.Delayed())
            {
                this.start = false;
                timer.enabled = false;
            }
        }
        else
        {
            // Check if the attacking player has an attacking card
            if (!hasAttackCard)
            {
                // Automatically end the player's turn
                battleController.SetPhase(0);

                // Disable this script
                this.enabled = false;
            }
            // Check if the blocking player has creatures for blocking
            else if (this.cardsForBlocking == 0)
            {
                // Move onto the damage calculation
                battleController.SetPhase(3);

                // Disable this script
                this.enabled = false;
            }
            // Check if the AI is the one blocking
            else if (turnController.IsPlayerTurn)
            {
                // Enable the AI's blocking script

                /**************** Temporary Code ******************/
                battleController.SetPhase(3);
                /**************** Temporary Code ******************/
            }
            else
            {
                // Enable the start battle button to click after the player finishes choosing the blocking creatures
                GameObject.Find("StartBattleButton").GetComponent<Button>().interactable = true;
            }
        }
    }

    private void FindAttackCard()
    {
        CardBehavior card;
        int i = 0;

        while (!currentAttackCard && i < attackingField.childCount)
        {
            card = attackingField.GetChild(i).GetComponent<CardBehavior>();

            if (card.Attacking)
            {
                currentAttackCard = card.gameObject;
                hasAttackCard = true;
            }
            else
            {
                i++;
            }
        }
    }
}