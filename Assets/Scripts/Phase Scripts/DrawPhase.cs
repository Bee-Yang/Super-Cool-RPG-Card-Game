using UnityEngine;

public class DrawPhase : MonoBehaviour
{
    private static double timeDelay = 1.0;
    private DeckBehavior player, enemy;
    private TurnControllerBehavior turnController;
    private Transform hand;
    private Timer timer;
    private bool done;

    public GameObject notification;

    void Awake()
    {
        turnController = this.GetComponent<TurnControllerBehavior>();
    }

    void OnEnable()
    {
        if (!timer)
        {
            timer = GameObject.Find("Utility").GetComponent<Timer>();
        }

        if (turnController.IsPlayerTurn)
        {
            this.hand = GameObject.Find("PlayerHand").transform;
        }
        else
        {
            this.hand = GameObject.Find("OpponentHand").transform;
        }

        this.done = false;

        // Enable the timer when this script is enabled
        timer.SetTimeDelay(timeDelay);
        timer.enabled = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Set values for local variables
        player = GameObject.Find("PlayerDeck").GetComponent<DeckBehavior>();
        enemy = GameObject.Find("OpponentDeck").GetComponent<DeckBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.done)
        {
            // Draw a card after the time delay
            if (this.timer.Delayed())
            {
                turnController.CheckGameOverConditions();

                if (turnController.GetPhase() == 1)
                {
                    // Draw a card depending on whose turn it is
                    if (turnController.IsPlayerTurn)
                    {
                        player.Draw();
                    }
                    else
                    {
                        enemy.Draw();
                    }
                }

                this.done = true;

                // Disable the timer
                this.timer.enabled = false;
            }
        }
        // Check if there are more than 9 cards in the hand
        else if (hand.childCount > 9)
        { 
            // Check whose turn it is
            if (turnController.IsPlayerTurn)
            {
                // Make the player able discard a card
                EnableOrDisableDiscard(true);
            }
            else
            {
                // Make the AI discard a card
                AIDiscardRoutine();
            }
        }
        else
        {
            // Check if the player had to discard a card
            if (this.notification.activeSelf)
            {
                // Make the player unable to discard cards
                EnableOrDisableDiscard(false);
            }

            // Change the phase to playing phase
            turnController.SetPhase(2);

            // Disable this script
            this.enabled = false;
        }
    }

    public void EnableOrDisableDiscard(bool status)
    {
        // Enable the notification
        this.notification.SetActive(status);

        // Go through each card in the hand
        foreach(Transform card in hand)
        {
            // Set the discardable status of the card
            card.GetComponent<CardBehavior>().Discardable = status;
        }
    }

    public void AIDiscardRoutine()
    {
        // Declare temporary variables
        int i = 1;
        CardAttributes card, temp;
        
        // Assign the first card in the AI's hand
        card = hand.GetChild(0).GetComponent<CardAttributes>();

        // Go through all the cards in the AI's hand
        while (i < hand.childCount)
        {
            // Get the next card in the hand
            temp = hand.GetChild(i).GetComponent<CardAttributes>();

            // Check if the temp card's cost is less than the current card's cost
            if(temp.GetCost() < card.GetCost())
            {
                // Make the temp card the current card
                card = temp;
            }

            // Increment the index
            i++;
        }

        // Discard the selected card
        card.gameObject.GetComponent<CardBehavior>().SetDestroyed();
    }
}
