using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayPhase : MonoBehaviour
{
    private static double timeDelay = 0.5;
    private ManaBehavior mana;
    private Transform AIHand, AIField;
    private Timer timer;
    private List<Transform> currentHand;
    private bool done;

    public bool Done
    {
        get { return this.done; }
        set { this.done = value; }
    }

    void Awake()
    {
        this.mana = GameObject.Find("EnemyMana").GetComponent<ManaBehavior>();
        this.AIHand = GameObject.Find("OpponentHand").transform;
        this.AIField = GameObject.Find("OpponentPlayingField").transform;
        this.timer = GameObject.Find("Utility").GetComponent<Timer>();

        this.currentHand = new List<Transform>();
    }

    void OnEnable()
    {
        // Reset done status to false
        this.done = false;

        // Add cards to a list of current cards for the AI's hand
        foreach(Transform card in this.AIHand)
        {
            this.currentHand.Add(card);
        }

        // Set timer delay and enable timer
        this.timer.SetTimeDelay(timeDelay);
        this.timer.enabled = true;
    }

    void OnDisable()
    {
        // Check if the timer is enabled
        if (this.timer.enabled)
        {
            // Disable the timer
            this.timer.enabled = false;
        }

        // Clear the list for current cards in the AI's hand
        this.currentHand.Clear();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Check if the timer delayed has been reached and if the AI is not finished playing
        if (this.timer.Delayed() && !this.done)
        {
            // Select a card and try to play it
            PlayCard();
        }
    }

    public void PlayCard()
    {
        // Reset the timer
        timer.ResetTimer();

        // Temporary variables
        Transform temp, card;
        int tempCost, cardCost;

        // Check if the AI has more than 0 cards in its hand
        if (this.currentHand.Count > 0)
        {
            // Assign the first card in the AI's hand and its cost to the variables
            card = this.currentHand[0];
            cardCost = card.GetComponent<CardAttributes>().GetCost();

            // Iterate through all cards in the AI's current hand
            for (int i = 1; i < this.currentHand.Count; i++)
            {
                // Assign the next card and its cost to the variables
                temp = this.currentHand[i];
                tempCost = temp.GetComponent<CardAttributes>().GetCost();

                // Check if the current card's cost is greater than the AI's current mana
                if (cardCost > mana.Mana)
                {
                    // Set the temp card as the current card
                    card = temp;
                    cardCost = tempCost;
                }
                // Check if the temp card's cost is less than the AI's current mana and greater than the cost of the current card
                else if (tempCost <= mana.Mana && tempCost > cardCost)
                {
                    // Set the temp card as the current card
                    card = temp;
                    cardCost = tempCost;
                }
            }

            // Check if the card can be played
            if (cardCost <= mana.Mana && AIField.childCount < 9)
            {
                // Decrease the AI's mana by the card's cost
                mana.DecreaseMana(cardCost);

                // Check to see if the card is not a utility card
                if (card.GetComponent<CardAttributes>().GetCardType() != "Utility")
                {
                    // Put card onto the field
                    card.SetParent(AIField);
                    card.GetComponent<CardBehavior>().SetHoverable(true);
                    card.GetComponent<CardBehavior>().FlipCard("front");
                }

                card.GetComponent<CardBehavior>().PutInPlay();

                // Remove this card from the list of playable cards
                this.currentHand.Remove(card);
            }
            else
            {
                // Set done to true if there are no more cards that can be played
                done = true;
            }
        }
        else
        {
            // Set done to true if the AI has no cards in its hand
            done = true;
        }
    }
}
